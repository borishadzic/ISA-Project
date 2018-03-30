import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Subscription } from 'rxjs/Subscription';
import { ActivatedRoute } from '@angular/router';





@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {

  private sub: Subscription;
  loginForm: FormGroup;
  googleAuth='http://localhost:49459/api/Account/ExternalLogin?provider=Google&response_type=token&approval_prompt=force&client_id=self&redirect_uri=http%3A%2F%2Flocalhost%3A4200%2Flogin&state=XlwxCG0_Q1WtPZX3iOoc9uaiDRrzzmuPD7tzVhXcPXM1';

  constructor(private fb: FormBuilder, private authService: AuthService,private route:ActivatedRoute) { }

  ngOnInit() {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      remember: true,
    });
    this.sub = this.authService.logedInEvent.subscribe(
      (value) => {
        if (value == true) {
          
        }
      }
    );

    this.route.fragment.subscribe(param=> {
      if(param && param.split('access_token='))
        if(param.split('access_token=')[1].split('&')[0])
          this.authService.loginGoogle(param.split('access_token=')[1].split('&')[0]);
          
    });
  }

  ngOnDestroy() {
    this.sub.unsubscribe(); 
  }

  onLogin() {
    this.authService.login(this.loginForm.value.username,
       this.loginForm.value.password, 
       this.loginForm.value.remember);
  }

 
  isUserRegistered(accessToken){
    
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

