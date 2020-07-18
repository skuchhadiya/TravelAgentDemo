using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TravelAgentAPI.Enums;

namespace TravelAgentAPI.DataModels
{
    public class Booking :Common
    {
        public string BookingRef { get; set; }
        public ClientTypes Type { get; set; }
        public DateTime BookedDate { get; set; }

        [ForeignKey("Client")]
        public Guid ClientID { get; set; }
        public virtual Client Client { get; set; }
        public virtual List<FlightBooking> FlightBooking { get; set; }
        public bool IsPaymentSuccessful { get; set; } 
        public Decimal TotalAmount { get; set; }

    }
}
