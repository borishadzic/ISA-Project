import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module'

import { UserRoutingModule } from './user-routing.module';
import { ReservationComponent } from './user/reservation/reservation.component'

@NgModule({
  imports: [
    CommonModule,
    UserRoutingModule,
    SharedModule
  ],
  declarations: [ReservationComponent]
})
export class UserModule { }
