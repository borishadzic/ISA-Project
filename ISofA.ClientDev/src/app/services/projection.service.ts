import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class ProjectionService {

  constructor(private http: HttpClient) { }

  public addProjections(theaterId: number, projections: any[]):Observable<any> {
    return this.http.post(environment.hostUrl + '/api/theaters/' + theaterId + '/projections', projections);
  }

  public getProjections(theaterId: number, dateStart: Date, days: number):Observable<any[]> {
    return this.http.get<any[]>(environment.hostUrl + '/api/projections', { params: {
      theaterId: theaterId + '',
      dateStart: dateStart.toJSON(),
      days: days +''
    }});
  }

}
