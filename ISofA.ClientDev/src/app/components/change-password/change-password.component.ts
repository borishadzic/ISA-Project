import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {

  public form: FormGroup;
  private token: string;

  constructor(private router: Router,
              private route: ActivatedRoute,
              private fb: FormBuilder,
              private http: HttpClient) { }

  ngOnInit() {
    this.token = this.route.snapshot.queryParams['token'];

    this.form = this.fb.group({
      OldPassword: ['', Validators.required],
      NewPassword: ['', Validators.required],
      ConfirmPassword: ['', Validators.required]
    });
  }

  onChangePassword() {
    if (this.form.valid && this.token) {
      this.http.post(environment.hostUrl + '/api/Account/ChangePassword', this.form.value, {
        headers: { 'Authorization': 'Bearer ' + this.token }
      }).subscribe(() => {
        alert('Password changed! Please login again.');
        this.router.navigate(['/login']);
      }, () => {
        alert('Error while changing password!');
      });
    }
  }

}
