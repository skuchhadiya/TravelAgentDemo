using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAgentAPI.DTOS
{
    public class BookingDTO
    {
         public string Name { get; set; }
         public Decimal TotalAmount { get; set;}
         public FlightBookingSearchInfoDTO OutBoudFlightInfo { get; set; }
         public FlightBookingSearchInfoDTO InBoudFlightInfo { get; set; }
    }
}
