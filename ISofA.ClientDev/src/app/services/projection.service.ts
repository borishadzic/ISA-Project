import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';
import { Observable } from 'rxjs/Observable';
import { DateUtils } from '../shared/date-utils';

@Injectable()
export class ProjectionService {

  constructor(private http: HttpClient) { }

  public addProjections(theaterId: number, projections: any[]): Observable<any> {
    return this.http.post(environment.hostUrl + '/api/theaters/' + theaterId + '/projections', projections);
  }

  public getProjections(theaterId: number, dateStart: Date, days: number): Observable<any[]> {
    return this.http.get<any[]>(environment.hostUrl + '/api/projections', {
      params: {
        theaterId: theaterId + '',
        dateStart: dateStart.toJSON(),
        days: days + ''
      }
    });
  }

  public getProjection(projectionId: number): Observable<any> {
    var projection = this.http.get<any>(environment.hostUrl + `/api/projections/${projectionId}`);
    return new Observable(sub => {
      projection.subscribe(proj => {
        proj.StartTime = DateUtils.convertUTCDateToLocalDate(new Date(proj.StartTime));
        sub.next(proj);
      }, err => sub.error(err), () => sub.complete());
    })
  }

}
