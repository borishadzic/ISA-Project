import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ProfileModel } from '../../models/profile-model';
import { ReservationModel } from '../../models/reservation-model';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  public User: ProfileModel;
  public friends: ProfileModel[];
  public searchResults: ProfileModel[];
  public myReservations: ReservationModel[];
  public show = false;
  public showResults = false;
  public message = 'Show Friends';
  public friendRequests: ProfileModel[];
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.http.get<ProfileModel>(environment.hostUrl + '/api/Users/myProfile').subscribe((user) => {this.User = user; }, (error) => alert(error));
    this.onGetMyFrieds();
    this.onGetMyReservations();
    this.http.get<ProfileModel[]>(environment.hostUrl + '/api/FriendRequests/GetFriendRequests').subscribe((requests) => {this.friendRequests=requests;}, (error)=> alert(error));
  }

  onGetMyFrieds(){
    this.http.get<any>(environment.hostUrl + '/api/FriendRequests/GetMyFriends').subscribe((friends) => { this.friends = friends; } );
    
  }

  onShowFriends() {
    if (this.message === 'Show Friends') {
      this.message = 'Hide Friends';
    } else {
      this.message = 'Show Friends';
    }
    this.show = !this.show;
  }

  onSearch(){
    this.http.post<ProfileModel[]>(environment.hostUrl+'/api/Users/SearchAll', {
      "Name": (document.getElementById("name") as HTMLInputElement).value
    }).subscribe(
      (users)=> {
        this.searchResults = users;
        this.showResults = true;
      }
    )
  }

  onAddFriend(user: ProfileModel){
    this.http.post(environment.hostUrl + '/api/FriendRequests/sendFriendRequest',
      user
    ).subscribe(
      () => {
        alert("Friend Request Sent");
        var index = this.searchResults.indexOf(user,0);
        this.searchResults.splice(index,1);
      },
      () => {
        alert("An error has accured")
      }
    )
  }

  onConfirmRequest(user: ProfileModel){
    this.http.post(environment.hostUrl+'/api/FriendRequests/accept',user).subscribe(
      ()=> {
        alert('Confirmed');
        this.onGetMyFrieds();
        var index = this.friendRequests.indexOf(user,0);
        this.friendRequests.splice(index,1);
      }
    );
  }

  onDenyRequest(user: ProfileModel){
    this.http.post(environment.hostUrl+'/api/FriendRequests/decline',user).subscribe(
      ()=> {
        alert('Declined');
        var index = this.friendRequests.indexOf(user,0);
        this.friendRequests.splice(index,1);
      }
    );
  }

  onRemoveFriend(user: ProfileModel){
    this.http.post(environment.hostUrl +'/api/friendrequests/removeFriend',user)
    .subscribe(()=>this.onGetMyFrieds());
  }

  onGetMyReservations(){
    this.http.get<ReservationModel[]>(environment.hostUrl+'/api/Profile/myReservations').subscribe(
      (reservations)=> {
        this.myReservations=reservations;
      }
    )
  }

}
