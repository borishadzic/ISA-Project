import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { AgmCoreModule } from '@agm/core';

import { TheaterRoutingModule } from './theater-routing.module';
import { TheaterNewComponent } from './theater-new/theater-new.component';
import { TheaterService } from './theater.service';
import { AdminRegistrationComponent } from './admin-registration/admin-registration.component';

@NgModule({
  imports: [
    SharedModule,
    AgmCoreModule,
    TheaterRoutingModule
  ],
  declarations: [
    TheaterNewComponent,
    AdminRegistrationComponent
  ],
  providers: [
    TheaterService
  ]
})
export class TheaterModule { }
