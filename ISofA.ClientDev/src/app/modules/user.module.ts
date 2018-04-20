import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module'

import { UserRoutingModule } from './user-routing.module';
import { ReservationComponent } from './user/reservation/reservation.component'
import { SelectUserDialogComponent } from './user/select-user-dialog/select-user-dialog.component';


@NgModule({
  imports: [
    CommonModule,
    UserRoutingModule,
    SharedModule
  ],
  declarations: [ReservationComponent, SelectUserDialogComponent],
  entryComponents: [
    SelectUserDialogComponent
  ]
})
export class UserModule { }
