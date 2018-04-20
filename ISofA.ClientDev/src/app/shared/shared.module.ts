import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

import { AuthService } from '../services/auth.service';
import { ShoppingCartService } from '../services/shopping-cart.service';
import { MaterialModule } from './material/material.module';

import { TimeTableComponent } from './time-table/time-table.component';
import { MinsFormatterPipe } from './mins-formatter.pipe';
import { RangePipe } from './range.pipe';
import { PadPipe } from './pad.pipe';
import { StageService } from '../services/stage.service';
import { PlayService } from '../services/play.service';
import { NameFilterPipe } from './name-filter.pipe';
import { ProjectionService } from '../services/projection.service';
import { MapDialogComponent } from './map-dialog/map-dialog.component';
import { AgmCoreModule } from '@agm/core';
import { TemplateNavbarComponent } from './template-navbar/template-navbar.component';
import { RouterModule } from '@angular/router';
import { TheaterContainerService } from '../services/theater-container.service';
import { LevelRequirementsService } from '../services/level-requirements.service';
import { TicketDiscountService } from '../services/ticket-discount.service';

let componentExports: any[] = [
  TimeTableComponent,
  TemplateNavbarComponent
];

let pipeExports: any[] = [
  MinsFormatterPipe,
  PadPipe,
  RangePipe,
  NameFilterPipe
];

@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule, // TODO: Not sure if need
    ReactiveFormsModule,
    RouterModule,
    HttpClientModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyBNuMGHVcGEkYjEoZQWiHZGJA03GS647Jw',
      libraries: ['places']
    }),
  ],
  exports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AgmCoreModule,
    componentExports,
    pipeExports
  ],
  entryComponents: [
    MapDialogComponent
  ],
  declarations: [TimeTableComponent, MinsFormatterPipe, RangePipe, PadPipe, NameFilterPipe, MapDialogComponent, TemplateNavbarComponent]
})
export class SharedModule {
  static forRoot(): ModuleWithProviders {
    return {
      ngModule: SharedModule,
      providers: [AuthService,
        ShoppingCartService,
        StageService, PlayService, ProjectionService, TheaterContainerService, TicketDiscountService, LevelRequirementsService]
    };
  }
}
