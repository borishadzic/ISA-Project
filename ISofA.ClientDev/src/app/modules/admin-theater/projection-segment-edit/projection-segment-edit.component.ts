import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProjectionService } from '../../../services/projection.service';
import { StageService } from '../../../services/stage.service';
import { Params } from '@angular/router/src/shared';
import { PlayService } from '../../../services/play.service';
import { SeatService } from '../../../services/seat.service';
import { MatSnackBar } from '@angular/material';
import { TicketDiscountService } from '../../../services/ticket-discount.service';

@Component({
  selector: 'app-projection-segment-edit',
  templateUrl: './projection-segment-edit.component.html',
  styleUrls: ['./projection-segment-edit.component.css']
})
export class ProjectionSegmentEditComponent implements OnInit {

  finishedLoading: boolean = false;
  projectionId: number;

  seats: any[];
  existingSeats: any[];
  projection: any;
  stage: any;
  play: any
  discount: string;

  selRow: number = -1;
  selCol: number = -1;

  $classes = [
    { 'seat-none': true },
    { 'seat-reserved': true },
    { 'seat-vip': true },
    { 'seat-discount': true },
  ]

  constructor(
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private projectionService: ProjectionService,
    private stageService: StageService,
    private playService: PlayService,
    private seatService: SeatService,
    private ticketService: TicketDiscountService
  ) { }

  ngOnInit() {
    this.route.params.subscribe((params: Params) => {
      this.projectionId = Number.parseInt(params.projectionId);
      this.seatService.getSeats(this.projectionId).subscribe(seats => {
        this.existingSeats = seats;
        this.isFinished();
      })
      this.projectionService.getProjection(this.projectionId).subscribe(proj => {
        this.projection = proj;
        this.stageService.getStage(proj.TheaterId, proj.StageId).subscribe(stage => {
          this.stage = stage;
          this.isFinished();
        });
        this.playService.getPlay(proj.PlayId).subscribe(play => {
          this.play = play;
          this.isFinished();
        })
      })
    })
  }

  isFinished() {
    if (this.existingSeats && this.projection && this.stage && this.play) {

      var seats = new Array(this.stage.SeatRows * this.stage.SeatColumns);

      for (var i = 0; i < this.stage.SeatRows * this.stage.SeatColumns; ++i)
        seats[i] = { state: 0, $class: { 'seat-none': true } };

      for (var i = 0; i < this.existingSeats.length; ++i) {
        var idx = (this.existingSeats[i].SeatRow) * this.stage.SeatRows + this.existingSeats[i].SeatColumn;
        seats[idx].state = this.existingSeats[i].State + 1;

        seats[idx].$class = {
          'seat-reserved': seats[idx].state == 1,
          'seat-vip': seats[idx].state == 2,
          'seat-discount': seats[idx].state == 3
        };

      }
      this.seats = seats;
      this.finishedLoading = true;
      console.log(this.seats);
      console.log(this.existingSeats)
      console.log(this.projection)
      console.log(this.stage)
      console.log(this.play)
    }
  }

  selectSeat(col, row) {
    var seat = this.seats[row * this.stage.SeatRows + col];

    if (seat.state == 0) {

      if (this.selRow != -1 && this.selCol != -1) {
        var oldSeat = this.seats[this.selRow * this.stage.SeatRows + this.selCol];
        oldSeat.$class = { 'seat-none': true, 'active': false };
      }

      seat.$class = { 'seat-none': true, 'active': true };
      this.selRow = row;
      this.selCol = col;
    }

  }

  createVIP() {
    this.seatService.createVIPSeat(this.projection.TheaterId, this.projectionId, { SeatRow: this.selRow, SeatColumn: this.selCol })
    .subscribe(x=>{
      var oldSeat = this.seats[this.selRow * this.stage.SeatRows + this.selCol];
        oldSeat.state = 2;
        oldSeat.$class = { 'seat-vip': true };
        this.selRow = -1;
        this.selCol = -1;
        this.snackBar.open('VIP Seat Created', undefined, { duration: 1400 });
    })
  }

  createDiscount() {
    this.ticketService.createDiscountTicket(this.projection.TheaterId, this.projectionId, { SeatRow: this.selRow, SeatColumn: this.selCol, Discount: Number.parseInt(this.discount) })
      .subscribe(x => {
        var oldSeat = this.seats[this.selRow * this.stage.SeatRows + this.selCol];
        oldSeat.state = 3;
        oldSeat.$class = { 'seat-discount': true };
        this.selRow = -1;
        this.selCol = -1;
        this.snackBar.open('Discount Seat Created', undefined, { duration: 1400 });
      });
  }

}
