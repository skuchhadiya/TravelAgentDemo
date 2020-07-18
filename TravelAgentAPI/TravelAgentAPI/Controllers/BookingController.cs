using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using TravelAgentAPI.Business.Providers;
using TravelAgentAPI.DTOS;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        // GET: api/<BookingController>
        [HttpGet ,Route("Seats/{flightId}/{flightSchedulerId}")]
        public async Task<IActionResult> Get(Guid flightId, Guid flightSchedulerId)
        {
            return Ok(await _service.GetFlightSeatsAsync(flightId, flightSchedulerId));
        }

        // GET api/<BookingController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BookingController>
        [HttpPost, Route("FlightBookingInfo")]
        public async Task<IActionResult> Post([FromBody] FlightSearchTermsDTO terms)
        {
            return Ok(await _service.GetFlightBookingInfoAsync(terms));
        }

        // PUT api/<BookingController>/5
        [HttpPost]
        public async Task<IActionResult> Post( [FromBody] BookingDTO booking)
        {
            return Ok( await  this._service.CreateBookingAsync(booking));
        }

        // DELETE api/<BookingController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
