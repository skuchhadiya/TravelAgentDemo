import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IEnvironment } from 'src/environments/IEnvironment';
import { environment } from 'src/environments/environment';
import { IFlightBookingInfoDTO, IFlightSearchTerms, IBookingDTO, ISeatDTO, FlightBookingDetailsDTO } from '../models/Booking';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookingsService {

  rootUrl: string;

  constructor(private httpClient: HttpClient) {
    const environmentVariable = <IEnvironment>environment;
    this.rootUrl = environmentVariable.apiEndPoint
  }

  public GetFlightBookingInfo(terms: IFlightSearchTerms): Observable<IFlightBookingInfoDTO[]> {

    return this.httpClient.post<IFlightBookingInfoDTO[]>(this.rootUrl + 'Booking/FlightBookingInfo', terms);
  }

  public CreateBooking(iBookingDTO: IBookingDTO): Observable<FlightBookingDetailsDTO> {

    return this.httpClient.post<FlightBookingDetailsDTO>(this.rootUrl + 'Booking', iBookingDTO);

  }

  public GetFlightSeatsSelection(flightId: string, flightSchedulerID): Observable<ISeatDTO[]> {

    return this.httpClient.get<ISeatDTO[]>(this.rootUrl + 'Booking/Seats/' + flightId + '/' + flightSchedulerID);

  }
}
