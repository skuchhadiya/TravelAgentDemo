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
    public class InBoudFlightManager : IFlightManager
    {
        public async  Task<FlightBooking> CreateAsync(BookingDTO booking, ApplicationDbContext context)
        {
                return new FlightBooking()
                {
                    FlightType = FlightType.InBound,
                    BookingDate = DateTime.Now,
                    Flight = await context.Flights.SingleOrDefaultAsync(x => x.Id == booking.InBoudFlightInfo.FlightId),
                    FlightScheduler = await context.FlightSchedulers.SingleOrDefaultAsync(x => x.Id == booking.InBoudFlightInfo.FlightSchedulerId),
                    Seat = await context.Seats.SingleOrDefaultAsync(x => x.Id == booking.InBoudFlightInfo.SeatId)
                };
        }

        public async Task<List<FlightBookingSearchInfoDTO>> GetFlightBookingInfoAsync(FlightSearchTermsDTO terms, ApplicationDbContext context)
        {
            var InBoundFlight = await context.Flights
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
    }
}
