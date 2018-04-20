import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './shared/shared.module';
import { TheaterModule } from './modules/theater/theater.module';
import { FanZoneModule } from './modules/fan-zone/fan-zone.module';
import { UserModule } from './modules/user.module'

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
import { AdminDialogComponent } from './components/admin-dialog/admin-dialog.component';
import { AdminTheaterModule } from './modules/admin-theater/admin-theater.module';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    NavbarComponent,
    ShoppingCartComponent,
    ShoppingCartIconComponent,
    ProfileComponent,
    AdminDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule.forRoot(),
    HomeModule,
    TheaterModule,
    FanZoneModule,
    AdminModule,
    AdminTheaterModule,
    UserModule
  ],
  entryComponents: [
    AdminDialogComponent
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
