using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgentAPI.DataModels;

namespace TravelAgentAPI.Managers
{
    public class OneWayClientManager : IClientManager
    {
        public List<Flight> Flights()
        {
            return new List<Flight>();
        }
    }
}
