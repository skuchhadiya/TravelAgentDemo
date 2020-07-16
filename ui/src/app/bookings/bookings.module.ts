import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookingListComponent } from './booking-list/booking-list.component';
import { BookingDetailsComponent } from './booking-details/booking-details.component';
import { BookingsRoutingModule } from './bookings-routing.module';



@NgModule({
  declarations: [
    BookingListComponent,
    BookingDetailsComponent],
  imports: [
    CommonModule,
    BookingsRoutingModule
  ]
})
export class BookingsModule { }
