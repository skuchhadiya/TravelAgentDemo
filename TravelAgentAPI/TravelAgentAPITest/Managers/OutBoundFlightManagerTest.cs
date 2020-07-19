using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TravelAgentAPI.DataModels;
using TravelAgentAPI.DTOS;
using TravelAgentAPI.Enums;
using TravelAgentAPI.Managers;

namespace TravelAgentAPITest.Managers
{
    [TestClass]
    public class OutBoundFlightManagerTest
    {
        private readonly ApplicationDbContext _context;

        public OutBoundFlightManagerTest()
        {
            _context = DummayApplicationContext.GetContextInstance();
        }
        [TestMethod]
        public void CreateAsync_It_Should_returnError()
        {
            this.SeedFlightBooking();

            OutBoundFlightManager outManager = new OutBoundFlightManager(_context);
            var bookingDTO = new BookingDTO()
            {
                Name = "XYZ",
                TotalAmount = 450,
                OutBoudFlightInfo = new FlightBookingSearchInfoDTO()
                {
                    FlightId = new Guid("f377e6dc-4916-45eb-9546-075afd407532"),
                    FlightSchedulerId = new Guid("134e11b6-ba9e-44d7-1253-08d82c033a6b"),
                    FlightType = FlightType.OutBound,
                    Code = "BA452",
                    Arrival = "London(Any)",
                    Depature = "Manchester(MAN)",
                    DepartureDateTime = Convert.ToDateTime("2020-07-19 17:45:00.0000000"),
                    ArrivalDateTime = Convert.ToDateTime("2020-07-19 20:45:00.0000000"),
                    JourneyTime = "2 hours, 0 minutes",
                    Price = 450,
                    SeatId = new Guid("870171d8-0466-4ffc-3e41-08d82c0569f4"),
                },
                InBoudFlightInfo = null
            };
           

            var result = outManager.CreateAsync(bookingDTO).Exception;

            Assert.AreEqual("One or more errors occurred. (Seat is already allocated to some one)", result.Message);
            this._context.Dispose();

        }

        [TestMethod]
        public void CreateAsync_It_Should_return_FlightBooking()
        {
            this.SeedFlightBooking();

            OutBoundFlightManager outManager = new OutBoundFlightManager(_context);
            var bookingDTO = new BookingDTO()
            {
                Name = "XYZ",
                TotalAmount = 450,
                OutBoudFlightInfo = new FlightBookingSearchInfoDTO()
                {
                    FlightId = new Guid("f377e6dc-4916-45eb-9546-075afd407532"),
                    FlightSchedulerId = new Guid("134e11b6-ba9e-44d7-1253-08d82c033a6b"),
                    FlightType = FlightType.OutBound,
                    Code = "BA452",
                    Arrival = "London(Any)",
                    Depature = "Manchester(MAN)",
                    DepartureDateTime = Convert.ToDateTime("2020-07-19 17:45:00.0000000"),
                    ArrivalDateTime = Convert.ToDateTime("2020-07-19 20:45:00.0000000"),
                    JourneyTime = "2 hours, 0 minutes",
                    Price = 450,
                    SeatId = new Guid("fc1d788d-27ab-4a8f-3e42-08d82c0569f4"),
                },
                InBoudFlightInfo = null
            };


            var expect = new FlightBooking()
            {
                FlightType = FlightType.OutBound,
                Flight = new Flight()
                {
                    Id = new Guid("f377e6dc-4916-45eb-9546-075afd407532"),
                    Code = "BA452",
                    Arrival = "London(Any)",
                    Depature = "Manchester(MAN)",
                    TotalSeats = 2,
                    Price = 450
                },
                FlightScheduler = new FlightScheduler()
                {
                    Id = new Guid("134e11b6-ba9e-44d7-1253-08d82c033a6b"),
                    FlightId = new Guid("f377e6dc-4916-45eb-9546-075afd407532"),
                    DepartureDateTime = Convert.ToDateTime("2020-07-19 17:45:00.0000000"),
                    ArrivalDateTime = Convert.ToDateTime("2020-07-19 20:45:00.0000000"),
                    JourneyTime = "2 hours, 0 minutes"
                },
                Seat = new Seat()
                {
                    Id = new Guid("870171d8-0466-4ffc-3e41-08d82c0569f4"),
                    FlightId = new Guid("f377e6dc-4916-45eb-9546-075afd407532"),
                    SeatNumber = "A1"
                }
            };

            var result = outManager.CreateAsync(bookingDTO).Result;

            Assert.AreEqual(result.Flight.Id, expect.Flight.Id);
            Assert.AreEqual(result.FlightScheduler.Id, expect.FlightScheduler.Id);

        }
        private void  SeedFlightBooking()
        {
            
            var flightId =new Guid("f377e6dc-4916-45eb-9546-075afd407532");
            var scheduler = new FlightScheduler()
            {
                Id = new Guid("134e11b6-ba9e-44d7-1253-08d82c033a6b"),
                FlightId = flightId,
                DepartureDateTime = Convert.ToDateTime("2020-07-19 17:45:00.0000000"),
                ArrivalDateTime = Convert.ToDateTime("2020-07-19 20:45:00.0000000"),
                JourneyTime = "2 hours, 0 minutes"
            };

            var seat = new Seat()
            {
                Id = new Guid("870171d8-0466-4ffc-3e41-08d82c0569f4"),
                FlightId = flightId,
                SeatNumber = "A1"
            };

            var seat2 = new Seat()
            {
                Id = new Guid("fc1d788d-27ab-4a8f-3e42-08d82c0569f4"),
                FlightId = flightId,
                SeatNumber = "A2"
            };

            List<FlightScheduler> schedulerList = new List<FlightScheduler>();
            schedulerList.Add(scheduler);

            List<Seat> listseats = new List<Seat>();
            listseats.Add(seat);
            listseats.Add(seat2);

            var flight = new Flight()
            {
                Id = flightId,
                Code = "BA452",
                Arrival = "London(Any)",
                Depature = "Manchester(MAN)",
                TotalSeats = 2,
                Price = 450,
                FlightSchedulers = schedulerList,
                Seats = listseats,
            };

            var flightbooking = new FlightBooking()
            {
                Id = Guid.NewGuid(),
                BookingID = Guid.NewGuid(),
                Flight = flight,
                FlightScheduler = scheduler,
                Seat = seat,
            };

            List<FlightBooking> blist = new List<FlightBooking>();
            blist.Add(flightbooking);

             _context.FlightBookings.AddRange(blist);
            _context.SaveChanges();
            
        }
    }
}
