import { Component, OnInit } from '@angular/core';
import { MatDialog, MatSnackBar } from '@angular/material';
import { TheaterService } from '../../theater/theater.service';
import { AuthService } from '../../../services/auth.service';
import { PlayService } from '../../../services/play.service';
import { StageService } from '../../../services/stage.service';
import { TimeTableDataset, TimeTableGroupData, TimeTableData } from '../../../shared/time-table/time-table-dataset';
import { ProjectionService } from '../../../services/projection.service';

export class ProjectionTemplateModel {
  playId: number;
  stageId: number;
  price: number;
  daysOffset: number;
  startMins: number;
  durationMins: number;
}

@Component({
  selector: 'app-schedule-edit',
  templateUrl: './schedule-edit.component.html',
  styleUrls: ['./schedule-edit.component.css']
})
export class ScheduleEditComponent implements OnInit {

  theater: any;
  plays: any[];
  stages: any[];

  selPlay?: any;
  selStage?: any;
  price: string = '0';
  daysOffset: number = 1;
  daysRepeat: number = 0;
  startHrs: number;
  startMins: number;

  projectionTemplates: ProjectionTemplateModel[] = [];
  ttDatasets: TimeTableDataset[] = [];

  $classes = [{ 'tt-bar-default': true }, { 'tt-bar-primary': true }, { 'tt-bar-secondary': true }];

  constructor(private dialog: MatDialog,
    private snackBar: MatSnackBar,
    private authService: AuthService,
    private theaterService: TheaterService,
    private playService: PlayService,
    private stageService: StageService,
    private projectionService: ProjectionService
  ) { }

  ngOnInit() {
    this.theaterService.getTheaterDetail(null, this.authService.adminOfTheater)
      .subscribe((x) => {        
        this.theater = x;
        this.startHrs = Math.floor(x.WorkStart / 60);
        this.startMins = x.WorkStart % 60;
      });
    this.playService.getPlays(this.authService.adminOfTheater)
      .subscribe((x) => {
        this.plays = x;
      });
    this.stageService.getStages(this.authService.adminOfTheater)
      .subscribe((x) => {
        this.stages = x;
      })
  }

  applySchedule() {
    this.projectionService.addProjections(this.theater.TheaterId, this.projectionTemplates.map(x => {
      var startTime = new Date();
      startTime.setDate(startTime.getDate() + x.daysOffset + Math.floor(x.startMins / 1440));
      startTime.setSeconds(0);
      startTime.setMilliseconds(0);
      startTime.setHours(Math.floor((x.startMins % 1440) / 60));
      startTime.setMinutes(x.startMins % 60);
      console.log(startTime.toUTCString());
      return {
        StartTime: startTime,
        Price: x.price,
        PlayId: x.playId,
        StageId: x.stageId
      }
    }))
      .subscribe(x => {
        console.log('succes!');
      });
  }

  addProjection() {
    if (this.selPlay && this.selStage) {
      var templates: ProjectionTemplateModel[] = [];
      for (var i = 0; i <= this.daysRepeat; ++i) {
        var template: ProjectionTemplateModel = {
          playId: this.selPlay.PlayId,
          stageId: this.selStage.StageId,
          price: Number.parseInt(this.price),
          daysOffset: this.daysOffset + i,
          startMins: this.startHrs * 60 + this.startMins,
          durationMins: this.selPlay.DurationMins
        }
        if (this.canAddTemp(template) == -1)
          return;
        templates.push(template);
      }

      this.projectionTemplates.push(...templates);

      for (var i = 0; i <= this.daysRepeat; ++i) {

        var template = templates[i];
        var groupIdx = this.canAddTemp(template);
        console.log(groupIdx);
        var dataset = this.getOrCreateDataset(template.daysOffset);
        var group = dataset.groupedData[this.stages.indexOf(this.selStage)];
        var $class = null;
        if (group.data.length == 0) {
          $class = this.$classes[Math.floor(Math.random() * 3)];
          console.log('3pick');
        } else {
          var nbrClsIdx = 0;
          if (groupIdx == group.data.length || groupIdx == nbrClsIdx) {
            if (groupIdx != nbrClsIdx)
              nbrClsIdx = this.$classes.indexOf(group.data[groupIdx - 1].$class);
            var ox = Math.round(Math.random());
            console.log('2pick');
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
            console.log('mod: ' + mod);
            $class = this.$classes[Math.floor(Math.log2(mod))];
          }
        }
        var data: TimeTableData = {
          name: this.selPlay.Name + '\n' + 'Price: ' + template.price,
          durationMins: this.selPlay.DurationMins,
          $class: $class,
          startMins: template.startMins - this.theater.WorkStart,
          template: template
        } as TimeTableData;
        group.data.splice(groupIdx, 0, data);
      }

    }
  }

  private canAddTemp(template: ProjectionTemplateModel): number {
    var date = new Date();
    date.setDate(date.getDate() + template.daysOffset);
    var idx = 0;
    for (var i = 0; i < this.ttDatasets.length; ++i) {
      var startDate = this.ttDatasets[i].startDate.getDate();
      if (startDate == date.getDate()) {
        var group = this.ttDatasets[i].groupedData[this.stages.indexOf(this.selStage)];
        var j = 0;
        var before = false;
        for (var j = 0; j < group.data.length; ++j) {
          if (group.data[j].startMins > template.startMins - this.theater.WorkStart) {
            if ((before = j != 0 && group.data[j - 1].startMins + group.data[j - 1].durationMins > template.startMins - this.theater.WorkStart) || group.data[j].startMins < template.startMins - this.theater.WorkStart + template.durationMins) {
              return -1; // todo: error msg if before then the projection j-1 broke
            } else {
              return j;
            }
          } else if (group.data[j].startMins == template.startMins - this.theater.WorkStart) {
            return -1; // todo error msg
          }
        }
        if (group.data.length > 0 && group.data.length != j) {
          if (group.data[j].startMins + group.data[j].durationMins > template.startMins - this.theater.WorkStart)
            return -1; // todo error msg
        }
        return j;
      }
      else if (startDate > date.getDate())
        return 0;
    }
    return 0;
  }

  private getOrCreateDataset(offset: number): TimeTableDataset {
    var date = new Date();
    date.setDate(date.getDate() + offset);
    var idx = 0;
    for (var i = 0; i < this.ttDatasets.length; ++i) {
      var startDate = this.ttDatasets[i].startDate.getDate();
      if (startDate < date.getDate())
        idx = i;
      if (startDate == date.getDate())
        return this.ttDatasets[i];
    }

    if (startDate && startDate < date.getDate())
      idx++;

    var groupData: TimeTableGroupData[] = [];
    for (var i = 0; i < this.stages.length; ++i) {
      groupData.push({ data: [], name: this.stages[i].Name });
    }

    var dataset = { groupedData: groupData, startDate: date };
    this.ttDatasets.splice(idx, 0, dataset);
    return dataset;
  }
}
