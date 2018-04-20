import { Component, OnInit, AfterViewInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Router, ActivatedRoute, Params } from '@angular/router'

import { ReservationModel } from '../../../models/reservation-model';
import { StageModel } from '../../../models/stage-model';
import { ProjectionModel } from '../../../models/projection-model';
import { ProfileModel } from '../../../models/profile-model';
import { Observable } from 'rxjs/Observable';
import { MatDialog } from '@angular/material';
import { SelectUserDialogData, SelectUserDialogComponent } from '../select-user-dialog/select-user-dialog.component';
import { InviteBindingModel } from '../../../models/invite-binding-model';


@Component({
  selector: 'app-reservation',
  templateUrl: './reservation.component.html',
  styleUrls: ['./reservation.component.css']
})
export class ReservationComponent implements OnInit, AfterViewInit {
  ngAfterViewInit(): void {
    this.activatedRoute.params.subscribe(
      (params: Params) => {
        console.log(params);
        this.projectionId = params['projectionId'];
        this.getProjection().subscribe(
          (proj) => {
            if (proj == null) {
              alert('No such projection');
              return
            }
            this.projection = proj;
            this.theaterId = proj.TheaterId.toString();
            this.getExistingSeats(proj.ProjectionId).subscribe(
              (existing) => {
                this.existingSeats = existing;
              }
            );
            this.getStage(proj.StageId).subscribe(
              (stage) => {
                this.stage = stage;
                this.rows = Array(stage.SeatRows).fill(0).map((x, i) => i);
                this.columns = Array(stage.SeatColumns).fill(0).map((x, i) => i);

              }
            );
          }
        )
      }
    );
  }
  public mySeats: ReservationModel[] = [];
  public existingSeats: ReservationModel[] = [];
  public projection: ProjectionModel;
  public stage: StageModel;
  public rows: any;
  public columns: any;
  public theaterId: string;
  public stageId: number;
  public projectionId: any;
  public tempSeat: ReservationModel;

  constructor(private http: HttpClient, private activatedRoute: ActivatedRoute, private dialog: MatDialog) { }

  ngOnInit() {

  }

  getProjection() {
    return this.http.get<ProjectionModel>(environment.hostUrl + '/api/Projections/' + this.projectionId)
  }

  getStage(Id) {
    return this.http.get<StageModel>(environment.hostUrl + '/api/Theaters/' + this.theaterId + '/Stages/' + Id)
  }

  getExistingSeats(id) {
    return this.http.get<ReservationModel[]>(environment.hostUrl + '/api/Theaters/projections/' + id + '/Seats')
  }

  onAddSeat(row: number, column: number) {
    var seat = this.mySeats.find(x => x.SeatColumn == column.toString() && x.SeatRow == row.toString());

    var existing = this.existingSeats.find(x => x.SeatColumn == column.toString() && x.SeatRow == row.toString());

    if (seat == null && existing == null) {
      seat = new ReservationModel(row.toString(), column.toString());
      console.log(seat);
      this.mySeats.push(seat);
    } else {
      var index = this.mySeats.indexOf(seat);
      this.mySeats.splice(index, 1);
    }
    console.log(this.mySeats.length);
  }

  onCheckState(row: number, column: number) {
    var seat = this.existingSeats.find(x => x.SeatColumn == column.toString() && x.SeatRow == row.toString()); //OVO RADI PRE onInita

    if (seat == null) {
      return true;
    } else return false;
  }

  onSendInvitesToFriends() {
    let dialogRef = this.dialog.open(SelectUserDialogComponent, {
      data: {
        completeAction: "Invite",
        emptyMsg: "You don't have any friends.",
        title: "invite friends",
        query: this.getMyFriends,
        queryThis: this
      } as SelectUserDialogData
    })

    dialogRef.afterClosed().subscribe((friends: ProfileModel[])=>{
      console.log(friends);
      console.log(friends.length);
      this.sendIvites(friends);
    })
  }

  sendIvites(users: ProfileModel[]){
    if (users.length > (this.mySeats.length-1)){
      console.log(this.mySeats.length-1);
      alert('Cant invite more friends then there are seats!');
      return;
    }
    var rows1:number[]=[];
    var columns1:number[]=[];
    var projectionIds1:number[]=[];
    for (var i =0; i< users.length; i++){
      rows1.push(Number.parseInt(this.mySeats[i].SeatRow));
      columns1.push(Number.parseInt(this.mySeats[i].SeatColumn));
      projectionIds1.push(this.projectionId);
    }
    var invites = new InviteBindingModel;
    invites.users=users;
    invites.projectionIds=projectionIds1;
    invites.rows=rows1;
    invites.columns=columns1;
    this.http.post(environment.hostUrl+'/api/Seats/SendInvatations',invites).subscribe(
      () => {
        alert('Invites sent');
        this.onConfirmReservations();
      }
    )
  }

  getMyFriends(): Observable<ProfileModel[]> {
    return this.http.get<ProfileModel[]>(environment.hostUrl + '/api/FriendRequests/GetMyFriends');
  }

  onConfirmReservations() {
    if (this.mySeats.length == 0) {
      alert('No seats selected');
      return;
    }
    this.http.post(environment.hostUrl + '/api/Theaters/' + this.theaterId + '/Projections/' + this.projectionId + '/ReserveSeats', this.mySeats)
      .subscribe(
        () => {
          alert('Reservations confirmed');
        },
        () => alert('An error has occured!')
      )
  }

}
