import { Component, Input, OnInit } from '@angular/core';
import { TimeTableDataset, TimeTableGroupData } from './time-table-dataset';

@Component({
  selector: 'app-time-table',
  templateUrl: './time-table.component.html',
  styleUrls: ['./time-table.component.css']
})
export class TimeTableComponent implements OnInit {

  @Input('data') datasets: TimeTableDataset[];
  @Input() ttRangeStart?: string;
  @Input() ttRangeDuration?: string;
  rangeStart: number;
  rangeDuration: number;

  constructor() { }

  ngOnInit() {
    this.rangeStart = Number.parseInt(this.ttRangeStart || "0");
    this.rangeDuration = Number.parseInt(this.ttRangeDuration || "1439");    
  }

  evenGroup(dataset:TimeTableDataset, group:TimeTableGroupData): boolean {    
    return (this.datasets.indexOf(dataset)*dataset.groupedData.length+dataset.groupedData.indexOf(group)) % 2 == 0;
  }

  markerLineX(n, divs): string {
    var x = (n / divs) * this.rangeDuration;
    if (n == divs)
      x = x - 2;
    return x + '';
  }

  range(n, divs): string {
    var x = this.rangeStart + (n / divs) * (this.rangeDuration);
    x = Math.ceil(x) % 1440;
    return this.pad(Math.floor(x / 60), 2) + ":" + this.pad(x % 60, 2);
  }

  // number formatting
  pad(n, width, z?): string {
    z = z || '0';
    n = n + '';
    return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
  }

}
