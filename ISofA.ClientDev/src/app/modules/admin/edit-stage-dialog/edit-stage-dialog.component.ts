import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-edit-stage-dialog',
  templateUrl: './edit-stage-dialog.component.html',
  styleUrls: ['./edit-stage-dialog.component.css']
})
export class EditStageDialogComponent implements OnInit {

  name: string;

  constructor(
    public dialogRef: MatDialogRef<EditStageDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
    this.name = this.data.stage;
  }

}
