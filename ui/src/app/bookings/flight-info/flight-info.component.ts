import { Component, Input, forwardRef, OnInit, OnChanges, SimpleChanges, Output, EventEmitter } from '@angular/core';
import { IFlightBookingInfoDTO } from 'src/app/models/Booking';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, FormGroup, FormControl, Validators } from '@angular/forms';
import { FlightTypeEnum } from 'src/app/enums/FlightTypeEnum';
import { deepClone } from 'src/app/Utility/deep-Clone';

@Component({
  selector: 'app-flight-info',
  templateUrl: './flight-info.component.html',
  styleUrls: ['./flight-info.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => FlightInfoComponent),
      multi: true
    }
  ]
})
export class FlightInfoComponent implements OnInit, OnChanges, ControlValueAccessor {

  @Input() flightOptions: IFlightBookingInfoDTO[];
  @Input() isOutBound: boolean;
  @Output() change = new EventEmitter<IFlightBookingInfoDTO>();
  options: IFlightBookingInfoDTO[];
  private _onChange: (option) => void;
  form: FormGroup;
  ngOnInit(): void {

    this.form = new FormGroup({
      seletedFlight: new FormControl(null, [Validators.required])
    })

  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['flightOptions'].currentValue) {
      this.options = deepClone(this.flightOptions);
      if (changes['flightOptions'].firstChange == false)
        this.form.controls['seletedFlight'].setValue(null);
    }
  }


  writeValue(obj: IFlightBookingInfoDTO): void {
    this.form.controls['seletedFlight'].setValue(obj)
  }
  registerOnChange(fn: any): void {
    this._onChange = fn;
  }
  registerOnTouched(fn: any): void {
  }
  setDisabledState?(isDisabled: boolean): void {
  }
  onSelect(option: IFlightBookingInfoDTO) {

    this.options = [option];
    this.form.controls['seletedFlight'].setValue(option);
    this.change.emit(option);
    this._onChange(option);
  }

}
