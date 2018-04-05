import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class TheaterService {

  constructor(private http: HttpClient) { }

  public addTheater(theater: any): Observable<any> {
    return this.http.post<any>(environment.hostUrl + '/api/theaters', theater);
  }

}
