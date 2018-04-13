import { Component, Input, OnInit } from '@angular/core';
import { TimeTableDataset } from './time-table-dataset';

@Component({
  selector: 'app-time-table',
  templateUrl: './time-table.component.html',
  styleUrls: ['./time-table.component.css']
})
export class TimeTableComponent implements OnInit {

  @Input('data') datasets: TimeTableDataset[];

  constructor() { }

  ngOnInit() {
  }

}
