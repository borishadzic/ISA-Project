import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ProfileModel } from '../../models/profile-model';
import { ReservationModel } from '../../models/reservation-model';
import { AuthService } from '../../services/auth.service';
import { ChangePasswordModel } from '../../models/change-password-model';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router'
import { LoginComponent } from '../login/login.component';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  public hideModal = false;
  public form: FormGroup;
  public User: ProfileModel;
  public friends: ProfileModel[];
  public searchResults: ProfileModel[];
  public myReservations: ReservationModel[];
  public show = false;
  public show2 = false;
  public showResults = false;
  public message = 'Show Friends';
  public message2 = 'Show Reservations';
  public friendRequests: ProfileModel[];
  constructor(private http: HttpClient, private authService: AuthService, private fb: FormBuilder, private router: Router ) { 
    
  }

  ngOnInit() {
    this.http.get<ProfileModel>(environment.hostUrl + '/api/Users/myProfile').subscribe((user) => {this.User = user; }, (error) => alert(error));
    this.onGetMyFrieds();
    this.onGetMyReservations();
    this.http.get<ProfileModel[]>(environment.hostUrl + '/api/FriendRequests/GetFriendRequests').subscribe((requests) => {this.friendRequests=requests;}, (error)=> alert(error));
    this.form = this.fb.group({
      'OldPassword': ['', [Validators.required]],
      'NewPassword':['',Validators.required],
      'ConfirmPassword':['',Validators.required],
    }, {
      validator:this.checkIfEqual
    });
  }

  checkIfEqual(group : FormGroup){
    const pass = group.controls.NewPassword.value;
    const confirmPass = group.controls.ConfirmPassword.value;

    return pass===confirmPass? null:{notSame:true}; 
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

  onShowReservations(){
    if (this.message2 === 'Show Reservations'){
      this.message2 = 'Hide Reservations';
    } else {
      this.message2 = 'Show Reservations';
    }
    this.show2= !this.show2;
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

  changeUserDetails(user: ProfileModel){

  }

  changePassword(){
    console.log(this.form.value);
    this.authService.changePassword(this.form.value).subscribe(
      () => {
        this.hideModal=true;
        this.authService.logout();
        this.router.navigate(['/login']);
      },
      () => {
        alert('password incorect!');
        return;
      }
    )
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
