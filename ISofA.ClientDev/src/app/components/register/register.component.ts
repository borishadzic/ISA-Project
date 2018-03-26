import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  form:FormGroup;
  fail: boolean = false;
  constructor(private fb: FormBuilder, 
              private authorservice: AuthService,
              private rotuer: Router) { }

  ngOnInit() {
    this.form = this.fb.group({
      'email': ['', [Validators.required, Validators.email]],
      'password':['',Validators.required],
      'confirmPassword':['',Validators.required],
      'name':['', Validators.required],
      'surname':['',Validators.required],
      'city':'',
      'phoneNumber':''
    }, {
      validator:this.checkIfEqual
    });
  }

  checkIfEqual(group : FormGroup){
    const pass = group.controls.password.value;
    const confirmPass = group.controls.confirmPassword.value;

    return pass===confirmPass? null:{notSame:true}; 
  }

  onRegister() {
    if (this.form.valid) {
      console.log(this.form.value);
      this.authorservice.register(this.form.value).subscribe(
        () => this.rotuer.navigate(['/login']),
        (err) => this.fail = true
      )
    }
  }

}
