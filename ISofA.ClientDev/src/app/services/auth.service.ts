import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';

import { RegisterModel } from '../models/register-model';

@Injectable()
export class AuthService {

  public logedInEvent = new Subject<boolean>();

  constructor(private http: HttpClient) { }

  isLogedIn(): boolean {
    return sessionStorage.getItem('token') != null || localStorage.getItem('token') != null;
  }

  register(user: RegisterModel): Observable<any> {
    return this.http.post('http://localhost:49459/api/account/register', user);
  }

  login(username: string, password: string, remember: boolean) {
    const reqBody = new HttpParams()
      .set('username',username)
      .set('password',password)
      .set('grant_type','password');
      

    this.http.post<any>('http://localhost:49459/Token',reqBody, {
      headers: { 'Content-Type' : 'application/x-www-form-urlencoded; charset=UTF-8'}
    }).subscribe(
      (value)=>{
        if (remember) {
          localStorage.setItem('token', JSON.stringify(value));
        }
        else {
          sessionStorage.setItem('token', JSON.stringify(value));
        }

        this.logedInEvent.next(true);
      },
      (error)=>{
        this.logedInEvent.next(false);
      }
    )
  }

}
