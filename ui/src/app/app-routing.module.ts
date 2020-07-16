import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';


const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'home', component: HomeComponent },
  //lazy loaded route 
  {
    path: 'bookings',
    loadChildren: './bookings/bookings.module#BookingsModule'
  },
  {
    path: 'flights',
    loadChildren: './flights/flights.module#FlightsModule'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
