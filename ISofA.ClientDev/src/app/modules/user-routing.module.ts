import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {ReservationComponent} from './user/reservation/reservation.component'

const routes: Routes = [
  { path: 'reservations/:projectionId', component: ReservationComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
