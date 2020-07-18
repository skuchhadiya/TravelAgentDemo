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
        private ApplicationDbContext _context;
        public OutBoundFlightManager(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<FlightBooking> CreateAsync(BookingDTO booking)
        {
                return new FlightBooking()
                {
                    FlightType = FlightType.OutBound,
                    BookingDate = DateTime.Now,
                    Flight = await _context.Flights.SingleOrDefaultAsync(x => x.Id == booking.OutBoudFlightInfo.FlightId),
                    FlightScheduler = await _context.FlightSchedulers.SingleOrDefaultAsync(x => x.Id == booking.OutBoudFlightInfo.FlightSchedulerId),
                    Seat = await _context.Seats.SingleOrDefaultAsync(x => x.Id == booking.OutBoudFlightInfo.SeatId)
                };
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
                        var info = new FlightBookingSearchInfoDTO()
                        {
                            FlightId = x.Id,
                            FlightSchedulerId = scheduler.Id,
                            FlightType = FlightType.OutBound,
                            Code = x.Code,
                            Arrival =x.Arrival,
                            Depature = x.Depature,
                            DepartureTime = scheduler.DepartureTime,
                            ArrivalTime = scheduler.ArrivalTime,
                            JourneyTime = scheduler.JourneyTime,
                            Price = x.Price,
                            SeatId = null
                        };
                        list.Add(info);

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
                BookingDate = outBoundFlight.BookingDate,
                Code = outBoundFlight.Flight.Code,
                Depature = outBoundFlight.Flight.Depature,
                DepatureTime = outBoundFlight.FlightScheduler.DepartureTime,
                Arrival = outBoundFlight.Flight.Arrival,
                ArrivalTime = outBoundFlight.FlightScheduler.ArrivalTime,
                JourneyTime = outBoundFlight.FlightScheduler.JourneyTime,
                Seat = outBoundFlight.Seat.SeatNumber,
                Price = outBoundFlight.Flight.Price
            };
        }
    }
}
