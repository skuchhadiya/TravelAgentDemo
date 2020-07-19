using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAgentAPI.DTOS
{
    public class FlightDetailsDTO
    {

        public DateTime BookingDate { get; set; }
        public string Code { get; set; }
        public string Depature { get; set; }
        public DateTime DepatureDateTime { get; set; }
        public string Arrival { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public string JourneyTime { get; set; }
        public string  Seat { get; set; }
        public Decimal Price { get; set; }
    }
}
