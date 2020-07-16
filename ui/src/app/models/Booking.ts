import { ClientTypesEnum } from '../enums/ClientTypesEnum';
import { IFlight, IFlightScheduler, Seat } from './Flight';

export interface IClient {
    id: string;
    name: string;
    type: ClientTypesEnum
}

export interface FlightBooking {

    id: string;
    bookingRef: string;
    bookedDate: Date;

    clientID: string;
    client?: IClient;

    outBookingDate: Date;

    outFlightId: string;
    outFlight?: IFlight

    outFlightSchedulerId: string;
    outFlightScheduler?: IFlightScheduler

    outSeatId: string;
    outSeat?: Seat;

    inBookingDate?: string;

    inFlightId?: string;
    inFlight?: IFlight

    inFlightSchedulerId?: string;
    inFlightScheduler?: IFlightScheduler

    inSeatId?: string;
    inSeat?: Seat;

}