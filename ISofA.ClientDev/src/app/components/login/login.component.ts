import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Subscription } from 'rxjs/Subscription';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {

  private sub: Subscription;
  loginForm: FormGroup;

  constructor(private fb: FormBuilder, private authService: AuthService) { }

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
  }

  ngOnDestroy() {
    this.sub.unsubscribe(); 
  }

  onLogin() {
    this.authService.login(this.loginForm.value.username,
       this.loginForm.value.password, 
       this.loginForm.value.remember);
  }

  
}

