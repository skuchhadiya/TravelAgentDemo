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
       Task<FlightBooking> CreateAsync(BookingDTO booking);
       Task<List<FlightBookingSearchInfoDTO>> GetFlightBookingInfoAsync(FlightSearchTermsDTO terms);
        FlightDetailsDTO GetFlightDetails(Booking booking);
    }
}
