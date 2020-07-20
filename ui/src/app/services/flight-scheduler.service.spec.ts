import { TestBed } from '@angular/core/testing';
import { FlightSchedulerService } from './flight-scheduler.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { IFlightScheduler } from '../models/Flight';


export const dummyflightSchedulers: IFlightScheduler[] = [
  { id: 'abc', flightId: 'xyz', departureDateTime: new Date(), arrivalDateTime: new Date(), journeyTime: '3 Hours' },
  { id: 'bcd', flightId: 'xyz', departureDateTime: new Date(), arrivalDateTime: new Date(), journeyTime: '3 Hours' }
];


describe('FlightSchedulerService', () => {
  let service: FlightSchedulerService,
    httpMock: HttpTestingController;
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ],
      providers: [
        FlightSchedulerService
      ]
    });
    httpMock = TestBed.get(HttpTestingController);
    service = TestBed.get(FlightSchedulerService);

  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should retrieve IFlightScheduler[] from server ', () => {

    service.GetFlightSchedulers('xyz').subscribe(
      scheduler => {
        expect(scheduler).toBeTruthy();
        expect(scheduler.length).toBe(2);
        expect(scheduler).toEqual(dummyflightSchedulers);
      }
    );
    const request = httpMock.expectOne(`${service.rootUrl}Scheduler/xyz`);
    expect(request.request.method).toBe('GET');
    request.flush(dummyflightSchedulers);
  });

  it('Should create Flight scheduler', () => {
    const scheduler: IFlightScheduler = { id: 'abc', flightId: 'xyz', departureDateTime: new Date(), arrivalDateTime: new Date(), journeyTime: '3 Hours' };

    service.CreateFlightScheduler(scheduler).subscribe(
      scheduler => {
        expect(scheduler).toBeTruthy();
        expect(scheduler).toBe(scheduler);
      });
    const request = httpMock.expectOne(`${service.rootUrl}Scheduler`);
    expect(request.request.method).toBe('POST');
    request.flush(scheduler);

  });

});
