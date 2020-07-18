import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IEnvironment } from 'src/environments/IEnvironment';
import { environment } from 'src/environments/environment';
import { IFlightBookingInfoDTO, IFlightSearchTerms, IBookingDTO, FlightBooking, ISeatDTO } from '../models/Booking';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookingsService {

  private _rootUrl: string;

  constructor(private httpClient: HttpClient) {
    const environmentVariable = <IEnvironment>environment;
    this._rootUrl = environmentVariable.apiEndPoint
  }

  public GetFlightBookingInfo(terms: IFlightSearchTerms): Observable<IFlightBookingInfoDTO[]> {

    return this.httpClient.post<IFlightBookingInfoDTO[]>(this._rootUrl + 'Booking/FlightBookingInfo', terms);
  }

  public CreateBooking(iBookingDTO: IBookingDTO): Observable<string> {

    return this.httpClient.post<string>(this._rootUrl + 'Booking', iBookingDTO);

  }
  public GetFlightSeatsSelection(flightId: string, flightSchedulerID): Observable<ISeatDTO[]> {
    return this.httpClient.get<ISeatDTO[]>(this._rootUrl + 'Booking/Seats/' + flightId + '/' + flightSchedulerID);
  }
}
