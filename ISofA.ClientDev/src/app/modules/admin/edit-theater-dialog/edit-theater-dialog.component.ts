import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-edit-theater-dialog',
  templateUrl: './edit-theater-dialog.component.html',
  styleUrls: ['./edit-theater-dialog.component.css']
})
export class EditTheaterDialogComponent implements OnInit {

  edit: any = {};
  startHrs: any;
  startMins: any;
  durHrs: any;
  durMins: any;

  constructor(
    public dialogRef: MatDialogRef<EditTheaterDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }


  ngOnInit() {
    this.edit.Name = this.data.Name;
    this.edit.Address = this.data.Address;
    this.edit.Description = this.data.Description;
    this.startHrs = Math.floor(this.data.WorkStart / 60);
    this.startMins = this.data.WorkStart % 60;
    this.durHrs = Math.floor(this.data.WorkDuration / 60);
    this.durMins = this.data.WorkDuration % 60;
    this.edit.Latitude = this.data.Latitude;
    this.edit.Longitude = this.data.Longitude;
  }

  finishEdit(): any {
    this.edit.WorkStart = this.startHrs * 60 + this.startMins;
    this.edit.WorkDuration = this.durHrs * 60 + this.durMins;

    return this.edit;
  }

}
