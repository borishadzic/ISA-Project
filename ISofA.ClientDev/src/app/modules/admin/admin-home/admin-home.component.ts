import { Component, OnInit } from '@angular/core';
import { TimeTableDataset } from '../../../shared/time-table/time-table-dataset';

@Component({
  selector: 'app-admin-home',
  templateUrl: './admin-home.component.html',
  styleUrls: ['./admin-home.component.css']
})
export class AdminHomeComponent implements OnInit {

  testData: TimeTableDataset[] = [
    {
      startDate: new Date(),
      groupedData: [
        { name: "A1", data: [{ name: "Avengers", startMins: 0, durationMins: 120 }] },
        { name: "A2", data: [{ name: "Avengers", startMins: 253, durationMins: 121 }] }
      ]
    },
    {
      startDate: new Date(),
      groupedData: [
        { name: "A1", data: [{ name: "Avengers", startMins: 1021, durationMins: 122 }] },
        { name: "A2", data: [{ name: "Avengers", startMins: 0, durationMins: 223 }] }
      ]
    }
  ]

  constructor() { }

  ngOnInit() {
  }

}
