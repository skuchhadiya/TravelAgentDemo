import { TestBed } from '@angular/core/testing';

import { FlightSchedulerService } from './flight-scheduler.service';

describe('FlightSchedulerService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FlightSchedulerService = TestBed.get(FlightSchedulerService);
    expect(service).toBeTruthy();
  });
});
