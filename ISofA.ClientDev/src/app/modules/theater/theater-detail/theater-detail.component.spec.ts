import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TheaterDetailComponent } from './theater-detail.component';

describe('TheaterDetailComponent', () => {
  let component: TheaterDetailComponent;
  let fixture: ComponentFixture<TheaterDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TheaterDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TheaterDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
