import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';
import { Observable } from 'rxjs/Observable';
import { SpeedSeatModel } from '../models/speed-seat-model';
import { DateUtils } from '../shared/date-utils';


@Injectable()
export class SeatService {

  constructor(private http: HttpClient) { }

  getSpeedSeats(theaterId: number | string): Observable<SpeedSeatModel[]> {
    var tickets = this.http.get<SpeedSeatModel[]>(environment.hostUrl + `/api/theaters/${theaterId}/Speed`)

    return new Observable(sub => {
      tickets.subscribe((x) => {
        for (var i = 0; i < x.length; ++i)
          x[i].StartTime = DateUtils.convertUTCDateToLocalDate(new Date(x[i].StartTime as string));
        sub.next(x);
      }, (x) => { sub.error(x) }, () => { sub.complete() });
    });
  }

}
