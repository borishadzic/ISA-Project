import { Component, OnInit } from '@angular/core';
import { TimeTableDataset } from '../../../shared/time-table/time-table-dataset';
import { MatDialog } from '@angular/material';
import { EditTheaterDialogComponent } from '../edit-theater-dialog/edit-theater-dialog.component';
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
    private theaterService: TheaterService,
    private authService: AuthService) { }

  ngOnInit() {
    this.theaterService.getTheaterDetail("", this.authService.adminOfTheater).subscribe(x => {
      this.theater = x;
    })
  }

  openEditTheaterDialog() {
    let dialogRef = this.dialog.open(EditTheaterDialogComponent, {
      data: { theater: this.theater }
    });

    dialogRef.afterClosed().subscribe(x => {
      if (x) {

      }
    });
  }

}
