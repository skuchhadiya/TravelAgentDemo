import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { IFlightScheduler } from 'src/app/models/Flight';
import { FlightSchedulerService } from 'src/app/services/flight-scheduler.service';
import { emptyGuid } from 'src/app/constants/constants';
import { times } from 'src/app/constants/times';

@Component({
  selector: 'app-flights-scheduler',
  templateUrl: './flights-scheduler.component.html',
  styleUrls: ['./flights-scheduler.component.scss']
})
export class FlightsSchedulerComponent implements OnInit {

  times = times;
  flightId: string;
  flightSchedulers: IFlightScheduler[] = []
  form: FormGroup;

  constructor(
    private _route: ActivatedRoute,
    private _flightSchedulerService: FlightSchedulerService) {
    this.flightId = _route.snapshot.params.id;
  }

  ngOnInit() {
    this._flightSchedulerService.GetFlightSchedulers(this.flightId)
      .subscribe(flightSchedulers => {
        console.log(flightSchedulers);
        this.flightSchedulers = flightSchedulers;
      });

    this.form = new FormGroup({
      id: new FormControl(emptyGuid),
      flightId: new FormControl(this.flightId),
      departureTime: new FormControl(null, [Validators.required]),
      arrivalTime: new FormControl(null, [Validators.required]),
      journeyTime: new FormControl(null, [Validators.required]),
    });
  }

  createScheduler() {
    console.log(this.form);
    if (this.form.valid) {
      const newflightScheduler = this._bulid_IFlightScheduler_Instance(this.form);
      this._flightSchedulerService.CreateFlightScheduler(newflightScheduler)
        .subscribe(flightScheduler => {
          console.log(flightScheduler);
          this.flightSchedulers.push(flightScheduler);
        })
    }

  }

  private _bulid_IFlightScheduler_Instance(form: FormGroup): IFlightScheduler {
    return {
      id: form.controls['id'].value,
      flightId: form.controls['flightId'].value,
      departureTime: form.controls['departureTime'].value,
      arrivalTime: form.controls['arrivalTime'].value,
      journeyTime: form.controls['journeyTime'].value,
    };
  }

}
