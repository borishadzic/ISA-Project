import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Router, ActivatedRoute, Params } from '@angular/router'

import { ReservationModel } from '../../../models/reservation-model';
import { StageModel } from '../../../models/stage-model';


@Component({
  selector: 'app-reservation',
  templateUrl: './reservation.component.html',
  styleUrls: ['./reservation.component.css']
})
export class ReservationComponent implements OnInit {
  public mySeats: ReservationModel[];
  public stage: StageModel;
  public rows: any;
  public columns: any;
  public theaterId : number;
  public stageId : number;
  public projectionId : number;

  constructor(private http: HttpClient, private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(
      (params:Params)=>{
        this.theaterId = params['theaterId'];
        this.stageId = params['stageId'];
        this.projectionId = params['projectionId'];
        this.http.get<StageModel>(environment.hostUrl + '/api/Theaters/'+this.theaterId.toString()+'/Stages/'+this.stageId.toString()).subscribe(
          (stage)=>{
            this.stage = stage;
            this.rows = Array(stage.SeatRows).fill(0).map((x,i)=>i);
            this.columns = Array(stage.SeatColumns).fill(0).map((x,i)=>i);
          }
        )
      }
    );
  }

  onAddSeat(row:number, column:number){
    let seat = this.mySeats.find( x=> x.SeatColumn == column.toString() && x.SeatRow == row.toString());
    if ( seat != null){
      var index = this.mySeats.indexOf(seat);
      this.mySeats.splice(index,1);
    } else {
      seat.SeatRow=row.toString();
      seat.SeatColumn = column.toString();
      this.mySeats.push(seat);
    }
  }

  onConfirmReservations(){
    this.http.post(environment.hostUrl +'/api/Theaters/'+this.theaterId+'/Projections/'+this.projectionId+'/ReserveSeats',this.mySeats)
    .subscribe(
      ()=>alert('Reservations confirmed'),
      ()=>alert('An error has occured!')
    )
  }

}
