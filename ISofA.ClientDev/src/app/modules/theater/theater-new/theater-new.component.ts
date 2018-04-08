import { Component, OnInit, NgZone, ViewChild, ElementRef } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { MapsAPILoader } from '@agm/core';
import { } from 'googlemaps';

import { TheaterService } from '../theater.service';

@Component({
  selector: 'app-theater-new',
  templateUrl: './theater-new.component.html',
  styleUrls: ['./theater-new.component.css']
})
export class TheaterNewComponent implements OnInit {

  public theaterType: any[] = [{ display: 'Cinema', value: 0}, { display: 'Play', value: 1}];
  public form: FormGroup;
  public latitude: number;
  public longitude: number;
  public searchControl: FormControl;
  public zoom: number;

  @ViewChild('search')
  public searchElementRef: ElementRef;

  constructor(private fb: FormBuilder,
              private mapsAPILoader: MapsAPILoader,
              private ngZone: NgZone,
              private theaterService: TheaterService,
              private router: Router) { }

  ngOnInit() {
    this.form = this.fb.group({
      Name: ['', Validators.required],
      Latitude: [null, Validators.required],
      Longitude: [null, Validators.required],
      Type: [0, Validators.required]
    });

    // set google maps defaults
    this.zoom = 4;
    this.latitude = 39.8282;
    this.longitude = -98.5795;

    // create search FormControl
    this.searchControl = new FormControl();

    // set current position
    this.setCurrentPosition();

    // load Places Autocomplete
    this.mapsAPILoader.load().then(() => {
      const autocomplete = new google.maps.places.Autocomplete(this.searchElementRef.nativeElement);
      autocomplete.addListener('place_changed', () => {
        this.ngZone.run(() => {
          // get the place result
          const place: google.maps.places.PlaceResult = autocomplete.getPlace();

          // verify result
          if (place.geometry === undefined || place.geometry === null) {
            return;
          }

          // set latitude, longitude and zoom
          this.latitude = place.geometry.location.lat();
          this.longitude = place.geometry.location.lng();
          this.zoom = 12;
        });
      });
    });
  }

  private setCurrentPosition() {
    if ('geolocation' in navigator) {
      navigator.geolocation.getCurrentPosition((position) => {
        this.latitude = position.coords.latitude;
        this.longitude = position.coords.longitude;
        this.zoom = 12;
      });
    }
  }

  onLocationSelected(event: any) {
    this.form.patchValue({
      'Latitude': event.coords.lat,
      'Longitude': event.coords.lng
    });
  }

  onSubmit() {
    if (this.form.valid) {
      this.theaterService.addTheater(this.form.value).subscribe(theater => {
        this.router.navigate(['/theaters', theater.TheaterId, 'register']);
      });
    }
  }

}
