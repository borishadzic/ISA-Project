import { Component, OnInit } from '@angular/core';

import { UserItemService } from '../user-item.service';
import { UserItem } from '../model/user-item';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-submited-items',
  templateUrl: './submited-items.component.html',
  styleUrls: ['./submited-items.component.css']
})
export class SubmitedItemsComponent implements OnInit {

  public userItems: Observable<UserItem[]>;

  constructor(private userItemService: UserItemService) { }

  ngOnInit() {
    this.userItems = this.userItemService.getUserItems();
  }

}
