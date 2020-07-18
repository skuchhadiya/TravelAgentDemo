using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAgentAPI.DataModels
{
    public class Flight : Common
    {
        public string Code { get; set; }

        public string Arrival { get; set; }

        public string Depature { get; set; }

        public int TotalSeats { get; set; }
        public Decimal Price { get; set; }

        public virtual List<FlightScheduler> FlightSchedulers { get; set; }

        public virtual List<Seat> Seats { get; set; }

    }
}
