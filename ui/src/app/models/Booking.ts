import { ClientTypesEnum } from '../enums/ClientTypesEnum';
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

export interface FlightBookingDetailsDTO {
    bookingRef: string;
    name: string;
    booked: Date,
    outBoundFlight: FlightDetailsDTO;
    inBoundFlight: FlightDetailsDTO;
    TotalAmount: number;
}

export interface FlightDetailsDTO {
    bookingDate: Date;
    code: string;
    depature: string;
    depatureDateTime: Date;
    arrival: string;
    arrivalDateTime: Date;
    journeyTime: string;
    seat: string;
    price: number

}

export interface IFlightSearchTerms {
    type: ClientTypesEnum;
    arrival: string;
    depature: string;
    depatureDate: Date;
    returnDate: Date;

}

export interface IFlightBookingInfoDTO {
    flightId: string;
    flightSchedulerId: string;
    flightType: FlightTypeEnum;
    code: string;
    arrival: string;
    depature: string;
    departureDateTime: Date;
    arrivalDateTime: Date;
    journeyTime: string;
    price: string;
    seatId: string;
}