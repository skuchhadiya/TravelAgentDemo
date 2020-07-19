using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgentAPI.DataModels;
using TravelAgentAPI.DTOS;
using TravelAgentAPI.Enums;

namespace TravelAgentAPI.Managers
{
    public class OutBoundFlightManager : IFlightManager
    {
        private readonly ApplicationDbContext _context;
        public OutBoundFlightManager(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<FlightBooking> CreateAsync(BookingDTO booking)
        {

            var hasSeatBooked = await _context.FlightBookings
                .AnyAsync(x => x.Seat.Id == booking.OutBoudFlightInfo.SeatId
                && x.FlightScheduler.DepartureDateTime == booking.OutBoudFlightInfo.DepartureDateTime);

            if (!hasSeatBooked)
            {
                return new FlightBooking()
                {
                    FlightType = FlightType.OutBound,
                    Flight = await _context.Flights.SingleOrDefaultAsync(x => x.Id == booking.OutBoudFlightInfo.FlightId),
                    FlightScheduler = await _context.FlightSchedulers.SingleOrDefaultAsync(x => x.Id == booking.OutBoudFlightInfo.FlightSchedulerId),
                    Seat = await _context.Seats.SingleOrDefaultAsync(x => x.Id == booking.OutBoudFlightInfo.SeatId)
                };
            }
            else throw new Exception("Seat is already allocated to some one");
        }

        public async Task<List<FlightBookingSearchInfoDTO>> GetFlightBookingInfoAsync(FlightSearchTermsDTO terms)
        {
            var outBoundFlight = await _context.Flights
               .Where(x => x.Depature == terms.Depature && x.Arrival == terms.Arrival)
               .Include(x => x.FlightSchedulers)
               .ToListAsync();

                return  outBoundFlight
                .SelectMany(x =>
                {
                    List<FlightBookingSearchInfoDTO> list = new List<FlightBookingSearchInfoDTO>();
                    foreach ( var scheduler in x.FlightSchedulers)
                    {
                        if (terms.DepatureDate < scheduler.DepartureDateTime)
                        {
                            var info = new FlightBookingSearchInfoDTO()
                            {
                                FlightId = x.Id,
                                FlightSchedulerId = scheduler.Id,
                                FlightType = FlightType.OutBound,
                                Code = x.Code,
                                Arrival = x.Arrival,
                                Depature = x.Depature,
                                DepartureDateTime = scheduler.DepartureDateTime,
                                ArrivalDateTime = scheduler.ArrivalDateTime,
                                JourneyTime = scheduler.JourneyTime,
                                Price = x.Price,
                                SeatId = null
                            };
                            list.Add(info);
                        }

                    };
                    return list;
                }).ToList();
        }

        public FlightDetailsDTO GetFlightDetails(Booking booking)
        {
            var outBoundFlight = booking.FlightBooking.Find(x => x.FlightType == FlightType.OutBound);
            if (outBoundFlight == null) throw new NullReferenceException("Booking must have OutBound Flight");
            
            return new FlightDetailsDTO
            {
                Code = outBoundFlight.Flight.Code,
                Depature = outBoundFlight.Flight.Depature,
                DepatureDateTime = outBoundFlight.FlightScheduler.DepartureDateTime,
                Arrival = outBoundFlight.Flight.Arrival,
                ArrivalDateTime = outBoundFlight.FlightScheduler.ArrivalDateTime,
                JourneyTime = outBoundFlight.FlightScheduler.JourneyTime,
                Seat = outBoundFlight.Seat.SeatNumber,
                Price = outBoundFlight.Flight.Price
            };
        }
    }
}
