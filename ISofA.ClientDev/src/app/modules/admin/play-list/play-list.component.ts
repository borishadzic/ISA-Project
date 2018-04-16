import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { PlayService } from '../../../services/play.service';
import { MatDialog, MatSnackBar } from '@angular/material';
import { AddPlayDialogComponent } from '../add-play-dialog/add-play-dialog.component';

@Component({
  selector: 'app-play-list',
  templateUrl: './play-list.component.html',
  styleUrls: ['./play-list.component.css']
})
export class PlayListComponent implements OnInit {

  plays: any[];

  constructor(private dialog: MatDialog,
    private snackBar: MatSnackBar,
    private authService: AuthService,
    private playService: PlayService) { }

  ngOnInit() {
  }

  getPlays() {
    if (this.plays)
      return;

    this.playService.getPlays(this.authService.adminOfTheater).subscribe((x) => {
      this.plays = x;
    });
  }

  openAddPlayDialog() {
    let dialogRef = this.dialog.open(AddPlayDialogComponent);

    dialogRef.afterClosed().subscribe(x => {
      if (x) {
        this.playService.addPlay(this.authService.adminOfTheater, x).subscribe((y) => {
          x.PlayId = y.PlayId;
          this.plays.push(x);
          this.snackBar.open(`Created Play "${y.Name}"`, undefined, { duration: 1500 });
        })
      }
    });
  }

}
