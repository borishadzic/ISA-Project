import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';

import { RegisterModel } from '../models/register-model';

@Injectable()
export class AuthService {
  private token :string;
  public logedInEvent = new Subject<boolean>();

  constructor(private http: HttpClient) { 
    if(sessionStorage.getItem('accessToken'))
      this.token=sessionStorage.getItem('accessToken');
    else if (localStorage.getItem('accessToken'))
      this.token=localStorage.getItem('accessToken');
  }

  isLogedIn(): boolean {
    return this.token!=null;
  }

  register(user: RegisterModel): Observable<any> {
    return this.http.post('http://localhost:49459/api/account/register', user);
  }

  getToken(){
    return this.token;
  }

  loginGoogle(token: string){
    this.http.get('http://localhost:49459/api/Account/UserInfo',{
      headers:{
        'Authorization':'Bearer ' + token
      }
    }).subscribe(response=>{
      this.token=token;
      sessionStorage.setItem('accessToken',token);
      sessionStorage.setItem('Email',(<any>response).Email);
    })
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
          localStorage.setItem('accessToken',value.access_token);
        }
        else {
          sessionStorage.setItem('token', JSON.stringify(value));
          sessionStorage.setItem('accessToken',value.access_token);
        }
        this.token=value.access_token;
        this.logedInEvent.next(true);
      },
      (error)=>{
        this.logedInEvent.next(false);
      }
    )
  }

}
