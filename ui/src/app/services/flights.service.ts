import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IEnvironment } from 'src/environments/IEnvironment';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { IFlight, IFlightDeatils, IFlightScheduler } from '../models/Flight';

@Injectable({
  providedIn: 'root'
})

export class FlightsService {

  private _rootUrl: string;

  constructor(private httpClient: HttpClient) {
    const environmentVariable = <IEnvironment>environment;
    this._rootUrl = environmentVariable.apiEndPoint
  }

  public GetFlights(): Observable<IFlight[]> {

    return this.httpClient.get<IFlight[]>(this._rootUrl + 'Flight');

  }

  public GetFlight(Id: string): Observable<IFlightDeatils> {

    return this.httpClient.get<IFlightDeatils>(this._rootUrl + 'Flight/' + Id);

  }

  public CreateFlight(flight: IFlight): Observable<IFlight> {
    console.log(flight);

    return this.httpClient.post<IFlight>(this._rootUrl + 'Flight', flight);

  }

  public CreateFlightScheduler(flightScheduler: IFlightScheduler): Observable<IFlightScheduler> {

    return this.httpClient.post<IFlightScheduler>(this._rootUrl + 'Scheduler', flightScheduler);

  }
}
