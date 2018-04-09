import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs/Observable';
import { UrlSegment } from '@angular/router';

@Injectable()
export class TheaterService {

  constructor(private http: HttpClient) { }

  public theaterTypeFragment(url: UrlSegment[]): string {
    return url[0].toString();
  }

  public theaterIdFragment(url: UrlSegment[]): number {
    return Number.parseInt(url[1].toString());
  }

  public getTheaterDetail(type: string, id: number): Observable<any> {
    return this.http.get<any>(environment.hostUrl + '/api/theaters/' + id);
  }

  public getTheaters(type: string): Observable<any[]> {
    return this.http.get<any[]>(environment.hostUrl + '/api/theaters', { params: { type: type } });
  }

  public addTheater(theater: any): Observable<any> {
    return this.http.post<any>(environment.hostUrl + '/api/theaters', theater);
  }

  public addTheaterAdmin(theaterId, user): Observable<any> {
    return this.http.post<any>(environment.hostUrl + '/api/theaters/' + theaterId + '/admins', user);
  }

}
