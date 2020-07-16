import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IEnvironment } from 'src/environments/IEnvironment';
import { environment } from 'src/environments/environment';
import { IFlightScheduler } from '../models/Flight';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FlightSchedulerService {

  private _rootUrl: string;

  constructor(private httpClient: HttpClient) {
    const environmentVariable = <IEnvironment>environment;
    this._rootUrl = environmentVariable.apiEndPoint
  }
  public GetFlightSchedulers(flightId: string): Observable<IFlightScheduler[]> {

    return this.httpClient.get<IFlightScheduler[]>(this._rootUrl + 'Scheduler/' + flightId);

  }

  public CreateFlightScheduler(flightScheduler: IFlightScheduler): Observable<IFlightScheduler> {

    return this.httpClient.post<IFlightScheduler>(this._rootUrl + 'Scheduler', flightScheduler);

  }
}