import { Component, OnInit, AfterViewInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Router, ActivatedRoute, Params } from '@angular/router'

import { ReservationModel } from '../../../models/reservation-model';
import { StageModel } from '../../../models/stage-model';
import { ProjectionModel } from '../../../models/projection-model';


@Component({
  selector: 'app-reservation',
  templateUrl: './reservation.component.html',
  styleUrls: ['./reservation.component.css']
})
export class ReservationComponent implements OnInit, AfterViewInit {
  ngAfterViewInit(): void {
    this.activatedRoute.params.subscribe(
      (params:Params)=>{
        console.log(params);
        this.projectionId = params['projectionId'];
        this.getProjection().subscribe(
          (proj)=> {
            if (proj==null){
              alert('No such projection');
              return
            }
            this.projection= proj;
            this.theaterId = proj.TheaterId.toString();
            this.getExistingSeats(proj.ProjectionId).subscribe(
              (existing)=>{
                this.existingSeats = existing;
              }
            );
            this.getStage(proj.StageId).subscribe(
              (stage)=> {
                this.stage = stage;
                this.rows = Array(stage.SeatRows).fill(0).map((x,i)=>i);
                this.columns = Array(stage.SeatColumns).fill(0).map((x,i)=>i);
                
              }
            );
          }
        )
      }
    );
  }
  public mySeats: ReservationModel[]=[];
  public existingSeats: ReservationModel[]=[];
  public projection: ProjectionModel;
  public stage: StageModel;
  public rows: any;
  public columns: any;
  public theaterId : string;
  public stageId : number;
  public projectionId : any;
  public tempSeat: ReservationModel;

  constructor(private http: HttpClient, private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
   
  }

  getProjection() {
    return this.http.get<ProjectionModel>(environment.hostUrl+'/api/Projections/'+this.projectionId)
  }

  getStage(Id){
    return this.http.get<StageModel>(environment.hostUrl+'/api/Theaters/'+this.theaterId+'/Stages/'+ Id)
  }
  
  getExistingSeats(id){
    return this.http.get<ReservationModel[]>(environment.hostUrl+'/api/Theaters/projections/'+id+'/Seats' )
  }

  onAddSeat(row:number, column:number){
    var seat = this.mySeats.find( x=> x.SeatColumn == column.toString() && x.SeatRow == row.toString());
    
    var existing = this.existingSeats.find(x=> x.SeatColumn == column.toString() && x.SeatRow == row.toString());
    
    if ( seat == null && existing==null){
      seat = new ReservationModel(row.toString(),column.toString());
      console.log(seat);
      this.mySeats.push(seat);
    } else {
      var index = this.mySeats.indexOf(seat);
      this.mySeats.splice(index,1);
    }
  }

  onCheckState(row:number, column:number) {
    var seat = this.existingSeats.find( x=> x.SeatColumn == column.toString() && x.SeatRow == row.toString()); //OVO RADI PRE onInita
    
    if (seat == null || seat.State=="1" || seat.State=="2"){
      return true;
    } else return false;
  }



  onConfirmReservations(){
    this.http.post(environment.hostUrl +'/api/Theaters/'+this.theaterId+'/Projections/'+this.projectionId+'/ReserveSeats',this.mySeats)
    .subscribe(
      ()=>{
        if(this.mySeats==[]){
          alert('No seats selected');
        } else
          alert('Reservations confirmed');
      },
      ()=>alert('An error has occured!')
    )
  }

}
