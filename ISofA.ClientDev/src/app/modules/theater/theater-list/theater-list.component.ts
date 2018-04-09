import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TheaterService } from '../theater.service';


@Component({
  selector: 'app-theater-list',
  templateUrl: './theater-list.component.html',
  styleUrls: ['./theater-list.component.css']
})
export class TheaterListComponent implements OnInit {

  type: string;
  Header: string;
  theaters: any[];

  constructor(private route: ActivatedRoute,
    private theaterService: TheaterService) { }

  ngOnInit() {
    this.route.url.subscribe(url => {
      this.type = this.theaterService.theaterTypeFragment(url);
      this.Header = this.type[0].toUpperCase() + this.type.substr(1);
      this.postResolvedRoute();
    });
  }

  postResolvedRoute() {
    this.theaterService.getTheaters(this.type).subscribe(x => {
      this.theaters = x;
    });
  }

}
