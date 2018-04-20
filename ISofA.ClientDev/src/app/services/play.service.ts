import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class PlayService {

  constructor(private http: HttpClient) { }

  public getPlay(playId: number): Observable<any> {
    return this.http.get<any>(environment.hostUrl + `/api/plays/${playId}`);
  }

  public getPlays(theaterId: number): Observable<any[]> {
    return this.http.get<any[]>(environment.hostUrl + '/api/plays', { params: { theaterId: theaterId + '' } });
  }

  public addPlay(theaterId: number, play: any): Observable<any> {
    return this.http.post(environment.hostUrl + '/api/theaters/' + theaterId + '/plays', play);
  }

  public updatePlay(theaterId: number, playId: number, play: any): Observable<any> {
    return this.http.put(environment.hostUrl + '/api/theaters/' + theaterId + '/plays/' + playId, play);
  }

  public deletePlay(theaterId: number, playId: number): Observable<any> {
    return this.http.delete(environment.hostUrl + '/api/theaters/' + theaterId + '/plays/' + playId);
  }

}
