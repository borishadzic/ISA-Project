import { Component, OnInit } from '@angular/core';

const navLinks = [
  { path: '/admin', label: 'Admin' }
]

@Component({
  selector: 'app-admin-navbar',
  templateUrl: './admin-navbar.component.html',
  styleUrls: ['./admin-navbar.component.css']
})
export class AdminNavbarComponent implements OnInit {

  navLinks = navLinks;

  constructor() { }

  ngOnInit() {
  }

}