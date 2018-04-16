import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class StageService {

  constructor(private http: HttpClient) { }

  public getStages(theaterId: number): Observable<any[]> {
    return this.http.get<any[]>(environment.hostUrl + '/api/theaters/' + theaterId + '/stages');
  }

  public addStage(theaterId: number, stage: any): Observable<any> {
    return this.http.post(environment.hostUrl + '/api/theaters/' + theaterId + '/stages', stage);
  }

  public updateStage(theaterId: number, stageId: number, stage: any): Observable<any> {
    return this.http.put(environment.hostUrl + '/api/theaters/' + theaterId + '/stages/' + stageId, stage);
  }
  
}
