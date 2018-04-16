import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminHomeComponent } from './admin-home/admin-home.component';
import { AdminNavbarComponent } from './admin-navbar/admin-navbar.component';
import { ScheduleComponent } from './schedule/schedule.component';
import { StagesComponent } from './stages/stages.component';
import { EditTheaterDialogComponent } from './edit-theater-dialog/edit-theater-dialog.component';
import { EditStageDialogComponent } from './edit-stage-dialog/edit-stage-dialog.component';
import { AddStageDialogComponent } from './add-stage-dialog/add-stage-dialog.component';
import { PlayListComponent } from './play-list/play-list.component';
import { TheaterAdminService } from './theater-admin.service';

@NgModule({
  imports: [
    SharedModule,
    AdminRoutingModule
  ],
  entryComponents: [
    EditTheaterDialogComponent,
    EditStageDialogComponent,
    AddStageDialogComponent
  ],
  declarations: [AdminHomeComponent, AdminNavbarComponent, ScheduleComponent, StagesComponent, EditTheaterDialogComponent, EditStageDialogComponent, AddStageDialogComponent, PlayListComponent],
  providers: [
    TheaterAdminService
  ]
})
export class AdminModule { }
