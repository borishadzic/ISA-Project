import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-edit-stage-dialog',
  templateUrl: './edit-stage-dialog.component.html',
  styleUrls: ['./edit-stage-dialog.component.css']
})
export class EditStageDialogComponent implements OnInit {

  edit: any = {};

  constructor(
    public dialogRef: MatDialogRef<EditStageDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
    this.edit.Name = this.data.Name;
    this.edit.SeatRows = this.data.SeatRows;
    this.edit.SeatColumns = this.data.SeatColumns;
  }

}
