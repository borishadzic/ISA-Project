import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TheaterContainerService } from '../../../services/theater-container.service';
import { NavLinkData } from '../../../shared/template-navbar/template-navbar-data';
import { navLinks } from '../theater.nav-links';
import { MatDialog } from '@angular/material';
import { MapDialogComponent } from '../../../shared/map-dialog/map-dialog.component';
import { PlayService } from '../../../services/play.service';
import { ProjectionService } from '../../../services/projection.service';
import { StageService } from '../../../services/stage.service';
import { DateUtils } from '../../../shared/date-utils';



@Component({
  selector: 'app-theater-repertoire',
  templateUrl: './theater-repertoire.component.html',
  styleUrls: ['./theater-repertoire.component.css']
})
export class TheaterRepertoireComponent implements OnInit {

  navLinks: NavLinkData[] = navLinks;
  navLinkBase: string;

  loadedData: boolean = false;
  selectedDate: Date = new Date();

  plays: any[];
  stages: any[];

  constructor(
    private dialog: MatDialog,
    private route: ActivatedRoute,
    private playService: PlayService,
    private stageService: StageService,
    private projectionService: ProjectionService,
    public theater: TheaterContainerService
  ) { }

  ngOnInit() {
    this.route.url.subscribe(url => {
      this.theater.resolveTheater(url).subscribe(x => {
        this.navLinkBase = '/' + url[0] + '/' + url[1];
      });
      this.playService.getPlays(this.theater.TheaterId).subscribe(x => {
        this.plays = x;
        this.getProjections();
      });
      this.stageService.getStages(this.theater.TheaterId).subscribe( x=> {
        this.stages = x;
        this.getProjections();
      });
    });
  }

  getProjections() {
    if (!this.plays || !this.stages)
      return;
    this.loadedData = false;
    this.projectionService.getProjections(this.theater.TheaterId, this.selectedDate, 1).subscribe(x => {
      this.getRepertoire(x);
    });
  }

  private getRepertoire(projections) {
    for (var i = 0; i < this.plays.length; ++i) {
      this.plays[i].projections = [];
    }

    for (var i = 0; i < projections.length; ++i) {
      var projection = projections[i];
      var play = this.play(projection.PlayId);
      projection.StartTime = DateUtils.convertUTCDateToLocalDate(new Date(projection.StartTime));
      projection.Stage = this.stage(projection.StageId);
      for(var j = 0; j < play.projections.length; ++j)
        if (play.projections[j].StartTime > projection.StartTime)
          break;      
      (play.projections as any[]).splice(j, 0, projection);
    }

    this.loadedData = true;
  }

  private play(playId) {
    for(var i = 0; i < this.plays.length; ++i) 
      if (this.plays[i].PlayId == playId)
        return this.plays[i];

    return null;
  }

  private stage(stageId) {
    for(var i = 0; i < this.stages.length; ++i) 
      if (this.stages[i].StageId == stageId)
        return this.stages[i];

    return null;
  }

}
