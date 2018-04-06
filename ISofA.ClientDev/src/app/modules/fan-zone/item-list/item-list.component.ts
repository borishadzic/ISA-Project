import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { ItemService } from '../item.service';
import { Item } from '../model/item';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-item-list',
  templateUrl: './item-list.component.html',
  styleUrls: ['./item-list.component.css']
})
export class ItemListComponent implements OnInit {

  items: Observable<Item[]>;

  constructor(private itemService: ItemService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.items = this.itemService.getItems(params['id']);
    });
  }

}
