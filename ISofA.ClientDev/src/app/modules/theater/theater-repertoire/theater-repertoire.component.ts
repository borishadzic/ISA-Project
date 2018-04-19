import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TheaterContainerService } from '../../../services/theater-container.service';
import { NavLinkData } from '../../../shared/template-navbar/template-navbar-data';
import { navLinks } from '../theater.nav-links';
import { MatDialog } from '@angular/material';
import { MapDialogComponent } from '../../../shared/map-dialog/map-dialog.component';
import { PlayService } from '../../../services/play.service';



@Component({
  selector: 'app-theater-repertoire',
  templateUrl: './theater-repertoire.component.html',
  styleUrls: ['./theater-repertoire.component.css']
})
export class TheaterRepertoireComponent implements OnInit {

  navLinks: NavLinkData[] = navLinks;
  navLinkBase: string;
  plays: any[];
  playNameFilter: string;

  constructor(
    private dialog: MatDialog,
    private route: ActivatedRoute,
    private playService: PlayService,
    public theater: TheaterContainerService    
  ) { }  

  ngOnInit() {
    this.route.url.subscribe(url => {
      this.theater.resolveTheater(url).subscribe(x => {
        this.navLinkBase = '/' + url[0] + '/' + url[1];
      });
      this.playService.getPlays(this.theater.TheaterId).subscribe(x=>{
        this.plays = x;
      })
    });
  }

}
