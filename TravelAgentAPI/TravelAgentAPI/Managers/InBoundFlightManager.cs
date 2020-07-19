using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TravelAgentAPI.DataModels;
using TravelAgentAPI.DTOS;
using TravelAgentAPI.Enums;

namespace TravelAgentAPI.Managers
{
    public class InBoundFlightManager : IFlightManager
    {
        private readonly ApplicationDbContext _context;
        public InBoundFlightManager(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async  Task<FlightBooking> CreateAsync(BookingDTO booking)
        {
           var hasSeatBooked = await _context.FlightBookings
                .AnyAsync(x =>  x.Seat.Id== booking.InBoudFlightInfo.SeatId 
                && x.FlightScheduler.DepartureDateTime == booking.InBoudFlightInfo.DepartureDateTime );

           if (!hasSeatBooked)
            {
                return new FlightBooking()
                {
                    FlightType = FlightType.InBound,
          
                    Flight = await _context.Flights.SingleOrDefaultAsync(x => x.Id == booking.InBoudFlightInfo.FlightId),
                    FlightScheduler = await _context.FlightSchedulers.SingleOrDefaultAsync(x => x.Id == booking.InBoudFlightInfo.FlightSchedulerId),
                    Seat = await _context.Seats.SingleOrDefaultAsync(x => x.Id == booking.InBoudFlightInfo.SeatId)
                };
            }
            else throw new DuplicateNameException("Seat is already allocated to some one");
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
                    if(terms.ReturnDate >  scheduler.ArrivalDateTime && terms.DepatureDate < scheduler.DepartureDateTime)
                    {
                        var info = new FlightBookingSearchInfoDTO()
                        {
                            FlightId = x.Id,
                            FlightSchedulerId = scheduler.Id,
                            FlightType = FlightType.InBound,
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
            var inBoundFlight = booking.FlightBooking.Find(x => x.FlightType == FlightType.InBound);
            if (inBoundFlight == null) return null;
            return new FlightDetailsDTO
            {
                Code = inBoundFlight.Flight.Code,
                Depature = inBoundFlight.Flight.Depature,
                DepatureDateTime = inBoundFlight.FlightScheduler.DepartureDateTime,
                Arrival = inBoundFlight.Flight.Arrival,
                ArrivalDateTime = inBoundFlight.FlightScheduler.ArrivalDateTime,
                JourneyTime = inBoundFlight.FlightScheduler.JourneyTime,
                Seat = inBoundFlight.Seat.SeatNumber,
                Price = inBoundFlight.Flight.Price
            };
        }
    }
}
