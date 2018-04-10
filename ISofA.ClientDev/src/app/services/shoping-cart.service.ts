import { Injectable } from '@angular/core';

import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Item } from '../modules/fan-zone/model/item';

@Injectable()
export class ShopingCartService {

  private items: Item[] = [];
  private numberOfItems: BehaviorSubject<number>;

  constructor() {
    this.numberOfItems = new BehaviorSubject(0);
  }

  addItem(item: Item) {
    this.items.push(item);
    this.numberOfItems.next(this.items.length);
  }

  removeItem(item: Item) {
    const index = this.items.indexOf(item);

    if (index !== -1) {
      this.items.splice(index, 1);
      this.numberOfItems.next(this.items.length);
    }
  }

  get NumberOfItems(): BehaviorSubject<number> {
    return this.numberOfItems;
  }

}
