import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { AgmCoreModule } from '@agm/core';

import { TheaterRoutingModule } from './theater-routing.module';
import { TheaterNewComponent } from './theater-new/theater-new.component';
import { TheaterService } from './theater.service';
import { AdminRegistrationComponent } from './admin-registration/admin-registration.component';
import { TheaterListComponent } from './theater-list/theater-list.component';
import { TheaterDetailComponent } from './theater-detail/theater-detail.component';
import { TheaterContainerService } from '../../services/theater-container.service';
import { TheaterRepertoireComponent } from './theater-repertoire/theater-repertoire.component';

@NgModule({
  imports: [
    SharedModule,
    AgmCoreModule,
    TheaterRoutingModule
  ],
  declarations: [
    TheaterNewComponent,
    AdminRegistrationComponent,
    TheaterListComponent,
    TheaterDetailComponent,
    TheaterRepertoireComponent
  ],
  providers: [
    TheaterService
  ]
})
export class TheaterModule { }
