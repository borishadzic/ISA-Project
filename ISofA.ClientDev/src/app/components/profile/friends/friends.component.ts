import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { ProfileModel } from '../../../models/profile-model';

@Component({
  selector: 'app-friends',
  templateUrl: './friends.component.html',
  styleUrls: ['./friends.component.css']
})
export class FriendsComponent implements OnInit {

  public friends: ProfileModel[];
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.http.get<any>(environment.hostUrl + 'api/friends').subscribe((friends) => { this.friends = friends; } );
  }

}
