import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookingListComponent } from './booking-list/booking-list.component';
import { BookingDetailsComponent } from './booking-details/booking-details.component';
import { BookingsRoutingModule } from './bookings-routing.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { FlightInfoComponent } from './flight-info/flight-info.component';



@NgModule({
  declarations: [
    BookingListComponent,
    BookingDetailsComponent,
    FlightInfoComponent],
  imports: [
    CommonModule,
    FormsModule,
    BookingsRoutingModule,
    ReactiveFormsModule
  ]
})
export class BookingsModule { }
