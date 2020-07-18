import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { BookingsService } from 'src/app/services/bookings.service';
import { ClientTypesEnum } from 'src/app/enums/ClientTypesEnum';
import { deepClone } from 'src/app/Utility/deep-Clone';
import { locations } from 'src/app/constants/locations';
import { IFlightSearchTerms, IFlightBookingInfoDTO, ISeatDTO, IBookingDTO, FlightBookingDetailsDTO } from 'src/app/models/Booking';
import { FlightTypeEnum } from 'src/app/enums/FlightTypeEnum';

@Component({

  selector: 'app-booking',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.scss']

})

export class BookingComponent implements OnInit {

  clientTypeEnum = ClientTypesEnum;
  searchform: FormGroup;
  bookingform: FormGroup;
  depaturelocations = deepClone(locations);
  arrivallocations = [];

  outBoudsFlight: IFlightBookingInfoDTO[];
  inBoudsFlight: IFlightBookingInfoDTO[];
  outBoundFlightseatsOptions: ISeatDTO[] = [];
  inBoundFlightseatsOptions: ISeatDTO[] = [];


  bookingDeatils: FlightBookingDetailsDTO;
  constructor(private _bookinService: BookingsService) { }

  ngOnInit() {

    this.searchform = new FormGroup({
      type: new FormControl(ClientTypesEnum.OneWay, [Validators.required]),
      depature: new FormControl(null, [Validators.required]),
      arrival: new FormControl({ value: null, disabled: true }, [Validators.required])
    })

    this.bookingform = new FormGroup({
      name: new FormControl(null, [Validators.required]),
      outBoudFlightInfo: new FormControl(null, [Validators.required]),
      outBoudFlightSeat: new FormControl(null, [Validators.required]),
      totalAmount: new FormControl(null, [Validators.required]),
      inBoudFlightInfo: new FormControl(null),
      inBoudFlightSeat: new FormControl(null)
    })
  }

  createSearch() {

    this.bookingform.reset();

    if (this.searchform.valid) {

      const search = this._build_IFlightSearchTerms_Instance(this.searchform);
      if (search.type === ClientTypesEnum.Return) {
        this.bookingform.controls['inBoudFlightInfo'].setValidators([Validators.required]);
        this.bookingform.controls['inBoudFlightSeat'].setValidators([Validators.required]);
      }

      this._bookinService.GetFlightBookingInfo(search)
        .subscribe(data => {

          this.outBoudsFlight = data.filter(x => x.flightType === FlightTypeEnum.OutBound);
          this.inBoudsFlight = data.filter(x => x.flightType === FlightTypeEnum.InBound);

        })
    }
  }

  depatureSelected(eventValue: string) {

    this.arrivallocations = deepClone(locations)
    const index = this.arrivallocations.findIndex(x => x === eventValue);
    this.arrivallocations.splice(index, 1);
    this.searchform.get('arrival').enable();
    this.searchform.get('arrival').setValue(null);

  }

  onSelectFlight(flight: IFlightBookingInfoDTO) {

    const amount = parseFloat(this.bookingform.get('totalAmount').value ? this.bookingform.get('totalAmount').value : '0');
    const newValue = amount + flight.price;
    this.bookingform.get('totalAmount').setValue(newValue);

    this._bookinService.GetFlightSeatsSelection(flight.flightId, flight.flightSchedulerId)
      .subscribe(seats => {
        if (flight.flightType === FlightTypeEnum.OutBound)
          this.outBoundFlightseatsOptions = seats;
        else (flight.flightType === FlightTypeEnum.InBound)
        this.inBoundFlightseatsOptions = seats;
      });

  }

  onSubmit() {

    if (this.bookingform.valid) {
      const value = this._build_IBookingDTO_Instance(this.bookingform);
      this._bookinService.CreateBooking(value)
        .subscribe(data => {
          console.log(data);
          this.bookingDeatils = data;
        })
    }

  }


  private _build_IFlightSearchTerms_Instance(form: FormGroup): IFlightSearchTerms {

    return {
      type: form.controls['type'].value,
      depature: form.controls['depature'].value,
      arrival: form.controls['arrival'].value
    }

  }

  private _build_IBookingDTO_Instance(form: FormGroup): IBookingDTO {

    const outFlightInfo: IFlightBookingInfoDTO = form.controls['outBoudFlightInfo'].value;
    const outFlightSeat: string = form.controls['outBoudFlightSeat'].value;
    outFlightInfo.seatId = outFlightSeat;

    const inFlightInfo: IFlightBookingInfoDTO = form.controls['inBoudFlightInfo'].value;

    let inFlightSeat: string;
    if (inFlightInfo) {
      inFlightSeat = form.controls['inBoudFlightSeat'].value;
      inFlightInfo.seatId = inFlightSeat
    }

    return {
      name: form.controls['name'].value,
      outBoudFlightInfo: outFlightInfo,
      totalAmount: parseFloat(form.controls['totalAmount'].value),
      inBoudFlightInfo: inFlightInfo,
    }
  }

}
