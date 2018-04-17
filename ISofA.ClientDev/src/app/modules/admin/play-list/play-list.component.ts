import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { PlayService } from '../../../services/play.service';
import { MatDialog, MatSnackBar } from '@angular/material';
import { AddPlayDialogComponent } from '../add-play-dialog/add-play-dialog.component';
import { EditPlayDialogComponent } from '../edit-play-dialog/edit-play-dialog.component';

@Component({
  selector: 'app-play-list',
  templateUrl: './play-list.component.html',
  styleUrls: ['./play-list.component.css']
})
export class PlayListComponent implements OnInit {

  plays: any[];
  playNameFilter: string;

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

  openEditPlayDialog(play: any) {
    let dialogRef = this.dialog.open(EditPlayDialogComponent, { data: play });

    dialogRef.afterClosed().subscribe(x => {
      if (x) {
        if (x.msg == 'success') {
          play.Name = x.data.Name;
          play.Genre = x.data.Genre;
          play.Director = x.data.Director;
          play.Actors = x.data.Actors;
          play.DurationMins = x.data.DurationMins;
          play.PosterUrl = x.data.PosterUrl;
          play.TrailerUrl = x.data.TrailerUrl;
          play.Description = x.data.Description;
          this.snackBar.open(`Updated Play "${x.data.Name}"`, undefined, { duration: 1400 });
        }
        else if(x.msg == 'delete') {
          this.plays.splice(this.plays.indexOf(play), 1);
          this.snackBar.open(`Deleted Play "${play.Name}"`, undefined, { duration: 1400 });
        }
      }
    });
  }

}
