import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FlightsListComponent } from './flights-list/flights-list.component';
import { FlightsSchedulerComponent } from './flights-scheduler/flights-scheduler.component';

const routes: Routes = [
    {
        path: '',
        component: FlightsListComponent
    },

    {
        path: 'scheduler/:id',
        component: FlightsSchedulerComponent
    }
];
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class FlightsRoutingModule { }
