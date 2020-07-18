using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgentAPI.DataModels;

namespace TravelAgentAPI.DTOS
{
    public class SeatsDTO:Seat
    {
        public bool IsBooked {get;set;}
    }
}
