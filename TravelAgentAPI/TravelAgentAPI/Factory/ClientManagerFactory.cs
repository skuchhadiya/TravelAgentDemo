using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgentAPI.DataModels;
using TravelAgentAPI.Enums;
using TravelAgentAPI.Managers;

namespace TravelAgentAPI.Factory
{
    public class ClientManagerFactory
    {
        public dynamic GetClientManeger(ClientTypes type)
        {
            if (ClientTypes.OneWay == type)
                return new OneWayClientManager();
            else if (ClientTypes.Return == type)
                return new ReturnClientManager();
            return null;

        }
    }
}
