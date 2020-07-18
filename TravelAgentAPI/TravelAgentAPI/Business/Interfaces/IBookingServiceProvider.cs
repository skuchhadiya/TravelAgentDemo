using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelAgentAPI.DTOS;

namespace TravelAgentAPI.Business.Providers
{
    public interface IBookingServiceProvider
    {
        Task<List<FlightBookingSearchInfoDTO>> GetFlightBookingInfoAsync(FlightSearchTermsDTO terms);
        Task<List<SeatsDTO>> GetFlightSeatsAsync(Guid flightID, Guid flightSchedulerID);
        Task<string> CreateBookingAsync(BookingDTO booking);
    }
}