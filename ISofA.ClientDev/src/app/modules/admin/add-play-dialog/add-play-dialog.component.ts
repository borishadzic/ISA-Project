import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-add-play-dialog',
  templateUrl: './add-play-dialog.component.html',
  styleUrls: ['./add-play-dialog.component.css']
})
export class AddPlayDialogComponent implements OnInit {

  play: any = {};
  duration: string;

  constructor(public dialogRef: MatDialogRef<AddPlayDialogComponent>) { }

  ngOnInit() {
  }

  buildPlay() {
    this.play.DurationMins = Number.parseInt(this.duration);

    return this.play;
  }
}
