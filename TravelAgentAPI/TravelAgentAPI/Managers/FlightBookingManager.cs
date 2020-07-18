using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgentAPI.DataModels;
using TravelAgentAPI.DTOS;
using TravelAgentAPI.Enums;

namespace TravelAgentAPI.Managers
{
    public class FlightBookingManager
    {
        private readonly IFlightManager _outBoundManager;
        private readonly IFlightManager _inBoundManager;
        private ApplicationDbContext _context;
        public FlightBookingManager(ApplicationDbContext context)
        {
            _context = context;
            _outBoundManager = new OutBoundFlightManager(_context);
            _inBoundManager = new InBoundFlightManager(_context);
        }
        public async Task<List<FlightBooking>> CreateAsync(BookingDTO booking)
        {
            var list = new List<FlightBooking>();
            list.Add(await _outBoundManager.CreateAsync(booking));
            if (booking.InBoudFlightInfo != null)
            {
                list.Add(await _inBoundManager.CreateAsync(booking));
            }
            return list;
        }
        public async Task<List<FlightBookingSearchInfoDTO>> GetFlightBookingInfoAsync(FlightSearchTermsDTO terms)
        {
            List<FlightBookingSearchInfoDTO> list = new List<FlightBookingSearchInfoDTO>();
            list.AddRange(await _outBoundManager.GetFlightBookingInfoAsync(terms));
            if (terms.Type == ClientTypes.Return)
            {
                list.AddRange(await _inBoundManager.GetFlightBookingInfoAsync(terms));
            }
            return list;
        }

        public FlightBookingDetailsDTO GetFlightBookingDetails(Booking booking) {

            return new FlightBookingDetailsDTO
            {
                BookingRef = booking.BookingRef,
                Name = booking.Client.Name,
                Booked = booking.BookedDate,
                OutBoundFlight = _outBoundManager.GetFlightDetails(booking),
                InBoundFlight = _inBoundManager.GetFlightDetails(booking),
                TotalAmount =booking.TotalAmount
            };
        }

     }
}
