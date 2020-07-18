using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelAgentAPI.Business.Providers;
using TravelAgentAPI.DTOS;


namespace TravelAgentAPI.Controllers
{
    [ApiController, Route("[controller]")]
    public class BookingController : ControllerBase
    {

        private IBookingServiceProvider _service;
        public BookingController(IBookingServiceProvider service)
        {
            _service = service;
        }

        // GET: Booking/Seats/{flightId}/{flightSchedulerId}
        [HttpGet ,Route("Seats/{flightId}/{flightSchedulerId}")]
        public async Task<IActionResult> Get(Guid flightId, Guid flightSchedulerId)
        {
            return Ok(await _service.GetFlightSeatsAsync(flightId, flightSchedulerId));
        }

        // POST Booking/FlightBookingInfo    Body { expecting to pass FlightSearchTermsDTO instance in body  }
        [HttpPost, Route("FlightBookingInfo")]
        public async Task<IActionResult> Post([FromBody] FlightSearchTermsDTO terms)
        {
            return Ok(await _service.GetFlightBookingInfoAsync(terms));
        }

        //Post  Booking     Body { expecting to pass BookingDTO instance in body  }
        [HttpPost]
        public async Task<IActionResult> Post( [FromBody] BookingDTO booking)
        {
            return Ok( await  this._service.CreateBookingAsync(booking));
        }
    }
}
