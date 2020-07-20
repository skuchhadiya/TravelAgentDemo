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
  errorMessage: string;
  successMessage: string;
  minDate = new Date();
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
      depatureDate: new FormControl(null, [Validators.required]),
      arrival: new FormControl({ value: null, disabled: true }, [Validators.required]),
      returnDate: new FormControl(null),
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

  returnSelected() {
    this.searchform.controls['returnDate'].setValidators([Validators.required]);
  }

  createSearch() {
    this.errorMessage = null;
    this.successMessage = null
    this.bookingform.reset();
    if (this.searchform.valid) {

      const search = this._build_IFlightSearchTerms_Instance(this.searchform);
      if (search.type === ClientTypesEnum.Return) {
        this.bookingform.controls['inBoudFlightInfo'].setValidators([Validators.required]);
        this.bookingform.controls['inBoudFlightSeat'].setValidators([Validators.required]);
      }

      this._bookinService.GetFlightBookingInfo(search)
        .subscribe(data => {
          if (data && data.length == 0) {
            this.errorMessage = 'Flights not found';
          }
          this.outBoudsFlight = data.filter(x => x.flightType === FlightTypeEnum.OutBound);
          this.inBoudsFlight = data.filter(x => x.flightType === FlightTypeEnum.InBound);

        },
          error => {
            this.errorMessage = 'Error while searching for available flights for given search';
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
    this.errorMessage = null;
    const amount = parseFloat(this.bookingform.get('totalAmount').value ? this.bookingform.get('totalAmount').value : '0');
    const newValue = amount + flight.price;
    this.bookingform.get('totalAmount').setValue(newValue);

    this._bookinService.GetFlightSeatsSelection(flight.flightId, flight.flightSchedulerId)
      .subscribe(seats => {
        if (!seats || seats && seats.length == 0) {
          this.errorMessage = 'All Seats for flight ' + flight.code + ' is allocated ';
        }
        if (flight.flightType === FlightTypeEnum.OutBound)
          this.outBoundFlightseatsOptions = seats;
        else (flight.flightType === FlightTypeEnum.InBound)
        this.inBoundFlightseatsOptions = seats;
      },
        error => {
          this.errorMessage = 'Unable to find available seats for flight' + flight.code;
        }
      );

  }

  oneWaySelected() {
    this.searchform.controls['returnDate'].setValidators([]);
    this.searchform.controls['returnDate'].reset();
  }

  onSubmit() {
    this.errorMessage = null;
    this.successMessage = null
    if (this.bookingform.valid) {
      const value = this._build_IBookingDTO_Instance(this.bookingform);
      this._bookinService.CreateBooking(value)
        .subscribe(newBookingDetils => {
          if (newBookingDetils) this.bookingform.reset();
          this.successMessage = 'Thanks ' + newBookingDetils.name + ' for booking with travel Agent, Your booking has been confirmend.' +
            'Booking Ref: ' + newBookingDetils.bookingRef;
          this.bookingDeatils = newBookingDetils;
        },
          error => {
            this.errorMessage = 'Something went worng';
          })
    }

  }


  private _build_IFlightSearchTerms_Instance(form: FormGroup): IFlightSearchTerms {

    return {
      type: form.controls['type'].value,
      depature: form.controls['depature'].value,
      arrival: form.controls['arrival'].value,
      depatureDate: form.controls['depatureDate'].value,
      returnDate: form.controls['returnDate'].value,
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
