import { ClientTypesEnum } from '../enums/ClientTypesEnum';
import { IFlight, IFlightScheduler, Seat } from './Flight';
import { FlightTypeEnum } from '../enums/FlightTypeEnum';

export interface IClient {
    id: string;
    name: string;
    type: ClientTypesEnum
};

export interface ISeatDTO {
    id: string;
    seatNumber: string;
    flightId: string;
    isBooked: boolean
};

export interface IBookingDTO {
    name: string;
    outBoudFlightInfo: IFlightBookingInfoDTO;
    totalAmount: number;
    inBoudFlightInfo?: IFlightBookingInfoDTO;
}

export interface IFlightSearchTerms {
    type: ClientTypesEnum;
    arrival: string;
    depature: string
}

export interface IFlightBookingInfoDTO {
    flightId: string;
    flightSchedulerId: string;
    flightType: FlightTypeEnum;
    code: string
    arrival: string
    depature: string
    departureTime: string
    arrivalTime: string
    journeyTime: string
    price: string
    seatId: string
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