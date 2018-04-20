import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';
import { Observable } from 'rxjs/Observable';
import { SpeedSeatModel } from '../models/speed-seat-model';
import { DateUtils } from '../shared/date-utils';


@Injectable()
export class TicketDiscountService {

  constructor(private http: HttpClient) { }

  public getDiscountTickets(theaterId: number | string): Observable<SpeedSeatModel[]> {
    var tickets = this.http.get<SpeedSeatModel[]>(environment.hostUrl + `/api/theaters/${theaterId}/DiscountTickets`)

    return new Observable(sub => {
      tickets.subscribe((x) => {
        for (var i = 0; i < x.length; ++i)
          x[i].StartTime = DateUtils.convertUTCDateToLocalDate(new Date(x[i].StartTime as string));
        sub.next(x);
      }, (x) => { sub.error(x) }, () => { sub.complete() });
    });
  }

  public reserveDiscountTicket(seat: SpeedSeatModel):Observable<any> {
    return this.http.put<any>(environment.hostUrl + `/api/projections/${seat.ProjectionId}/DiscountTickets`, { SeatRow: seat.SeatRow, SeatColumn: seat.SeatColumn });
  }

  public createDiscountTicket(theaterId: number | string, projectionId: number | string, seat: any): Observable<any> {
    return this.http.post<any>(environment.hostUrl + `/api/Theaters/${theaterId}/Projections/${projectionId}/DiscountTickets`, seat);
  }
}