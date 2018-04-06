import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './shared/shared.module';
import { TheaterModule } from './modules/theater/theater.module';
import { FanZoneModule } from './modules/fan-zone/fan-zone.module';
import { AgmCoreModule } from '@agm/core';

import { TokenInterceptor } from './services/token.interceptor';

import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    NavbarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule.forRoot(),
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyBNuMGHVcGEkYjEoZQWiHZGJA03GS647Jw',
      libraries: ['places']
    }),
    TheaterModule,
    FanZoneModule
  ],
  providers: [
    [{
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }]
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
