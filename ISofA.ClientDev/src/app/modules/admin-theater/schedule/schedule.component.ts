import { Component, OnInit } from '@angular/core';
import { TimeTableDataset, TimeTableGroupData, TimeTableData } from '../../../shared/time-table/time-table-dataset';
import { AuthService } from '../../../services/auth.service';
import { TheaterService } from '../../theater/theater.service';
import { StageService } from '../../../services/stage.service';
import { ProjectionService } from '../../../services/projection.service';
import { PlayService } from '../../../services/play.service';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.css']
})
export class ScheduleComponent implements OnInit {

  dayStart: Date = new Date();
  showDays: number = 1;

  theater: any;
  projections: any[];
  stages: any[];
  plays: any[];
  ttDatasets: TimeTableDataset[];
  tempDatasets: TimeTableDataset[];
  finishedLoading: boolean = false;

  $classes = [{ 'tt-bar-default': true }, { 'tt-bar-primary': true }, { 'tt-bar-secondary': true }];

  constructor(
    private authService: AuthService,
    private theaterService: TheaterService,
    private stageService: StageService,
    private playService: PlayService,
    private projectionService: ProjectionService
  ) { }

  ngOnInit() {
  }

  private getData() {
    this.theaterService.getTheaterDetail("", this.authService.adminOfTheater)
      .subscribe((x) => {
        this.theater = x;
        this.getProjections();
      });
    this.stageService.getStages(this.authService.adminOfTheater)
      .subscribe((x) => {
        this.stages = x;
        this.getProjections();
      });
    this.playService.getPlays(this.authService.adminOfTheater)
      .subscribe((x) => {
        this.plays = x;
        this.getProjections();
      });
  }

  getProjections() {
    if (!this.theater || !this.stages || !this.plays)
      return;
    this.ttDatasets = [];
    this.tempDatasets = [];
    this.finishedLoading = false;
    this.projectionService.getProjections(this.theater.TheaterId, this.dayStart, this.showDays)
      .subscribe(x => {        
        this.loadSchedule(x);
      });
  }

  private loadSchedule(projections) {
    for (var i = 0; i < projections.length; ++i) {
      var projection = projections[i];
      projection.StartTime = this.convertUTCDateToLocalDate(new Date(projection.StartTime));
      var dataset = this.getOrCreateDataset(projection.StartTime);
      var group = dataset.groupedData[this.stageIdx(projection.StageId)];
      var groupIdx = this.insertIdx(projection, group.data);      
      var $class = null;
      if (group.data.length == 0) {
        $class = this.$classes[Math.floor(Math.random() * 3)];
      } else {
        var nbrClsIdx = 0;
        if (groupIdx == group.data.length || groupIdx == nbrClsIdx) {
          if (groupIdx != nbrClsIdx)
            nbrClsIdx = this.$classes.indexOf(group.data[groupIdx - 1].$class);
          var ox = Math.round(Math.random());
          switch (nbrClsIdx) {
            case 0:
              {
                $class = this.$classes[1 + ox];
                break;
              }
            case 1:
              {
                $class = this.$classes[2 * ox];
                break;
              }
            case 2:
              {
                $class = this.$classes[ox];
                break;
              }
          }
        } else {
          var nbr1Idx = this.$classes.indexOf(group.data[groupIdx - 1].$class);
          var nbr2Idx = this.$classes.indexOf(group.data[groupIdx].$class);
          var mod = ~(Math.pow(2, nbr1Idx) | Math.pow(2, nbr2Idx)) & 7;
          $class = this.$classes[Math.floor(Math.log2(mod))];
        }
      } // end of class picking
      var play =  this.plays[this.playIdx(projection.PlayId)];
      var startMins = (projection.StartTime as Date).getHours() * 60 + (projection.StartTime as Date).getMinutes();      
      var data: TimeTableData = {
        name: play.Name + '\n' + 'Price: ' + projection.Price,
        durationMins: play.DurationMins,
        $class: $class,
        startMins: startMins - this.theater.WorkStart,
      };
      group.data.splice(groupIdx, 0, data);
    }
    this.ttDatasets = this.tempDatasets;
    this.finishedLoading = true;
  }

  private convertUTCDateToLocalDate(date: Date) {
    var newDate = new Date(date.getTime()+date.getTimezoneOffset()*60*1000);

    var offset = date.getTimezoneOffset() / 60;
    var hours = date.getHours();

    newDate.setHours(hours - offset);

    return newDate;   
  }

  private stageIdx(stageId): number {
    for (var i = 0; i < this.stages.length; ++i)
      if (this.stages[i].StageId == stageId)
        return i;
    return -1;
  }

  private playIdx(playId): number {
    for (var i = 0; i < this.plays.length; ++i)
      if (this.plays[i].PlayId == playId)
        return i;
    return -1;
  }

  private insertIdx(projection: any, data: TimeTableData[]): number {
    var j = 0;
    var before = false;
    var startMins = (projection.StartTime as Date).getHours() * 60 + (projection.StartTime as Date).getMinutes();
    var durationMins = this.plays[this.playIdx(projection.PlayId)].DurationMins;
    for (var j = 0; j < data.length; ++j) {
      if (data[j].startMins > startMins - this.theater.WorkStart) {
        if ((before = j != 0 && data[j - 1].startMins + data[j - 1].durationMins > startMins - this.theater.WorkStart) || data[j].startMins < startMins - this.theater.WorkStart + durationMins) {
          return -1;
        } else {
          return j;
        }
      } else if (data[j].startMins == startMins - this.theater.WorkStart) {
        return -1;
      }
    }
    if (data.length > 0 && data.length != j) {
      if (data[j].startMins + data[j].durationMins > startMins - this.theater.WorkStart)
        return -1;
    }
    return j;
  }

  private getOrCreateDataset(date: Date): TimeTableDataset {
    var idx = 0;
    for (var i = 0; i < this.tempDatasets.length; ++i) {
      var startDate = this.tempDatasets[i].startDate.getDate();
      if (startDate < date.getDate())
        idx = i;
      if (startDate == date.getDate())
        return this.tempDatasets[i];
    }

    if (startDate && startDate < date.getDate())
      idx++;

    var groupData: TimeTableGroupData[] = [];
    for (var i = 0; i < this.stages.length; ++i) {
      groupData.push({ data: [], name: this.stages[i].Name });
    }

    var dataset = { groupedData: groupData, startDate: date };
    this.tempDatasets.splice(idx, 0, dataset);
    return dataset;
  }

}
