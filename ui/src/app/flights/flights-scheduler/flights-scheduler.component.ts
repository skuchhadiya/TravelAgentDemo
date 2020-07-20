import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { IFlightScheduler } from 'src/app/models/Flight';
import { FlightSchedulerService } from 'src/app/services/flight-scheduler.service';
import { emptyGuid } from 'src/app/constants/constants';

function diff_hours(dt2, dt1): any {
  var diff = (dt2.getTime() - dt1.getTime()) / 1000;
  diff /= (60 * 60);
  return diff;
}

@Component({
  selector: 'app-flights-scheduler',
  templateUrl: './flights-scheduler.component.html',
  styleUrls: ['./flights-scheduler.component.scss']
})
export class FlightsSchedulerComponent implements OnInit {

  minDate = new Date();
  flightId: string;
  flightSchedulers: IFlightScheduler[] = []
  form: FormGroup;

  get journeyTime(): string {
    if (this.form) {
      const departureDateTime = new Date(this.form.controls['departureDateTime'].value);
      const arrivalDateTime = new Date(this.form.controls['arrivalDateTime'].value);
      if (departureDateTime && arrivalDateTime) {
        return this.timeDiffCalc(arrivalDateTime, departureDateTime)
      }
      return '0'
    }
    return '0';
  }

  constructor(
    private _route: ActivatedRoute,
    private _flightSchedulerService: FlightSchedulerService) {
    this.flightId = _route.snapshot.params.id;
  }

  ngOnInit() {

    this._flightSchedulerService.GetFlightSchedulers(this.flightId)
      .subscribe(flightSchedulers => {
        this.flightSchedulers = flightSchedulers;
      });

    this.form = new FormGroup({
      id: new FormControl(emptyGuid),
      flightId: new FormControl(this.flightId),
      departureDateTime: new FormControl(null, [Validators.required]),
      arrivalDateTime: new FormControl(null, [Validators.required])
    });

  }

  createScheduler() {

    if (this.form.valid) {
      const newflightScheduler = this._bulid_IFlightScheduler_Instance(this.form);
      this._flightSchedulerService.CreateFlightScheduler(newflightScheduler)
        .subscribe(flightScheduler => {
          this.flightSchedulers.push(flightScheduler);
        })
    }

  }

  private _bulid_IFlightScheduler_Instance(form: FormGroup): IFlightScheduler {
    const departureDateTime = new Date(this.form.controls['departureDateTime'].value);
    const arrivalDateTime = new Date(this.form.controls['arrivalDateTime'].value);
    const diff = this.timeDiffCalc(arrivalDateTime, departureDateTime);

    return {

      id: form.controls['id'].value,
      flightId: form.controls['flightId'].value,
      departureDateTime: form.controls['departureDateTime'].value,
      arrivalDateTime: form.controls['arrivalDateTime'].value,
      journeyTime: diff

    };


  }

  private timeDiffCalc(dateFuture, dateNow) {
    let diffInMilliSeconds = Math.abs(dateFuture - dateNow) / 1000;

    // calculate days
    const days = Math.floor(diffInMilliSeconds / 86400);
    diffInMilliSeconds -= days * 86400;
    // calculate hours
    const hours = Math.floor(diffInMilliSeconds / 3600) % 24;
    diffInMilliSeconds -= hours * 3600;

    // calculate minutes
    const minutes = Math.floor(diffInMilliSeconds / 60) % 60;
    diffInMilliSeconds -= minutes * 60;

    let difference = '';
    if (days > 0) {
      difference += (days === 1) ? `${days} day, ` : `${days} days, `;
    }

    difference += (hours === 0 || hours === 1) ? `${hours} hour, ` : `${hours} hours, `;

    difference += (minutes === 0 || hours === 1) ? `${minutes} minutes` : `${minutes} minutes`;

    return difference;
  }

}
