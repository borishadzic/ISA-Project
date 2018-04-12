import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';
import { Router } from '@angular/router';

import { RegisterModel } from '../models/register-model';
import { ExternalLoginModel } from '../models/external-login-model';
import { environment } from '../../environments/environment';

@Injectable()
export class AuthService {

  private token: string;
  public logedInEvent = new Subject<boolean>();
  private userRole: string;
  private adminOfTheater: string;
  private iSofaUserId: string;

  constructor(private http: HttpClient, private router: Router) {
    if (sessionStorage.getItem('accessToken')) {
      this.token = sessionStorage.getItem('accessToken');
      this.userRole = sessionStorage.getItem('userRole');
      this.adminOfTheater = sessionStorage.getItem('adminOfTheater');
      this.iSofaUserId = sessionStorage.getItem('iSofaUserId');
    } else if (localStorage.getItem('accessToken')) {
      this.token = localStorage.getItem('accessToken');
      this.userRole = localStorage.getItem('userRole');
      this.adminOfTheater = localStorage.getItem('adminOfTheater');
      this.iSofaUserId = localStorage.getItem('iSofaUserId');
    }
  }

  isLogedIn(): boolean {
    return this.token != null;
  }

  register(user: RegisterModel): Observable<any> {
    return this.http.post('http://localhost:49459/api/account/register', user);
  }

  getToken() {
    return this.token;
  }

  loginGoogle(token: string) {
    console.log('usao u loginGoogle');
    this.http.get('http://localhost:49459/api/Account/UserInfo', {
      headers: {
        'Authorization': 'Bearer ' + token
      }
    }).subscribe(response => {
      console.log('odgovor od userinfoa');
      if ((<any>response).HasRegistered) {
        console.log('registrovan korisnik');
        this.token = token;
        sessionStorage.setItem('accessToken', token);
        sessionStorage.setItem('Email', (<any>response).Email);
        // window.location.href= "index.html"
      } else {
        console.log('neregistrovan korisnik');
        this.http.post('http://localhost:49459/api/Account/RegisterExternal', null, {
          headers: {
            'Authorization': 'Bearer ' + token
          }
        }).subscribe(() => {

          console.log('preusmerenje za login');
          window.location.href = 'http://localhost:49459/api/Account/ExternalLogin?provider=Google&response_type=token&approval_prompt=force&client_id=self&redirect_uri=http%3A%2F%2Flocalhost%3A4200%2Flogin&state=XlwxCG0_Q1WtPZX3iOoc9uaiDRrzzmuPD7tzVhXcPXM1';
          // window.location.href='http://localhost:4200/login'

        }, () => {
          alert('Ne valja, boki je kriv');
        });
        console.log('Access-token= ' + token);
        // window.location.href = 'http://localhost:4200/register';
      }

      console.log('dosao sam ovde!');
    });
  }

  getExtrenalLogins(): Observable<ExternalLoginModel[]> {
    return this.http.get<ExternalLoginModel[]>(environment.hostUrl +
      '/api/Account/ExternalLogins?returnUrl=http%3a%2f%2flocalhost%3a4200%2flogin&generateState=true');
  }

  login(username: string, password: string, remember: boolean) {
    const reqBody = new HttpParams()
      .set('username', username)
      .set('password', password)
      .set('grant_type', 'password');


    this.http.post<any>('http://localhost:49459/Token', reqBody, {
      headers: { 'Content-Type' : 'application/x-www-form-urlencoded; charset=UTF-8'}
    }).subscribe(
      (value) => {
        if (remember) {
          localStorage.setItem('token', JSON.stringify(value));
          localStorage.setItem('accessToken', value.access_token);
          // ovo sam dodao
          localStorage.setItem('userRole', value.iSofAUserRole);
          localStorage.setItem('adminOfTheater', value.adminOfTheater);
          localStorage.setItem('iSofaUserId', value.iSofaUserId);
        } else {
          sessionStorage.setItem('token', JSON.stringify(value));
          sessionStorage.setItem('accessToken', value.access_token);
          // ovo sam dodao
          sessionStorage.setItem('userRole', value.iSofAUserRole);
          sessionStorage.setItem('adminOfTheater', value.adminOfTheater);
          sessionStorage.setItem('iSofaUserId', value.iSofaUserId);
        }
        this.token = value.access_token;
        // ovo isto
        this.userRole = value.iSofAUserRole;
        this.adminOfTheater = value.adminOfTheater;
        this.iSofaUserId = value.iSofaUserId;
        this.logedInEvent.next(true);
      },
      (error) => {
        this.logedInEvent.next(false);
      }
    );
  }

  // Posle doradi da bude poput onog na web apiju (ISofAAuthorizationAuthorization...)
  // PS. Vrednost one enumaracije se salje kao string 'FanZoneAdmin' ne 1
  public hasAccess(theaterId, userRole) {
    if (this.userRole === 'SysAdmin') {
      return true;
    }

    return this.adminOfTheater === theaterId && this.userRole === userRole;
  }

  get UserId(): string {
    return this.iSofaUserId;
  }

}
