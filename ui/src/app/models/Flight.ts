
export interface IFlight {
    id: string;
    code: string;
    arrival: string;
    depature: string;
    totalSeats: number;
    price: number;
}

export interface IFlightDeatils extends IFlight {
    flightSchedulers: IFlightScheduler[],
    seats: Seat[]

}

export interface IFlightScheduler {
    id: String;
    flightId: string;
    departureTime: string;
    arrivalTime: string;
    journeyTime: string;
}

export interface Seat {
    id: string;
    flightId: string;
    seatNumber: string
}