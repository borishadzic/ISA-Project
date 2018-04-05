import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';

import { AuthService } from '../services/auth.service';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  exports: [
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule
  ]
})
export class SharedModule {
  static forRoot(): ModuleWithProviders  {
    return {
      ngModule: SharedModule,
      providers: [ AuthService ]
    };
  }
}
