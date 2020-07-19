using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgentAPI.Enums;

namespace TravelAgentAPI.DTOS
{
    public class FlightSearchTermsDTO
    {
        public ClientTypes Type { get; set; }
        public string Arrival { get; set; }
        public string Depature { get; set; }
        public DateTime DepatureDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
