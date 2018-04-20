import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { ProfileModel } from '../../../models/profile-model';
import { Observable } from 'rxjs/Observable';

export interface SelectUserDialogData {
  title: string;
  completeAction: string;
  emptyMsg: string;
  query(): Observable<ProfileModel[]>;
  queryThis: any;
}

@Component({
  selector: 'app-select-user-dialog',
  templateUrl: './select-user-dialog.component.html',
  styleUrls: ['./select-user-dialog.component.css']
})
export class SelectUserDialogComponent implements OnInit {

  nameFilter: string = "";

  allUsers: ProfileModel[];
  users: ProfileModel[];
  selectedUsers: ProfileModel[] = [];

  timeout: any = null;

  constructor(
    public dialogRef: MatDialogRef<SelectUserDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: SelectUserDialogData
  ) { }

  ngOnInit() {
      this.data.query.call(this.data.queryThis).subscribe(users => {
        this.allUsers = users;
        this.users = users.sort((a, b) => a.name < b.name ? -1 : a.name > b.name ? 1 : 0).map(x=>x);
      })
  }

  nameFilterChange($event: string) {
    this.nameFilter = $event;

    clearTimeout(this.timeout);

    this.timeout = setTimeout(() => {
      var nameFilter = this.nameFilter.toLowerCase().replace(/  +/g, ' ').trim();
      this.users = this.allUsers.filter(user => user.Name.toLowerCase().indexOf(nameFilter) != -1)
        .filter(user => this.selectedUsers.indexOf(user) == -1)
        .sort((a, b) => a.Name < b.Name ? -1 : a.Name > b.Name ? 1 : 0);
      this.timeout = null;
    }, 250);

  }

  selectUser(user: ProfileModel) {
    this.selectedUsers.push(...this.users.splice(this.users.indexOf(user), 1));
  }

  unselectUser(user: ProfileModel) {
    this.allUsers.push(...this.selectedUsers.splice(this.selectedUsers.indexOf(user), 1));
    var nameFilter = this.nameFilter.toLowerCase().replace(/  +/g, ' ').trim();
    this.users = this.allUsers.filter(user => user.Name.toLowerCase().indexOf(nameFilter) != -1)
      .filter(user => this.selectedUsers.indexOf(user) == -1)
      .sort((a, b) => a.Name < b.Name ? -1 : a.Name > b.Name ? 1 : 0);
  }

}
