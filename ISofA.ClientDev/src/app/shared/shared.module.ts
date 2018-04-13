import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';

import { AuthService } from '../services/auth.service';
import { ShoppingCartService } from '../services/shopping-cart.service';
import { MaterialModule } from './material/material.module';

import { TimeTableComponent } from './time-table/time-table.component';

var componentExports: any[] = [
  TimeTableComponent
];

@NgModule({
  imports: [    
    CommonModule,
    MaterialModule,
    ReactiveFormsModule,
    HttpClientModule    
  ],
  exports: [
    CommonModule,
    MaterialModule,
    ReactiveFormsModule,
    HttpClientModule,  
    componentExports    
  ],
  declarations: [TimeTableComponent]
})
export class SharedModule {
  static forRoot(): ModuleWithProviders  {
    return {
      ngModule: SharedModule,
      providers: [ AuthService, ShoppingCartService ]
    };
  }
}
