import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TheaterService } from '../theater.service';

@Component({
  selector: 'app-theater-detail',
  templateUrl: './theater-detail.component.html',
  styleUrls: ['./theater-detail.component.css']
})
export class TheaterDetailComponent implements OnInit {

  id: number;
  type: string;
  theater: any;

  constructor(private route: ActivatedRoute,
              private theaterService: TheaterService) { }

  ngOnInit() {
    this.route.url.subscribe(url => {
      this.type = this.theaterService.theaterTypeFragment(url);
      this.id = this.theaterService.theaterIdFragment(url);
      this.postResolvedRoute();
    });
  }

  postResolvedRoute() {
    this.theaterService.getTheaterDetail(this.type, this.id).subscribe(x => {
      this.theater = x;      
    })
  }

}
