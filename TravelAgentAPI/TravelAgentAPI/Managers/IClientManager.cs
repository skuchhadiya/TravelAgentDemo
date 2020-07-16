using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgentAPI.DataModels;

namespace TravelAgentAPI.Managers
{
    public interface IClientManager
    {
        public List<Flight> Flights();
    }
}
