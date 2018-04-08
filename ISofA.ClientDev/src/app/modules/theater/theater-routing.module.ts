import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { TheaterNewComponent } from './theater-new/theater-new.component';
import { AdminRegistrationComponent } from './admin-registration/admin-registration.component';

const routes: Routes = [
  { path: 'theaters/new', component: TheaterNewComponent },
  { path: 'theaters/:id/register', component: AdminRegistrationComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TheaterRoutingModule { }
