import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { AuthService } from '../../services/auth.service';
import { Subscription } from 'rxjs/Subscription';
import { ExternalLoginModel } from '../../models/external-login-model';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  externalLogins: ExternalLoginModel[];
  env = environment;

  constructor(private fb: FormBuilder,
              private authService: AuthService,
              private router: Router,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      remember: true,
    });

    this.route.fragment.subscribe(param => {
      if (param && param.split('access_token=')) {
        if (param.split('access_token=')[1].split('&')[0]) {
          this.authService.loginGoogle(param.split('access_token=')[1].split('&')[0]);
        }
      }
    });

    this.authService.getExtrenalLogins().subscribe(x => this.externalLogins = x);
  }

  onLogin() {
    this.authService.login(this.loginForm.value.username,
                           this.loginForm.value.password,
                           this.loginForm.value.remember).subscribe(
      (val) => {
        if (val.redirect === '1') {
          this.router.navigate(['/change-password'], {
            queryParams: { 'token': val.access_token}
          });
        } else {
          this.router.navigate(['/']);
        }
      },
      () => alert('Username or password invalid!')
    );
  }


  isUserRegistered(accessToken) {

  }

  // getAccessToken(){
  //   if(location.hash){
  //     if(location.hash.split('access_token=')){
  //       var accessToken=location.hash.split('access_token=')[1].split('&')[0];
  //       if(accessToken){
  //         this.isUserRegistered(accessToken);
  //       }
  //     }
  //   }
  // }


}

