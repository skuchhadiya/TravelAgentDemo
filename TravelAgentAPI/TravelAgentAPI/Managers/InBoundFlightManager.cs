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
    public class InBoundFlightManager : IFlightManager
    {
        private ApplicationDbContext _context;
        public InBoundFlightManager(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async  Task<FlightBooking> CreateAsync(BookingDTO booking)
        {
                return new FlightBooking()
                {
                    FlightType = FlightType.InBound,
                    BookingDate = DateTime.Now,
                    Flight = await _context.Flights.SingleOrDefaultAsync(x => x.Id == booking.InBoudFlightInfo.FlightId),
                    FlightScheduler = await _context.FlightSchedulers.SingleOrDefaultAsync(x => x.Id == booking.InBoudFlightInfo.FlightSchedulerId),
                    Seat = await _context.Seats.SingleOrDefaultAsync(x => x.Id == booking.InBoudFlightInfo.SeatId)
                };
        }

        public async Task<List<FlightBookingSearchInfoDTO>> GetFlightBookingInfoAsync(FlightSearchTermsDTO terms)
        {
            var InBoundFlight = await _context.Flights
                  .Where(x => x.Depature == terms.Arrival && x.Arrival == terms.Depature)
                  .Include(x => x.FlightSchedulers)
                  .ToListAsync();
            
            return InBoundFlight.SelectMany(x =>
            {
                List<FlightBookingSearchInfoDTO> list = new List<FlightBookingSearchInfoDTO>();
                foreach (var scheduler in x.FlightSchedulers)
                {
                    var info = new FlightBookingSearchInfoDTO()
                    {
                        FlightId = x.Id,
                        FlightSchedulerId = scheduler.Id,
                        FlightType = FlightType.InBound,
                        Code = x.Code,
                        Arrival = x.Arrival,
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
            var inBoundFlight = booking.FlightBooking.Find(x => x.FlightType == FlightType.InBound);
            if (inBoundFlight == null) return null;
            return new FlightDetailsDTO
            {
                BookingDate = inBoundFlight.BookingDate,
                Code = inBoundFlight.Flight.Code,
                Depature = inBoundFlight.Flight.Depature,
                DepatureTime = inBoundFlight.FlightScheduler.DepartureTime,
                Arrival = inBoundFlight.Flight.Arrival,
                ArrivalTime = inBoundFlight.FlightScheduler.ArrivalTime,
                JourneyTime = inBoundFlight.FlightScheduler.JourneyTime,
                Seat = inBoundFlight.Seat.SeatNumber,
                Price = inBoundFlight.Flight.Price
            };
        }
    }
}
