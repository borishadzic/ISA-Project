import { Component, OnInit } from '@angular/core';
import { NavLinkData } from '../../../shared/template-navbar/template-navbar-data';
import { navLinks } from '../theater.nav-links';
import { ActivatedRoute } from '@angular/router';
import { TicketDiscountService } from '../../../services/ticket-discount.service';
import { TheaterContainerService } from '../../../services/theater-container.service';
import { SpeedSeatModel } from '../../../models/speed-seat-model';
import { MatSnackBar } from '@angular/material';

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
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    public theater: TheaterContainerService,
    private ticketService: TicketDiscountService
  ) { }

  ngOnInit() {
    this.route.url.subscribe(url => {
      this.theater.resolveTheater(url).subscribe(x => {
        this.navLinkBase = '/' + url[0] + '/' + url[1];
      });
      this.ticketService.getDiscountTickets(this.theater.TheaterId).subscribe(x => {
        this.tickets = x;
      });
    });
  }

  reserveDiscountTicket(seat: SpeedSeatModel) {
    this.ticketService.reserveDiscountTicket(seat).subscribe(x => {
      this.tickets.splice(this.tickets.indexOf(seat), 1);
      this.snackBar.open(`Reservation made for ${seat.PlayName}`, undefined, { duration: 1400 });
    });
  }
}
