using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TravelAgentAPI.Enums;

namespace TravelAgentAPI.DataModels
{
    public class FlightBooking: Common
    {
        [ForeignKey("Booking")]
        public Guid BookingID { get; set; }
        public FlightType FlightType { get; set; }
        public DateTime BookingDate { get; set; }
        public virtual Flight Flight { get; set; }
        public virtual FlightScheduler FlightScheduler { get; set; }
        public virtual Seat Seat { get; set; }
    }
}
