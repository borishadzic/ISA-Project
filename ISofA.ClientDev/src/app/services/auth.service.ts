import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';

import { RegisterModel } from '../models/register-model';
import { ExternalLoginModel } from '../models/external-login-model';
import { environment } from '../../environments/environment';
import { ChangePasswordModel } from '../models/change-password-model';

@Injectable()
export class AuthService {

  private tokenInfo: any;
  private token: string;
  public logedInEvent = new Subject<boolean>();

  constructor(private http: HttpClient, private router: Router) {
    if (sessionStorage.getItem('accessToken')) {
      this.token = sessionStorage.getItem('accessToken');
      this.tokenInfo = JSON.parse(sessionStorage.getItem('tokenInfo'));
    } else if (localStorage.getItem('accessToken')) {
      this.token = localStorage.getItem('accessToken');
      this.tokenInfo = JSON.parse(localStorage.getItem('tokenInfo'));
    }
  }

  isLogedIn(): boolean {
    return this.token != null;
  }

  register(user: RegisterModel): Observable<any> {
    return this.http.post(environment.hostUrl+'/api/account/register', user);
  }

  changePassword(password: ChangePasswordModel){
    return this.http.post(environment.hostUrl+'/api/account/ChangePassword',password);
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
        this.router.navigate(['/']);
      } else {
        console.log('neregistrovan korisnik');
        this.http.post('http://localhost:49459/api/Account/RegisterExternal', null, {
          headers: {
            'Authorization': 'Bearer ' + token
          }
        }).subscribe(() => {

          console.log('preusmerenje za login');
          window.location.href = environment.hostUrl + '/api/Account/ExternalLogin?provider=Google&response_type=token&approval_prompt=force&client_id=self&redirect_uri=http%3A%2F%2Flocalhost%3A4200%2Flogin&state=XlwxCG0_Q1WtPZX3iOoc9uaiDRrzzmuPD7tzVhXcPXM1';

          // window.location.href='http://localhost:4200/login'

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

  login(username: string, password: string, remember: boolean): Observable<any> {
    const reqBody = new HttpParams()
      .set('username', username)
      .set('password', password)
      .set('grant_type', 'password');

    return this.http.post<any>('http://localhost:49459/Token', reqBody, {
      headers: { 'Content-Type' : 'application/x-www-form-urlencoded; charset=UTF-8'}
    }).pipe(
      tap((value) => {
        if (value.redirect === '0') {
          if (remember) {
            localStorage.setItem('tokenInfo', JSON.stringify(value));
            localStorage.setItem('accessToken', value.access_token);
          } else {
            sessionStorage.setItem('tokenInfo', JSON.stringify(value));
            sessionStorage.setItem('accessToken', value.access_token);
          }
          this.token = value.access_token;
          this.tokenInfo = value;
          this.logedInEvent.next(true);
        }
      })
    );
  }

  public hasAccess(theaterId, userRole) {
    if (this.tokenInfo == null) {
      return false;
    }

    // if (this.tokenInfo.iSofAUserRole === 'SysAdmin') {
    //   return true;
    // }

    return this.tokenInfo.adminOfTheater === theaterId && this.tokenInfo.iSofAUserRole === userRole;
  }

  get isAdmin(): boolean {
    if (this.tokenInfo) {
      return this.tokenInfo.iSofAUserRole === 'SysAdmin';
    } else  {
      return false;
    }
  }

  get adminOfTheater(): number {
    return this.tokenInfo.adminOfTheater;
  }

  get UserId(): string {
    return this.tokenInfo.iSofaUserId;
  }

  public logout() {
    sessionStorage.clear();
    localStorage.clear();
    this.token = null;
    this.tokenInfo = null;
    this.logedInEvent.next(false);
  }

}
