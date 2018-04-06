import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ItemListComponent } from './item-list/item-list.component';
import { ItemNewComponent } from './item-new/item-new.component';
import { UserItemListComponent } from './user-item-list/user-item-list.component';
import { UserItemAwaitingComponent } from './user-item-awaiting/user-item-awaiting.component';
import { UserItemDetailComponent } from './user-item-detail/user-item-detail.component';
import { UserItemNewComponent } from './user-item-new/user-item-new.component';
import { FanZoneComponent } from './fan-zone.component';

const routes: Routes = [
  { path: 'theaters/:id/fanzone', component: FanZoneComponent, children: [
    { path: '', component: ItemListComponent },
    { path: 'new', component: ItemNewComponent },
    { path: 'useritems', component: UserItemListComponent },
    { path: 'useritems/new', component: UserItemNewComponent },
    { path: 'awaiting', component: UserItemAwaitingComponent },
    { path: 'useritems/:uid', component: UserItemDetailComponent },
  ]},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FanZoneRoutingModule { }
