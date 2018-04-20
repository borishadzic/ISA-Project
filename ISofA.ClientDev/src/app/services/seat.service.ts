import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class SeatService {

  constructor(private http: HttpClient) { }

  public getSeats(projectionId: number) {
    return this.http.get<any[]>(environment.hostUrl + `/api/theaters/projections/${projectionId}/seats`);
  }

  createVIPSeat(theaterId:number | string, projectionId: number | string, seat: any) {
    return this.http.post<any>(environment.hostUrl + `/api/theaters/${theaterId}/projections/${projectionId}/vipseats`, seat);
  }
}
