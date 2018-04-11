import { Component, OnInit } from '@angular/core';

import { ShoppingCartService } from '../../services/shopping-cart.service';
import { Item } from '../../modules/fan-zone/model/item';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent implements OnInit {

  public items: Item[];
  public sum: number;

  constructor(private shoppingCartService: ShoppingCartService) { }

  ngOnInit() {
    this.items = this.shoppingCartService.Items;
    this.sum = this.shoppingCartService.Sum;
  }


  onCheckout() {
    this.shoppingCartService.checkout().subscribe(() => {
      this.items = [];
      this.sum = 0;
    });
  }

  onRemoveFromCart(item) {
    this.shoppingCartService.removeItem(item);
    this.sum = this.shoppingCartService.Sum;
  }

}
