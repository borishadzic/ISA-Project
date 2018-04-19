import { Injectable } from '@angular/core';
import { TheaterService } from '../modules/theater/theater.service';
import { Observable } from 'rxjs/Observable';
import { UrlSegment } from '@angular/router';

@Injectable()
export class TheaterContainerService {

  private _theaterId: number = 0;

  public evaluated: boolean = false;  

  public TheaterId: number;
  public Name: string;
  public Address: string;
  public Description: string;
  public WorkStart: number;
  public WorkDuration: number;
  public Latitude: number;
  public Longitude: number;


  constructor(private theaterService: TheaterService) { }

  public getTheater(theaterId: number): Observable<any> {
    if (this._theaterId == 0 || this._theaterId != theaterId) {
      this.evaluated = false;
      this._theaterId = theaterId;
      this.TheaterId = theaterId;

      var theater = this.theaterService.getTheaterDetail("", theaterId);
      return new Observable(subscriber => {
        theater.subscribe(x => {
          this.TheaterId = x.TheaterId;
          this.Name = x.Name;
          this.Address = x.Address;
          this.Description = x.Description;
          this.WorkStart = x.WorkStart;
          this.WorkDuration = x.WorkDuration;
          this.Latitude = x.Latitude;
          this.Longitude = x.Longitude;
          this.evaluated = true;
          subscriber.next(this);
        })        
      })

    }

    return new Observable(x => x.next(this));
  }

  public resolveTheater(url: UrlSegment[]) {
    return this.getTheater(Number.parseInt(url[1].toString()));
  }

}
