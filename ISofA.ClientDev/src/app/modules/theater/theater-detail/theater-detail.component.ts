import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TheaterContainerService } from '../../../services/theater-container.service';
import { NavLinkData } from '../../../shared/template-navbar/template-navbar-data';
import { navLinks } from '../theater.nav-links';
import { MatDialog } from '@angular/material';
import { MapDialogComponent } from '../../../shared/map-dialog/map-dialog.component';


@Component({
  selector: 'app-theater-detail',
  templateUrl: './theater-detail.component.html',
  styleUrls: ['./theater-detail.component.css']
})
export class TheaterDetailComponent implements OnInit {

  navLinks: NavLinkData[] = navLinks;
  navLinkBase: string;

  constructor(
    private dialog: MatDialog,
    private route: ActivatedRoute,
    public theater: TheaterContainerService
  ) { }

  ngOnInit() {
    this.route.url.subscribe(url => {
      this.theater.resolveTheater(url).subscribe(x => {
        this.navLinkBase = '/' + url[0] + '/' + url[1];
      });
    });
  }

  openLocationDialog() {
    this.dialog.open(MapDialogComponent, { data: { title: this.theater.Name, longitude: this.theater.Longitude, latitude: this.theater.Latitude } });
  }

}
