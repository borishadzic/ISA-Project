import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/Observable';

import { UserItem } from '../model/user-item';
import { UserItemService } from '../user-item.service';

@Component({
  selector: 'app-user-item-list',
  templateUrl: './user-item-list.component.html',
  styleUrls: ['./user-item-list.component.css']
})
export class UserItemListComponent implements OnInit {

  public userItems: Observable<UserItem[]>;

  constructor(private userItemService: UserItemService,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.parent.params.subscribe(params => {
      this.userItems = this.userItemService.getItems(params['id']);
    });
  }

}
