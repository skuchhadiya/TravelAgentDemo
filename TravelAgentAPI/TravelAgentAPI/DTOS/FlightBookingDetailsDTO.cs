using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TravelAgentAPI.DTOS;

namespace TravelAgentAPI.DataModels
{
    public class FlightBookingDetailsDTO
    {

        public string BookingRef { get; set; }
        public string Name { get; set; }
        public DateTime Booked { get; set; }
        public FlightDetailsDTO OutBoundFlight { get; set; }
        public FlightDetailsDTO InBoundFlight { get; set; }
        public Decimal TotalAmount { get; set; }

    }
}
