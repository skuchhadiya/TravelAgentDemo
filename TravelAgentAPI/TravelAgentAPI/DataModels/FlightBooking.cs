using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAgentAPI.DataModels
{
    public class FlightBooking :Common
    {
        public string BookingRef { get; set; }

        public DateTime BookedDate { get; set; }

        [ForeignKey("Client")]
        public Guid ClientID { get; set; }
        public virtual Client Client { get; set; }

        public DateTime OutBookingDate { get; set; }

        [ForeignKey("Flight")]
        public Guid OutFlightId { get; set; }
        
        public virtual Flight OutFlight { get; set; }

        [ForeignKey("FlightScheduler")]
        public Guid OutFlightSchedulerId { get; set; }
        public virtual FlightScheduler OutFlightScheduler { get; set; }

        
        [ForeignKey("Seat")]
        public Guid OutSeatId { get; set; }
        public virtual Seat OutSeat { get; set; }


        public DateTime? InBookingDate { get; set; }

        [ForeignKey("FlightScheduler")]
        public Guid? InSchedulerId { get; set; }
        public virtual FlightScheduler InScheduler { get; set; }


        [ForeignKey("Flight")]
        public Guid? InFlightId { get; set; }
        public virtual Flight InFlight { get; set; }


        [ForeignKey("Seat")]
        public Guid? InSeatId { get; set; }
        public virtual Seat InSeat { get; set; }

    }
}
