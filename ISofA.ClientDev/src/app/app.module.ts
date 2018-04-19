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
import { HomeModule } from './modules/home/home.module';
import { ShoppingCartComponent } from './components/shopping-cart/shopping-cart.component';
import { ShoppingCartIconComponent } from './components/shopping-cart-icon/shopping-cart-icon.component';
import { AdminModule } from './modules/admin/admin.module';
import { ProfileComponent } from './components/profile/profile.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    NavbarComponent,
    ShoppingCartComponent,
    ShoppingCartIconComponent,
    ProfileComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule.forRoot(),
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyBNuMGHVcGEkYjEoZQWiHZGJA03GS647Jw',
      libraries: ['places']
    }),
    HomeModule,
    TheaterModule,
    FanZoneModule,
    AdminModule
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
