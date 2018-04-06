import { Component, OnInit } from '@angular/core';
import { UserItemService } from '../user-item.service';
import { ActivatedRoute } from '@angular/router';
import { UserItem } from '../model/user-item';

@Component({
  selector: 'app-user-item-awaiting',
  templateUrl: './user-item-awaiting.component.html',
  styleUrls: ['./user-item-awaiting.component.css']
})
export class UserItemAwaitingComponent implements OnInit {

  private theaterId: number;
  public userItems: UserItem[];

  constructor(private userItemService: UserItemService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.theaterId = this.route.snapshot.parent.params['id'];

    this.userItemService.getAwaitingItems(this.theaterId).subscribe(userItems => {
      this.userItems = userItems;
    });
  }

  onApprove(userItemId: string, index: number) {
    this.userItemService.approveItem(this.theaterId, userItemId).subscribe(() => {
      this.userItems.splice(index, 1);
      alert('Item approved!');
    });
  }

  onDecline(userItemId: string, index: number) {
    this.userItemService.declineItem(this.theaterId, userItemId).subscribe(() => {
      this.userItems.splice(index, 1);
      alert('Item declined!');
    });
  }

}
