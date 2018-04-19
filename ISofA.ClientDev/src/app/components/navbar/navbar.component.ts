import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material';

import { AuthService } from '../../services/auth.service';
import { Subscription } from 'rxjs/Subscription';
import { AdminDialogComponent } from '../admin-dialog/admin-dialog.component';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html'
})
export class NavbarComponent implements OnInit, OnDestroy {

  isLogedIn = false;
  isAdmin = false;
  sub: Subscription;

  constructor(private authService: AuthService, private router: Router, public dialog: MatDialog) { }

  ngOnInit() {
    this.isLogedIn = this.authService.isLogedIn();
    this.isAdmin = this.authService.isAdmin;

    console.log('Nesto', this.isAdmin);

    this.sub = this.authService.logedInEvent.subscribe(
      (value) => {
        this.isLogedIn = value;
        this.isAdmin = this.authService.isAdmin;
      }
    );
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  onOpenDialog() {
    this.dialog.open(AdminDialogComponent);
  }

  onLogout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

}
