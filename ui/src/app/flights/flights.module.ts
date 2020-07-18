import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlightsListComponent } from './flights-list/flights-list.component';
import { FlightsRoutingModule } from './flights-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlightsSchedulerComponent } from './flights-scheduler/flights-scheduler.component';

@NgModule({
  declarations: [
    FlightsListComponent,
    FlightsSchedulerComponent

  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    FlightsRoutingModule
  ]
})
export class FlightsModule { }
