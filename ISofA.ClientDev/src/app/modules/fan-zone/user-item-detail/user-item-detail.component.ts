import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { combineLatest } from 'rxjs/observable/combineLatest';
import { timer } from 'rxjs/observable/timer';

import { BidService } from '../bid.service';
import { AuthService } from '../../../services/auth.service';
import { UserItemService } from '../user-item.service';

import { Bid } from '../model/bid';
import { UserItem } from '../model/user-item';

@Component({
  selector: 'app-user-item-detail',
  templateUrl: './user-item-detail.component.html',
  styleUrls: ['./user-item-detail.component.css']
})
export class UserItemDetailComponent implements OnInit {

  public error = false;
  public form: FormGroup;
  public userItem: UserItem;
  public isUserOwner = false;
  private theaterId: string;
  private userItemId: string;

  constructor(private userItemService: UserItemService,
              private authService: AuthService,
              private bidService: BidService,
              private router: Router,
              private route: ActivatedRoute,
              private fb: FormBuilder) { }

  ngOnInit() {
    const parentParams = this.route.parent.params;
    const childParams = this.route.params;

    combineLatest(parentParams, childParams).subscribe(([parent, child]) => {
      this.userItemId = child['uid'];

      this.userItemService.getItem(parent['id'], child['uid']).subscribe(userItem => {
        this.userItem = userItem;
        this.isUserOwner = userItem.UserId === this.authService.UserId;
        this.createFormGroup();
      });
    });
  }

  private createFormGroup(): void {
    this.form = this.fb.group({
      BidAmount: [this.userItem ? (this.userItem.HighestBid + 10) : null, [
        Validators.required,
        Validators.min(this.userItem && this.userItem.HighestBid ? (this.userItem.HighestBid + 10) : 10)
      ]]
    });
  }

  onBid() {
    if (this.form.valid) {
      this.bidService.bid(this.userItemId, this.form.value).subscribe(userItem => {
        this.userItem = userItem;
      }, () => {
        this.error = true;
        timer(3000).subscribe(() => this.error = false);
      });
    }
  }

  onSell(bid: Bid) {
    this.bidService.sellItem(this.userItemId, bid.BidderId).subscribe(() => {
      alert('Item sold!');
      this.router.navigate(['../'], { relativeTo: this.route });
    });
  }
}
