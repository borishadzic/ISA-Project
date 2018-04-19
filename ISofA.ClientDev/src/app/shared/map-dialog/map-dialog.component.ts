import { Component, OnInit, Input, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-map-dialog',
  templateUrl: './map-dialog.component.html',
  styleUrls: ['./map-dialog.component.css']
})
export class MapDialogComponent implements OnInit {

  @Input() mapTitle: string = "Location";
  @Input() mapLongitude: number;
  @Input() mapLatitude: number;
  zoom: number;
  longitude: number;
  latitude: number;

  constructor(
    public dialogRef: MatDialogRef<MapDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  ngOnInit() {
    this.zoom = 12;
    this.mapTitle = this.data.title;
    this.mapLatitude = this.data.latitude;
    this.mapLongitude = this.data.longitude;
    this.latitude = this.data.latitude;
    this.longitude = this.data.longitude;
  }

}
