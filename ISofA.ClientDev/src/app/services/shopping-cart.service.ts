import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Observable } from 'rxjs/Observable';
import { tap, filter } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { Item } from '../modules/fan-zone/model/item';

@Injectable()
export class ShoppingCartService {

  private items: Item[] = [];
  private sum = 0;
  private numberOfItems: BehaviorSubject<number>;

  constructor(private http: HttpClient) {
    this.load();
    this.numberOfItems = new BehaviorSubject(this.items.length);
  }

  private load() {
    const sessionItems = sessionStorage.getItem('shoppingCart');

    if (sessionItems) {
      this.items = JSON.parse(sessionItems);
      this.items.forEach(item => this.sum += item.Price);
    } else {
      this.items = [];
    }
  }

  private save() {
    sessionStorage.setItem('shoppingCart', JSON.stringify(this.items));
  }

  private notifyAndSave() {
    this.numberOfItems.next(this.items.length);
    this.save();
  }

  private clear() {
    sessionStorage.removeItem('shoppingCart');
    this.items.length = 0;
    this.sum = 0;
    this.numberOfItems.next(0);
  }

  isInCart(itemId: string): boolean {
    for (const i of this.items) {
      if (i.ItemId === itemId) {
        return true;
      }
    }

    return false;
  }

  checkout(): Observable<any> {
    return this.http.post(environment.hostUrl + '/api/items', this.items)
      .pipe(
        tap(() => this.clear())
      );
  }

  addItem(item: Item) {
    if (!this.isInCart(item.ItemId)) {
      this.items.push(item);
      this.sum += item.Price;
      this.notifyAndSave();
    }
  }

  removeItem(item: Item) {
    const index = this.items.indexOf(item);

    if (index !== -1) {
      this.sum -= item.Price;
      this.items.splice(index, 1);
      this.notifyAndSave();
    }
  }

  get Sum(): number {
    return this.sum;
  }

  get Items(): Item[] {
    return this.items;
  }

  get NumberOfItems(): BehaviorSubject<number> {
    return this.numberOfItems;
  }

}
