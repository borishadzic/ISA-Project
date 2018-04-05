import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';

import { FanZoneRoutingModule } from './fan-zone-routing.module';
import { ItemListComponent } from './item-list/item-list.component';

@NgModule({
  imports: [
    SharedModule,
    FanZoneRoutingModule
  ],
  declarations: [ItemListComponent]
})
export class FanZoneModule { }
