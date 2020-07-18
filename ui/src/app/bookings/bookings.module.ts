import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookingComponent } from './booking/booking.component';
import { BookingsRoutingModule } from './bookings-routing.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { FlightInfoComponent } from './flight-info/flight-info.component';

@NgModule({
  declarations: [

    BookingComponent,
    FlightInfoComponent],

  imports: [

    CommonModule,
    FormsModule,
    BookingsRoutingModule,
    ReactiveFormsModule
  ]
})

export class BookingsModule { }
