import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FlightsListComponent } from './flights-list/flights-list.component';
import { FlightsDetailsComponent } from './flights-details/flights-details.component';
import { FlightsSchedulerComponent } from './flights-scheduler/flights-scheduler.component';
import { FlightSeatsComponent } from './flight-seats/flight-seats.component';

const routes: Routes = [
    {
        path: '',
        component: FlightsListComponent
    },

    {
        path: 'scheduler/:id',
        component: FlightsSchedulerComponent
    },
    {
        path: 'seat/:id',
        component: FlightSeatsComponent
    },
    {
        path: 'deatils/:id',
        component: FlightsDetailsComponent
    },
];
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class FlightsRoutingModule { }
