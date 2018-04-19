import { Component, OnInit, Input } from '@angular/core';
import { NavLinkData } from './template-navbar-data';

@Component({
  selector: 'app-template-navbar',
  templateUrl: './template-navbar.component.html',
  styleUrls: ['./template-navbar.component.css']
})
export class TemplateNavbarComponent implements OnInit {

  @Input() navLinkBase: string;
  @Input() navLinks: NavLinkData[];

  constructor() { }

  ngOnInit() {
  }

}
