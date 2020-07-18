using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAgentAPI.DataModels
{
    public class ApplicationDbContext: DbContext, IApplicationDbContext
    {
        public DbSet<Flight> Flights { get; set; }

        public DbSet<ClientType> ClientTypes { get; set; }

        public DbSet<FlightScheduler> FlightSchedulers { get; set; }

        public DbSet<Seat> Seats { get; set; }
        public DbSet<Client> Clients { get; set; }

        public DbSet<FlightBooking> FlightBookings { get; set; }

        public DbSet<Booking> Bookings { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                
            modelBuilder.Entity<ClientType>()
                .ToTable("ClientTypes")
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Flight>()
                 .ToTable("Flights")
                 .Property(x => x.Id)
                 .ValueGeneratedOnAdd();

            modelBuilder.Entity<FlightScheduler>()
                .ToTable("FlightSchedulers")
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Seat>()
                .ToTable("Seats")
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Client>()
               .ToTable("Clients")
               .Property(x => x.Id)
               .ValueGeneratedOnAdd();

            modelBuilder.Entity<FlightBooking>()
               .ToTable("FlightBookings")
               .Property(x => x.Id)
               .ValueGeneratedOnAdd();

            modelBuilder.Entity<Booking>()
               .ToTable("Bookings")
               .Property(x => x.Id)
               .ValueGeneratedOnAdd();
        }
    }
}
