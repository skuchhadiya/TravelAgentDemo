using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelAgentAPI.DataModels;

namespace TravelAgentAPI.Business.Providers
{
    public interface IFlightProviderService
    {
        Task<Flight> CreateFlightAsync(Flight flight);
        Task<Flight> GetFlightAsync(Guid id);
        Task<List<Flight>> GetFlightsAsync();
        Task<List<FlightScheduler>> GetFlightSchedulersAsync(Guid flightId);
        Task<FlightScheduler> CreateFlightSchedulerAsync(FlightScheduler flightScheduler);
    }
}