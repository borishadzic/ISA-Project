import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-stage-add-dialog',
  templateUrl: './stage-add-dialog.component.html',
  styleUrls: ['./stage-add-dialog.component.css']
})
export class StageAddDialogComponent implements OnInit {

  stage: any = {};

  constructor(public dialogRef: MatDialogRef<StageAddDialogComponent>) { }

  ngOnInit() {
  }

}
