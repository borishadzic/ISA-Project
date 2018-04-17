import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminHomeComponent } from './admin-home/admin-home.component';
import { AdminNavbarComponent } from './admin-navbar/admin-navbar.component';
import { ScheduleComponent } from './schedule/schedule.component';
import { EditTheaterDialogComponent } from './edit-theater-dialog/edit-theater-dialog.component';
import { EditStageDialogComponent } from './edit-stage-dialog/edit-stage-dialog.component';
import { AddStageDialogComponent } from './add-stage-dialog/add-stage-dialog.component';
import { PlayListComponent } from './play-list/play-list.component';
import { StageListComponent } from './stage-list/stage-list.component';
import { AddPlayDialogComponent } from './add-play-dialog/add-play-dialog.component';
import { EditPlayDialogComponent } from './edit-play-dialog/edit-play-dialog.component';

@NgModule({
  imports: [
    SharedModule,
    AdminRoutingModule
  ],
  entryComponents: [
    EditTheaterDialogComponent,
    EditStageDialogComponent,
    AddStageDialogComponent,
    AddPlayDialogComponent,
    EditPlayDialogComponent
  ],
  declarations: [AdminHomeComponent, AdminNavbarComponent, ScheduleComponent, EditTheaterDialogComponent, EditStageDialogComponent, AddStageDialogComponent, PlayListComponent, StageListComponent, AddPlayDialogComponent, EditPlayDialogComponent]
})
export class AdminModule { }
