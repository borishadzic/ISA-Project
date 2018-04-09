import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TheaterListComponent } from './theater-list.component';

describe('TheaterListComponent', () => {
  let component: TheaterListComponent;
  let fixture: ComponentFixture<TheaterListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TheaterListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TheaterListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
