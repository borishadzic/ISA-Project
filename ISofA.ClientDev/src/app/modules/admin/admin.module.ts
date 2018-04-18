import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminHomeComponent } from './admin-home/admin-home.component';
import { AdminNavbarComponent } from './admin-navbar/admin-navbar.component';
import { ScheduleComponent } from './schedule/schedule.component';
import { TheaterEditDialogComponent } from './theater-edit-dialog/theater-edit-dialog.component';
import { StageEditDialogComponent } from './stage-edit-dialog/stage-edit-dialog.component';
import { StageAddDialogComponent } from './stage-add-dialog/stage-add-dialog.component';
import { PlayListComponent } from './play-list/play-list.component';
import { StageListComponent } from './stage-list/stage-list.component';
import { PlayAddDialogComponent } from './play-add-dialog/play-add-dialog.component';
import { PlayEditDialogComponent } from './play-edit-dialog/play-edit-dialog.component'
import { ScheduleEditComponent } from './schedule-edit/schedule-edit.component';

@NgModule({
  imports: [
    SharedModule,
    AdminRoutingModule
  ],
  entryComponents: [
    TheaterEditDialogComponent,
    StageEditDialogComponent,
    StageAddDialogComponent,
    PlayAddDialogComponent,
    PlayEditDialogComponent
  ],
  declarations: [AdminHomeComponent, AdminNavbarComponent, ScheduleComponent, TheaterEditDialogComponent, StageEditDialogComponent, StageAddDialogComponent, PlayListComponent, StageListComponent, PlayAddDialogComponent, PlayEditDialogComponent, ScheduleEditComponent]
})
export class AdminModule { }
