import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatSnackBar } from '@angular/material';
import { PlayService } from '../../../services/play.service';

@Component({
  selector: 'app-edit-play-dialog',
  templateUrl: './edit-play-dialog.component.html',
  styleUrls: ['./edit-play-dialog.component.css']
})
export class EditPlayDialogComponent implements OnInit {

  public edit: any = {};
  duration: string;

  constructor(
    private snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<EditPlayDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private playService: PlayService) { }

  ngOnInit() {
    this.edit.Name = this.data.Name;
    this.edit.Genre = this.data.Genre;
    this.edit.Director = this.data.Director;
    this.edit.Actors = this.data.Actors;
    this.duration = this.data.DurationMins+'';
    this.edit.PosterUrl = this.data.PosterUrl;
    this.edit.TrailerUrl = this.data.TrailerUrl;
    this.edit.Description = this.data.Description;
  }

  savePlay() {
    this.edit.DurationMins = Number.parseInt(this.duration);

    this.playService.updatePlay(this.data.TheaterId, this.data.PlayId, this.edit)
    .subscribe((x)=>{      
      this.dialogRef.close({msg: 'success', data: this.edit});
    });
  }

  deletePlay() {
    this.playService.deletePlay(this.data.TheaterId, this.data.PlayId)
    .subscribe((x)=>{
      this.dialogRef.close({msg: 'delete'});
    });

  }

}
