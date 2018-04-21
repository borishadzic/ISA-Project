import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { TheaterNewComponent } from './theater-new/theater-new.component';
import { AdminRegistrationComponent } from './admin-registration/admin-registration.component';
import { TheaterListComponent } from './theater-list/theater-list.component';
import { TheaterDetailComponent } from './theater-detail/theater-detail.component';
import { TheaterRepertoireComponent } from './theater-repertoire/theater-repertoire.component';
import { DiscountTicketListComponent } from './discount-ticket-list/discount-ticket-list.component';

const routes: Routes = [
  { path: 'theaters', component: TheaterListComponent },
  { path: 'cinemas', component: TheaterListComponent },
  { path: 'theaters/new', component: TheaterNewComponent, data: { type: 'Theater'} },
  { path: 'cinemas/new', component: TheaterNewComponent, data: { type: 'Cinema'} },
  { path: 'theaters/:id', component: TheaterDetailComponent },
  { path: 'cinemas/:id', component: TheaterDetailComponent },
  { path: 'theaters/:id/repertoire', component: TheaterRepertoireComponent },
  { path: 'cinemas/:id/repertoire', component: TheaterRepertoireComponent },
  { path: 'theaters/:id/discount-tickets', component: DiscountTicketListComponent },
  { path: 'cinemas/:id/discount-tickets', component: DiscountTicketListComponent },
  { path: 'theaters/:id/register', component: AdminRegistrationComponent },
  { path: 'cinemas/:id/register', component: AdminRegistrationComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TheaterRoutingModule { }
