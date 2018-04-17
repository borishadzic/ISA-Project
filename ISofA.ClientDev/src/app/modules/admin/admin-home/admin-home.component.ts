import { Component, OnInit } from '@angular/core';
import { TimeTableDataset } from '../../../shared/time-table/time-table-dataset';
import { MatDialog, MatSnackBar } from '@angular/material';
import { TheaterEditDialogComponent } from '../theater-edit-dialog/theater-edit-dialog.component';
import { AuthService } from '../../../services/auth.service';
import { TheaterService } from '../../theater/theater.service';

@Component({
  selector: 'app-admin-home',
  templateUrl: './admin-home.component.html',
  styleUrls: ['./admin-home.component.css']
})
export class AdminHomeComponent implements OnInit {

  theater: any;

  constructor(private dialog: MatDialog,
    private snackBar: MatSnackBar,
    private theaterService: TheaterService,
    private authService: AuthService) { }

  ngOnInit() {
    this.theaterService.getTheaterDetail("", this.authService.adminOfTheater).subscribe(x => {
      this.theater = x;
    })
  }

  openEditTheaterDialog() {
    let dialogRef = this.dialog.open(TheaterEditDialogComponent, {
      data: this.theater
    });

    dialogRef.afterClosed().subscribe(x => {
      if (x) {
        this.theaterService.updateTheater(this.authService.adminOfTheater, x).subscribe(() => {
          this.theater.Name = x.Name;
          this.theater.Address = x.Address;
          this.theater.Description = x.Description;
          this.theater.WorkStart = x.WorkStart;
          this.theater.WorkDuration = x.WorkDuration;
          this.theater.Latitude = x.Latitude;
          this.theater.Longitude = x.Longitude;
          this.snackBar.open(`${x.Name} Updated`, undefined, { duration: 1500 });
        });
      }
    });
  }

}
