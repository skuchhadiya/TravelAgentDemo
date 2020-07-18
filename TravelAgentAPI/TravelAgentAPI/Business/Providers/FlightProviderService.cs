using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using TravelAgentAPI.DataModels;
using TravelAgentAPI.DTOS;

namespace TravelAgentAPI.Business.Providers
{
    public class FlightProviderService : IFlightProviderService
    {

        private readonly ApplicationDbContext _context;

        public FlightProviderService(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<List<Flight>> GetFlightsAsync()
        {
            var result = await _context.Flights.ToListAsync();
            return result.Select(x => this.GetFlightDTO(x)).ToList();
        }

        public async Task<Flight> GetFlightAsync(Guid Id)
        {
            var result = await _context.Flights.Where(x => x.Id == Id).SingleOrDefaultAsync();
            return this.GetFlightDTO(result);
        }

        public async Task<Flight> CreateFlightAsync(Flight flight)
        {
            var addedFlight = _context.Flights.Add(new Flight()
            {
                Code = flight.Code,
                Arrival = flight.Arrival,
                Depature = flight.Depature,
                TotalSeats =flight.TotalSeats,
                Price = flight.Price,
            });
            await _context.SaveChangesAsync();
            await AutoGenerateSetsAsync(addedFlight.Entity);
            return this.GetFlightDTO(addedFlight.Entity);

        }

        public async Task<List<FlightScheduler>> GetFlightSchedulersAsync(Guid flightId)
        {
            var result = await _context.FlightSchedulers.Where( x=> x.FlightId == flightId).ToListAsync();
            return result.Select(x => this.GetFlightSchedulerDTO(x)).ToList();
        }


        public async Task<FlightScheduler> CreateFlightSchedulerAsync(FlightScheduler flightScheduler)
        {
           
            var addedFlightScheduler = _context.FlightSchedulers.Add(new FlightScheduler()
            {
                FlightId = flightScheduler.FlightId,
                DepartureTime = flightScheduler.DepartureTime,
                ArrivalTime = flightScheduler.ArrivalTime,
                JourneyTime = flightScheduler.JourneyTime,
            });
            await _context.SaveChangesAsync();
            return this.GetFlightSchedulerDTO(addedFlightScheduler.Entity);

        }

        private Flight GetFlightDTO(Flight flight)
        {
            //repeating code
            return new Flight
            {
                Id = flight.Id,
                Code = flight.Code,
                Arrival = flight.Arrival,
                Depature = flight.Depature,
                TotalSeats =flight.TotalSeats,
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
            //Assuming there should be a standard for total seats in flight
            List<Seat> seats = new List<Seat>();
            if(flight.TotalSeats == 152)
            {
                //38=152/4
                var rows = new[] { "A", "B", "C", "D" };
                seats.AddRange(this.GetRowSeats(flight.Id, rows, 38));
            }
            else if (flight.TotalSeats == 360)
            {
                //60=360/6
                var rows = new[] { "A", "B", "C", "D", "E","F", };
                seats.AddRange(this.GetRowSeats(flight.Id, rows, 60));
            }
            else if (flight.TotalSeats == 760)
            {
                //38=660/6
                var rows = new[] { "A", "B", "C", "D", "E", "F", "G", "H","I","J" };
                seats.AddRange(this.GetRowSeats(flight.Id, rows, 76));
            }
            _context.AddRange(seats);
            await _context.SaveChangesAsync();
        }
        private List<Seat> GetRowSeats( Guid flightId, string[] rows, int rowLength)
        {
            List<Seat> seats = new List<Seat>();
            foreach (var range in rows)
            {
                for (var i = 0; i < rowLength; i++)
                {
                    var seat = new Seat() { FlightId = flightId, SeatNumber = range + (i + 1).ToString() };
                    seats.Add(seat);
                }
            }
            return seats;

        }
    }
}
