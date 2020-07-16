import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FlightsSchedulerComponent } from './flights-scheduler.component';

describe('FlightsSchedulerComponent', () => {
  let component: FlightsSchedulerComponent;
  let fixture: ComponentFixture<FlightsSchedulerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FlightsSchedulerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FlightsSchedulerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
