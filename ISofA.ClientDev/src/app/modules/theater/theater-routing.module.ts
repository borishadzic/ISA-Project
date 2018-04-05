import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TheaterNewComponent } from './theater-new/theater-new.component';

const routes: Routes = [
  { path: 'theaters/new', component: TheaterNewComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TheaterRoutingModule { }
