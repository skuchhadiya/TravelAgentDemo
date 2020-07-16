import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookingListComponent } from './booking-list/booking-list.component';
import { BookingDetailsComponent } from './booking-details/booking-details.component';

const routes: Routes = [
    {
        path: '',
        component: BookingListComponent
    },
    {
        path: ':id',
        component: BookingDetailsComponent
    }
];
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class BookingsRoutingModule { }
