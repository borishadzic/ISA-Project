import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-add-stage-dialog',
  templateUrl: './add-stage-dialog.component.html',
  styleUrls: ['./add-stage-dialog.component.css']
})
export class AddStageDialogComponent implements OnInit {

  stage: any = {};

  constructor(public dialogRef: MatDialogRef<AddStageDialogComponent>) { }

  ngOnInit() {
  }

}
