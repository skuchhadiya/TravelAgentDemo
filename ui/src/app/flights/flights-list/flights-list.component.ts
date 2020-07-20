import { Component, OnInit } from '@angular/core';
import { IFlight } from 'src/app/models/Flight';
import { FlightsService } from 'src/app/services/flights.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { emptyGuid, totalSeatsOption } from 'src/app/constants/constants';
import { locations } from 'src/app/constants/locations';
import { deepClone } from 'src/app/Utility/deep-Clone';

@Component({
  selector: 'app-flights-list',
  templateUrl: './flights-list.component.html',
  styleUrls: ['./flights-list.component.scss']
})

export class FlightsListComponent implements OnInit {

  depaturelocations = deepClone(locations);
  totalSeatsOption = totalSeatsOption;
  arrivallocations = [];
  form: FormGroup;
  flights: IFlight[] = [];

  constructor(private _flightService: FlightsService) { }

  ngOnInit() {

    this._flightService.GetFlights().subscribe(flights => {
      this.flights = flights
    });

    this.form = new FormGroup({
      id: new FormControl(emptyGuid),
      code: new FormControl(null, [Validators.required]),
      depature: new FormControl(null, [Validators.required]),
      arrival: new FormControl({ value: null, disabled: true }, [Validators.required]),
      totalSeats: new FormControl(null, [Validators.required]),
      price: new FormControl(null, [Validators.required]),
    });

  }

  createFligth(form: FormGroup) {
    console.log(form);

    if (form.valid) {
      const flight = this._bulid_IFlight_Instance(this.form)
      this._flightService.CreateFlight(flight).subscribe(flight => {
        console.log(flight);
        this.flights.push(flight);
      })
    }
  }

  depatureSelected(eventValue: string) {

    this.arrivallocations = deepClone(locations)
    const index = this.arrivallocations.findIndex(x => x === eventValue);
    this.arrivallocations.splice(index, 1);
    this.form.get('arrival').enable();
    this.form.get('arrival').setValue(null)
  }

  private _bulid_IFlight_Instance(form: FormGroup): IFlight {
    return {
      id: form.controls['id'].value,
      code: form.controls['code'].value,
      depature: form.controls['depature'].value,
      arrival: form.controls['arrival'].value,
      totalSeats: parseInt(form.controls['totalSeats'].value),
      price: form.controls['price'].value,
    };
  }
}
