import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs/Observable';
import { AuthService } from '../../services/auth.service';

@Injectable()
export class TheaterAdminService {

  constructor(private http: HttpClient,
    private authService: AuthService) { }

  public getAdminTheater(): Observable<any> {
    console.log(this.authService)
    return this.http.get<any>(environment.hostUrl + '/api/theaters/' + this.authService.adminOfTheater);
  }

}