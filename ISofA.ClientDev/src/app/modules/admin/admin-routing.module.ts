import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AdminHomeComponent } from './admin-home/admin-home.component';
import { ScheduleComponent } from './schedule/schedule.component';
import { ScheduleEditComponent } from './schedule-edit/schedule-edit.component';

const routes: Routes = [
  { path: 'admin', component: AdminHomeComponent },
  { path: 'admin/edit/schedule', component: ScheduleEditComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
