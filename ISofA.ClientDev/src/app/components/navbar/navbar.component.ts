import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from '../../services/auth.service';
import { Subscription } from 'rxjs/Subscription';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html'
})
export class NavbarComponent implements OnInit, OnDestroy {

  isLogedIn = false;
  sub: Subscription;

  constructor(private authService: AuthService, private router: Router) { }

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

  onLogout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

}
