using Microsoft.EntityFrameworkCore;

namespace TravelAgentAPI.DataModels
{
    public interface IApplicationDbContext
    {
        public DbSet<Flight> Flights { get; set; }

        public DbSet<ClientType> ClientTypes { get; set; }

        public DbSet<FlightScheduler> FlightSchedulers { get; set; }

        public DbSet<Seat> Seats { get; set; }
        public DbSet<Client> Clients { get; set; }

        public DbSet<FlightBooking> FlightBookings { get; set; }



    }
}