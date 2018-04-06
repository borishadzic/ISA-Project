import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { FanZoneRoutingModule } from './fan-zone-routing.module';

import { ItemService } from './item.service';
import { UserItemService } from './user-item.service';
import { BidService } from './bid.service';

import { FanZoneComponent } from './fan-zone.component';
import { ItemListComponent } from './item-list/item-list.component';
import { ItemNewComponent } from './item-new/item-new.component';
import { UserItemListComponent } from './user-item-list/user-item-list.component';
import { UserItemDetailComponent } from './user-item-detail/user-item-detail.component';
import { UserItemNewComponent } from './user-item-new/user-item-new.component';
import { UserItemAwaitingComponent } from './user-item-awaiting/user-item-awaiting.component';

@NgModule({
  imports: [
    SharedModule,
    FanZoneRoutingModule
  ],
  declarations: [
    FanZoneComponent,
    ItemListComponent,
    ItemNewComponent,
    UserItemListComponent,
    UserItemDetailComponent,
    UserItemNewComponent,
    UserItemAwaitingComponent
  ],
  providers: [
    ItemService,
    UserItemService,
    BidService
  ]
})
export class FanZoneModule { }
