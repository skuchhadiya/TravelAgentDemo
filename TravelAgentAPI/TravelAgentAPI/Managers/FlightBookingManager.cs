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
        private  ApplicationDbContext _context;
        public FlightBookingManager(ApplicationDbContext context)
        {
            _context = context;
            _outBoundManager = new OutBoundFlightManager();
            _inBoundManager = new InBoudFlightManager();
        }
        public async Task<List<FlightBooking>> CreateAsync(BookingDTO booking)
        {
            var list = new List<FlightBooking>();
            list.Add(await _outBoundManager.CreateAsync(booking, _context));
            if (booking.InBoudFlightInfo != null)
            {
                list.Add(await _inBoundManager.CreateAsync(booking, this._context));
            }
            return list;
        }
        public async Task<List<FlightBookingSearchInfoDTO>> GetFlightBookingInfoAsync(FlightSearchTermsDTO terms)
        {
            List<FlightBookingSearchInfoDTO> list = new List<FlightBookingSearchInfoDTO>();
            list.AddRange(await _outBoundManager.GetFlightBookingInfoAsync(terms, _context));
            if(terms.Type == ClientTypes.Return)
            {
                list.AddRange(await _inBoundManager.GetFlightBookingInfoAsync(terms, _context));
            }
            return list;
        }
    }
}
