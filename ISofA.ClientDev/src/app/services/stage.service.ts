import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';
import { Observable } from 'rxjs/Observable';
import { StageModel } from '../models/stage-model';

@Injectable()
export class StageService {

  constructor(private http: HttpClient) { }

  public getStage(theaterId: number|string, stageId: number|string) : Observable<StageModel> {
    return this.http.get<StageModel>(environment.hostUrl + `/api/theaters/${theaterId}/stages/${stageId}`);
  }

  public getStages(theaterId: number|string): Observable<StageModel[]> {
    return this.http.get<StageModel[]>(environment.hostUrl + '/api/theaters/' + theaterId + '/stages');
  }

  public addStage(theaterId: number|string, stage: any): Observable<any> {
    return this.http.post(environment.hostUrl + '/api/theaters/' + theaterId + '/stages', stage);
  }

  public updateStage(theaterId: number|string, stageId: number, stage: any): Observable<any> {
    return this.http.put(environment.hostUrl + '/api/theaters/' + theaterId + '/stages/' + stageId, stage);
  }
  
}
