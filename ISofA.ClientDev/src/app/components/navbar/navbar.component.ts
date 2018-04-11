import { Component, OnInit, OnDestroy } from '@angular/core';

import { AuthService } from '../../services/auth.service';
import { Subscription } from 'rxjs/Subscription';

@Component({
  selector: 'app-navbar',
  template: `
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
    <a class="navbar-brand" routerLink="">ISofA</a>
    <button class="navbar-toggler" type="button"
      data-toggle="collapse" data-target="#navbarNavAltMarkup"
      aria-controls="navbarNavAltMarkup" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarNavAltMarkup">
        <div class="navbar-nav">
          <a class="nav-item nav-link" routerLink="/cinemas">Cinemas</a>
          <a class="nav-item nav-link" routerLink="/theaters">Theaters</a>
          <ng-container *ngIf="!isLogedIn; else template">
            <a class="nav-item nav-link" routerLink="/login">Login</a>
            <a class="nav-item nav-link" routerLink="/register">Register</a>
          </ng-container>
          <ng-template #template>
            <app-shopping-cart-icon></app-shopping-cart-icon>
          </ng-template>
        </div>
    </div>
  </nav>
  `
})
export class NavbarComponent implements OnInit, OnDestroy {

  isLogedIn = true;
  sub: Subscription;

  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.isLogedIn = this.authService.isLogedIn();

    this.sub = this.authService.logedInEvent.subscribe(
      (value) => {
        this.isLogedIn = value;
      }
    );
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

}
