import { Component, OnInit } from '@angular/core';
import { NavLinkData } from '../../../shared/template-navbar/template-navbar-data';
import { navLinks } from '../theater.nav-links';
import { ActivatedRoute } from '@angular/router';
import { TheaterContainerService } from '../../../services/theater-container.service';
import { SeatService } from '../../../services/seat.service';
import { SpeedSeatModel } from '../../../models/speed-seat-model';

@Component({
  selector: 'app-discount-ticket-list',
  templateUrl: './discount-ticket-list.component.html',
  styleUrls: ['./discount-ticket-list.component.css']
})
export class DiscountTicketListComponent implements OnInit {

  navLinks: NavLinkData[] = navLinks;
  navLinkBase: string;
  
  tickets: SpeedSeatModel[];

  constructor(    
    private route: ActivatedRoute,
    public theater: TheaterContainerService,
    private seatService: SeatService
  ) { }

  ngOnInit() {
    this.route.url.subscribe(url => {
      this.theater.resolveTheater(url).subscribe(x => {
        this.navLinkBase = '/' + url[0] + '/' + url[1];
      });
      this.seatService.getSpeedSeats(this.theater.TheaterId).subscribe(x=>{
        this.tickets = x;
      });
    });
  }

}
