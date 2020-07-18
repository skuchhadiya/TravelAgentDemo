using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgentAPI.DataModels;
using TravelAgentAPI.DTOS;

namespace TravelAgentAPI.Managers
{
    public interface IFlightManager
    {
       Task<FlightBooking> CreateAsync(BookingDTO booking, ApplicationDbContext context);
       Task<List<FlightBookingSearchInfoDTO>> GetFlightBookingInfoAsync(FlightSearchTermsDTO terms, ApplicationDbContext context);
    }
}
