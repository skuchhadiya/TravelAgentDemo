using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAgentAPI.DataModels
{
    public class Seat :Common
    {
        [ForeignKey("Flight")]
        public Guid FlightId { get; set; }
        public string SeatNumber { get; set; }

    }
}
