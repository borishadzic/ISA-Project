import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { environment } from '../../environments/environment';
import { LevelRequirements } from '../models/level-requirements';

@Injectable()
export class LevelRequirementsService {

  constructor(private http: HttpClient) { }

  getLevelRequirements(): Observable<LevelRequirements> {
    return this.http.get<LevelRequirements>(environment.hostUrl + '/api/configs/userlevel');
  }

  setLevelRequirements(levels: LevelRequirements): Observable<LevelRequirements> {
    return this.http.post<LevelRequirements>(environment.hostUrl + '/api/configs/userlevel', levels);
  }

}
