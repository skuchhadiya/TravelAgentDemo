using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAgentAPI.DataModels
{
    public class FlightScheduler :Common
    {
        [ForeignKey ("Flight")]
        public Guid FlightId { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public string JourneyTime { get; set; }
    }
}
