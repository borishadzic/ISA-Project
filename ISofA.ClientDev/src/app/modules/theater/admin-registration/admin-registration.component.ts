import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, Validators, FormGroup, FormControl } from '@angular/forms';

import { TheaterService } from '../theater.service';

@Component({
  selector: 'app-admin-registration',
  templateUrl: './admin-registration.component.html',
  styleUrls: ['./admin-registration.component.css']
})
export class AdminRegistrationComponent implements OnInit {

  public form: FormGroup;
  public adminTypes: any[] = [{display: 'Admin', value: 0}, {display: 'FanZone admin', value: 1}];
  private theaterId: string;

  constructor(private theaterService: TheaterService,
              private route: ActivatedRoute,
              private fb: FormBuilder) { }

  ngOnInit() {
    this.form = this.fb.group({
      Name: ['', Validators.required],
      Surname: ['', Validators.required],
      Email: ['', [Validators.required, Validators.email]],
      City: ['', Validators.required],
      PhoneNumber: ['', Validators.required],
      AdminType: [this.adminTypes[0].value, Validators.required]
    });

    this.route.params.subscribe(param => {
      this.theaterId = param['id'];
    });
  }

  onRegister() {
    if (this.form.valid) {
      this.theaterService.addTheaterAdmin(this.theaterId, this.form.value).subscribe(() => {
        alert('Admin created');
        this.form.reset();
      });
    }
  }

}
