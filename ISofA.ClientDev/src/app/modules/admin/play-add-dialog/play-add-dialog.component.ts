import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-play-add-dialog',
  templateUrl: './play-add-dialog.component.html',
  styleUrls: ['./play-add-dialog.component.css']
})
export class PlayAddDialogComponent implements OnInit {

  play: any = {};
  duration: string;

  constructor(public dialogRef: MatDialogRef<PlayAddDialogComponent>) { }

  ngOnInit() {
  }

  buildPlay() {
    this.play.DurationMins = Number.parseInt(this.duration);

    return this.play;
  }
}
