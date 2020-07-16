import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlightsListComponent } from './flights-list/flights-list.component';
import { FlightsDetailsComponent } from './flights-details/flights-details.component';
import { FlightsRoutingModule } from './flights-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlightsSchedulerComponent } from './flights-scheduler/flights-scheduler.component';
import { FlightSeatsComponent } from './flight-seats/flight-seats.component';



@NgModule({
  declarations: [
    FlightsListComponent,
    FlightsDetailsComponent,
    FlightsSchedulerComponent,
    FlightSeatsComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    FlightsRoutingModule
  ]
})
export class FlightsModule { }
