import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-edit-theater-dialog',
  templateUrl: './edit-theater-dialog.component.html',
  styleUrls: ['./edit-theater-dialog.component.css']
})
export class EditTheaterDialogComponent implements OnInit {

  edit: any = {};

  constructor(
    public dialogRef: MatDialogRef<EditTheaterDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }


  ngOnInit() {
    this.edit.Name = this.data.theater.Name;
    this.edit.Address = this.data.theater.Address;
  }

}
