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
        public virtual Flight Flight { get; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public string JourneyTime { get; set; }
    }
}
