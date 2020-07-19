using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgentAPI.Enums;

namespace TravelAgentAPI.DTOS
{
    public class FlightBookingSearchInfoDTO
    {
        public Guid FlightId { get; set; }
        public Guid FlightSchedulerId { get; set; }
        public FlightType FlightType { get; set; }
        public string Code { get; set; }
        public string Arrival { get; set; }
        public string Depature { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public string JourneyTime { get; set; }
        public decimal Price { get; set; }
        public Guid? SeatId { get; set; }
    }
}
