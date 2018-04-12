import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-fan-zone',
  templateUrl: './fan-zone.component.html'
})
export class FanZoneComponent implements OnInit {

  isAuthorized = false;

  constructor(private authService: AuthService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.isAuthorized = this.authService.hasAccess(params['id'], 'FanZoneAdmin');
    });
  }

}
