using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgentAPI.DataModels;
using TravelAgentAPI.DTOS;

namespace TravelAgentAPI.Business.Providers
{
    public class FlightProviderService : IFlightProviderService
    {

        private readonly ApplicationDbContext context;

        public FlightProviderService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Flight>> GetFlightsAsync()
        {
            var result = await context.Flights.ToListAsync();
            return result.Select(x => this.GetFlightDTO(x)).ToList();
        }

        public async Task<Flight> GetFlightAsync(Guid Id)
        {
            var result = await context.Flights.Where(x => x.Id == Id).SingleOrDefaultAsync();
            return this.GetFlightDTO(result);
        }

        public async Task<Flight> CreateFlightAsync(Flight flight)
        {
            var addedFlight = context.Flights.Add(new Flight()
            {
                Code = flight.Code,
                Arrival = flight.Arrival,
                Depature = flight.Depature,
                TotalSeats =flight.TotalSeats,
                Price = flight.Price,
            });
            await context.SaveChangesAsync();
            await AutoGenerateSetsAsync(addedFlight.Entity);
            return this.GetFlightDTO(addedFlight.Entity);

        }

        public async Task<List<FlightScheduler>> GetFlightSchedulersAsync(Guid flightId)
        {
            var result = await context.FlightSchedulers.Where( x=> x.FlightId == flightId).ToListAsync();
            return result.Select(x => this.GetFlightSchedulerDTO(x)).ToList();
        }

        //public async Task<List<SeatsDTO>> GetFlightSeatsAsync(Guid flightId, Guid flightSchedulerID)
        //{
        //    var result = await context.Seats.Where(x => x.FlightId == flightId).ToListAsync();
        
        //}


        public async Task<FlightScheduler> CreateFlightSchedulerAsync(FlightScheduler flightScheduler)
        {
           
            var addedFlightScheduler = context.FlightSchedulers.Add(new FlightScheduler()
            {
                FlightId = flightScheduler.FlightId,
                DepartureTime = flightScheduler.DepartureTime,
                ArrivalTime = flightScheduler.ArrivalTime,
                JourneyTime = flightScheduler.JourneyTime,
            });
            await context.SaveChangesAsync();
            return this.GetFlightSchedulerDTO(addedFlightScheduler.Entity);

        }

        //Good to Create DTO Object not to send memory reference 
        private Flight GetFlightDTO(Flight flight)
        {
            //repeating code
            return new Flight
            {
                Id = flight.Id,
                Code = flight.Code,
                Arrival = flight.Arrival,
                Depature = flight.Depature,
                Price = flight.Price,
                FlightSchedulers = flight.FlightSchedulers,
                Seats = flight.Seats,
            };
        }

        private FlightScheduler GetFlightSchedulerDTO(FlightScheduler flightScheduler)
        {
            //repeating code
            return new FlightScheduler
            {
                Id = flightScheduler.Id,
                FlightId = flightScheduler.FlightId,
                DepartureTime = flightScheduler.DepartureTime,
                ArrivalTime = flightScheduler.ArrivalTime,
                JourneyTime = flightScheduler.JourneyTime,
            };
        }

        private async Task AutoGenerateSetsAsync(Flight flight)
        {
            List<Seat> seats = new List<Seat>();
            for(var i =0; i < flight.TotalSeats; i++)
            {
                var seat = new Seat() { FlightId = flight.Id, SeatNumber = (i+1).ToString() };
                seats.Add(seat);
            }
            context.AddRange(seats);
            await context.SaveChangesAsync();
        } 
    }
}
