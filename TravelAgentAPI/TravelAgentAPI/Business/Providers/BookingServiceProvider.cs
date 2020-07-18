using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgentAPI.DataModels;
using TravelAgentAPI.DTOS;
using TravelAgentAPI.Enums;
using TravelAgentAPI.Managers;

namespace TravelAgentAPI.Business.Providers
{
    public class BookingServiceProvider : IBookingServiceProvider
    {
        private readonly ApplicationDbContext _context;
        private FlightBookingManager _flightBookingManager;

        public BookingServiceProvider(ApplicationDbContext context)
        {
            this._context = context;
            this._flightBookingManager = new FlightBookingManager(context);

        }

        public async Task<FlightBookingDetailsDTO> CreateBookingAsync(BookingDTO booking)
        {
            var newbooking = new Booking
            {
                BookingRef = (DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + booking.Name),
                Type = booking.InBoudFlightInfo != null ? ClientTypes.Return : ClientTypes.OneWay,
                BookedDate = DateTime.Now,
                Client = new Client { Name = booking.Name },
                FlightBooking = await _flightBookingManager.CreateAsync(booking),
                IsPaymentSuccessful = true,
                TotalAmount = booking.TotalAmount

            };
            var result = this._context.Bookings.Add(newbooking);
            await _context.SaveChangesAsync();

            return _flightBookingManager.GetFlightBookingDetails(result.Entity);

        } 

        public async Task<List<FlightBookingSearchInfoDTO>> GetFlightBookingInfoAsync(FlightSearchTermsDTO terms)
        {
            return await _flightBookingManager.GetFlightBookingInfoAsync(terms);

        }

        public async Task<List<SeatsDTO>> GetFlightSeatsAsync(Guid flightID, Guid flightSchedulerID)
        {
            var allocatedSeats = await _context.FlightBookings
                .Where(x => x.FlightScheduler.Id == flightSchedulerID)
                .Select(x => x.Seat).ToListAsync();
            var totalSeats = await _context.Seats.Where(x => x.FlightId == flightID).ToListAsync();

            var result = totalSeats.Select(x => new SeatsDTO()
            {
                Id = x.Id,
                FlightId =x.FlightId,
                SeatNumber = x.SeatNumber,
                IsBooked = allocatedSeats.Any(y => y.Id == x.Id)

            }).ToList();
            return result;
        }

    }
}
