import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ProfileModel } from '../../models/profile-model';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  public User: ProfileModel;
  public friends: ProfileModel[];
  public show = false;
  public message = 'Show Friends';
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.http.get<ProfileModel>(environment.hostUrl + '/api/myProfile').subscribe((user) => {this.User = user; }, (error) => alert(error));
    this.http.get<any>(environment.hostUrl + '/api/FriendRequests/GetMyFriends').subscribe((friends) => { this.friends = friends; } );
    console.log(this.friends);
  }

  onShowFriends() {
    if (this.message === 'Show Friends') {
      this.message = 'Hide Friends';
    } else {
      this.message = 'Show Friends';
    }
    this.show = !this.show;
  }

}
