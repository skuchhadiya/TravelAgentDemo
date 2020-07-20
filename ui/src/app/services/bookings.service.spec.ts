import { TestBed } from '@angular/core/testing';
import { BookingsService } from './bookings.service';
import { HttpTestingController, HttpClientTestingModule } from '@angular/common/http/testing';
import { IFlightBookingInfoDTO, IFlightSearchTerms } from '../models/Booking';
import { FlightTypeEnum } from '../enums/FlightTypeEnum';
import { ClientTypesEnum } from '../enums/ClientTypesEnum';

export const flightBookingInfoDTO: IFlightBookingInfoDTO[] = [

  {
    flightId: 'xyz',
    flightSchedulerId: 'abc',
    flightType: FlightTypeEnum.OutBound,
    code: 'BA451',
    arrival: 'London',
    depature: 'Mumbai',
    departureDateTime: new Date(),
    arrivalDateTime: new Date(),
    journeyTime: '9 Hours',
    price: '450',
    seatId: 'bcd'
  },
  {
    flightId: 'bcd',
    flightSchedulerId: 'bcd',
    flightType: FlightTypeEnum.InBound,
    code: 'BA452',
    arrival: 'Mumbai',
    depature: 'London',
    departureDateTime: new Date(),
    arrivalDateTime: new Date(),
    journeyTime: '9 Hours',
    price: '450',
    seatId: 'bcd'
  }

];


fdescribe('BookingsService', () => {
  let service: BookingsService,
    httpMock: HttpTestingController;
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ],
      providers: [
        BookingsService
      ]
    });
    httpMock = TestBed.get(HttpTestingController);
    service = TestBed.get(BookingsService);

  });

  afterEach(() => {
    httpMock.verify();
  });

  fit('Should returns IFlightBookingInfoDTO[] from server', () => {
    const terms: IFlightSearchTerms = {
      type: ClientTypesEnum.OneWay,
      arrival: 'London',
      depature: 'Mumbai',
      depatureDate: new Date('2020-07-20'),
      returnDate: new Date('2020-07-21')
    };

    service.GetFlightBookingInfo(terms).subscribe(
      flighBookingInfo => {
        expect(flighBookingInfo).toBeTruthy();
        expect(flighBookingInfo).toBe(flightBookingInfoDTO);
      });
    const request = httpMock.expectOne(`${service.rootUrl}Booking/FlightBookingInfo`);
    expect(request.request.method).toBe('POST');
    request.flush(flightBookingInfoDTO);
  });
});
