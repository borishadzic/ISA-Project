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

var componentExports: any[] = [
  TimeTableComponent
];

var pipeExports: any[] = [
  MinsFormatterPipe,
  PadPipe,
  RangePipe,
  NameFilterPipe
]

@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule, // TODO: Not sure if need
    ReactiveFormsModule,
    HttpClientModule
  ],
  exports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    componentExports,
    pipeExports
  ],
  declarations: [TimeTableComponent, MinsFormatterPipe, RangePipe, PadPipe, NameFilterPipe]
})
export class SharedModule {
  static forRoot(): ModuleWithProviders {
    return {
      ngModule: SharedModule,
      providers: [AuthService, ShoppingCartService, StageService, PlayService]
    };
  }
}
