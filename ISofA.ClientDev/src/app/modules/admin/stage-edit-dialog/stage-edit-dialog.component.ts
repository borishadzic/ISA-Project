import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-stage-edit-dialog',
  templateUrl: './stage-edit-dialog.component.html',
  styleUrls: ['./stage-edit-dialog.component.css']
})
export class StageEditDialogComponent implements OnInit {

  edit: any = {};

  constructor(
    public dialogRef: MatDialogRef<StageEditDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
    this.edit.Name = this.data.Name;
    this.edit.SeatRows = this.data.SeatRows;
    this.edit.SeatColumns = this.data.SeatColumns;
  }

}
