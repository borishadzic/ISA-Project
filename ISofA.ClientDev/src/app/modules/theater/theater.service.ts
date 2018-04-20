import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs/Observable';
import { UrlSegment } from '@angular/router';
import { DateUtils } from '../../shared/date-utils';

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
    var theater = this.http.get<any>(environment.hostUrl + '/api/theaters/' + id);
    return new Observable(sub => theater.subscribe(x => {
      var start = DateUtils.convertUTCDateToLocalDate(new Date(x.WorkStart));
      x.WorkStart = start.getHours() * 60 + start.getMinutes();

      sub.next(x);
    }));
  }

  public getTheaters(type: string): Observable<any[]> {
    return this.http.get<any[]>(environment.hostUrl + '/api/theaters', { params: { type: type } });
  }

  public addTheater(theater: any): Observable<any> {
    return this.http.post<any>(environment.hostUrl + '/api/theaters', theater);
  }

  public updateTheater(theaterId: number, theater: any): Observable<any> {
    var start = new Date();
    start.setHours(Math.floor(theater.WorkStart / 60));
    start.setMinutes(theater.WorkStart % 60);
    theater.WorkStart = start;
    return this.http.put<any>(environment.hostUrl + '/api/theaters/' + theaterId, theater);
  }

  public addTheaterAdmin(theaterId, user): Observable<any> {
    return this.http.post<any>(environment.hostUrl + '/api/theaters/' + theaterId + '/admins', user);
  }

}
