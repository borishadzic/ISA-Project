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
  public searchResults: ProfileModel[];
  public show = false;
  public showResults = false;
  public message = 'Show Friends';
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.http.get<ProfileModel>(environment.hostUrl + '/api/Users/myProfile').subscribe((user) => {this.User = user; }, (error) => alert(error));
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
      () => alert("Friend Request Sent"),
      () => alert("An error has accured")
    )
  }
}
