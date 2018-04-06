import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { UserItemService } from '../user-item.service';
import { UserItem } from '../model/user-item';
import { nullSafeIsEquivalent } from '@angular/compiler/src/output/output_ast';
import { BidService } from '../bid.service';

@Component({
  selector: 'app-user-item-detail',
  templateUrl: './user-item-detail.component.html',
  styleUrls: ['./user-item-detail.component.css']
})
export class UserItemDetailComponent implements OnInit {

  form: FormGroup;
  userItem: UserItem;
  private userItemId: string;

  constructor(private userItemService: UserItemService,
              private bidService: BidService,
              private route: ActivatedRoute,
              private fb: FormBuilder) { }

  ngOnInit() {
    this.userItemId = this.route.snapshot.params['uid'];

    this.userItemService.getItem(this.userItemId).subscribe(userItem => {
      this.userItem = userItem;
      this.createFormGroup();
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
      });
    }
  }
}
