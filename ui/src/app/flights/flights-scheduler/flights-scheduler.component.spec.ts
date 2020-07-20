import { async, ComponentFixture, TestBed, tick, fakeAsync } from '@angular/core/testing';
import { FlightsSchedulerComponent } from './flights-scheduler.component';
import { FormsModule, ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { FlightsRoutingModule } from '../flights-routing.module';
import { FlightsModule } from '../flights.module';
import { FlightSchedulerService } from 'src/app/services/flight-scheduler.service';
import { ActivatedRoute } from '@angular/router';
import { dummyflightSchedulers } from 'src/app/services/flight-scheduler.service.spec';
import { of } from 'rxjs';
import { IFlightScheduler } from 'src/app/models/Flight';
import { doesNotThrow } from 'assert';
import { HttpClient } from '@angular/common/http';
import { emptyGuid } from 'src/app/constants/constants';

fdescribe('FlightsSchedulerComponent', () => {
  let component: FlightsSchedulerComponent;
  let service: FlightSchedulerService;
  let route = { snapshot: { params: { 'id': 'xyx' } } } as unknown as ActivatedRoute;

  xit('should create FlightsSchedulerComponent', () => {
    expect(component).toBeTruthy();
  });

  fit('should initialise flightSchedulers ngOnInit', () => {

    service = new FlightSchedulerService({} as HttpClient);
    spyOn(service, 'GetFlightSchedulers').and.returnValue(of(dummyflightSchedulers));

    component = new FlightsSchedulerComponent(route, service)
    component.ngOnInit();

    expect(component.flightSchedulers).toEqual(dummyflightSchedulers);

  });

  fit('should initialise form on ngOnInit', () => {

    service = new FlightSchedulerService({} as HttpClient);
    spyOn(service, 'GetFlightSchedulers').and.returnValue(of(dummyflightSchedulers));

    component = new FlightsSchedulerComponent(route, service)
    component.ngOnInit();

    expect(component.form).toBeTruthy();

  });

  fit('should create FlightScheduler ', () => {

    service = new FlightSchedulerService({} as HttpClient);
    let spy = spyOn(service, 'CreateFlightScheduler').and.returnValue(of(dummyflightSchedulers[0]));

    component = new FlightsSchedulerComponent(route, service)
    component.flightSchedulers = dummyflightSchedulers;

    component.form = new FormGroup({
      id: new FormControl(emptyGuid),
      flightId: new FormControl('xyz'),
      departureDateTime: new FormControl(new Date(), [Validators.required]),
      arrivalDateTime: new FormControl(new Date(), [Validators.required])
    });
    component.createScheduler();

    expect(spy).toHaveBeenCalled();
    expect(component.flightSchedulers.length).toBe(3);

  });

});
