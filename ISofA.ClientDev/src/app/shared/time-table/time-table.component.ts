import { Component, Input, OnInit, ElementRef, ViewChild } from '@angular/core';
import { TimeTableDataset, TimeTableGroupData } from './time-table-dataset';
import { AfterViewInit } from '@angular/core/src/metadata/lifecycle_hooks';

@Component({
  selector: 'app-time-table',
  templateUrl: './time-table.component.html',
  styleUrls: ['./time-table.component.css']
})
export class TimeTableComponent implements OnInit, AfterViewInit {

  @ViewChild("timeAxis", {read: ElementRef}) timeAxis: ElementRef;

  @Input('data') datasets: TimeTableDataset[];
  @Input() ttRangeStart?: string;
  @Input() ttRangeDuration?: string;
  rangeStart: number;
  rangeDuration: number;

  timePreviewLineX: number = -100;
  svgPt1: SVGPoint;

  constructor() { }

  ngOnInit() {
    this.rangeStart = Number.parseInt(this.ttRangeStart || "0");
    this.rangeDuration = Number.parseInt(this.ttRangeDuration || "1439");    
  }

  ngAfterViewInit() {
    this.svgPt1 = (this.timeAxis.nativeElement as SVGSVGElement).createSVGPoint();
  }

  evenGroup(dataset:TimeTableDataset, group:TimeTableGroupData): boolean {    
    return (this.datasets.indexOf(dataset)*dataset.groupedData.length+dataset.groupedData.indexOf(group)) % 2 == 0;
  }

  mmove($event) {
    this.svgPt1.x = $event.clientX;
    this.svgPt1.y = $event.clientY;    
    this.timePreviewLineX = Math.round(this.svgPt1.matrixTransform((this.timeAxis.nativeElement as SVGSVGElement).getScreenCTM().inverse()).x);    
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
