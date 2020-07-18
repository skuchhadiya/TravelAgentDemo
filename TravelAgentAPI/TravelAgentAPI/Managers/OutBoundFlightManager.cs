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
        public async Task<FlightBooking> CreateAsync(BookingDTO booking, ApplicationDbContext context)
        {
                return new FlightBooking()
                {
                    FlightType = FlightType.OutBound,
                    BookingDate = DateTime.Now,
                    Flight = await context.Flights.SingleOrDefaultAsync(x => x.Id == booking.InBoudFlightInfo.FlightId),
                    FlightScheduler = await context.FlightSchedulers.SingleOrDefaultAsync(x => x.Id == booking.InBoudFlightInfo.FlightSchedulerId),
                    Seat = await context.Seats.SingleOrDefaultAsync(x => x.Id == booking.InBoudFlightInfo.SeatId)
                };
        }

        public async Task<List<FlightBookingSearchInfoDTO>> GetFlightBookingInfoAsync(FlightSearchTermsDTO terms, ApplicationDbContext context)
        {
            var outBoundFlight = await context.Flights
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
    }
}
