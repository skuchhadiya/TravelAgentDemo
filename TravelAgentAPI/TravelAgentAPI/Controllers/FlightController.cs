using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelAgentAPI.Business.Providers;
using TravelAgentAPI.DataModels;


namespace TravelAgentAPI.Controllers
{
    [ApiController, Route("[controller]")]
    public class FlightController : ControllerBase
    {

        private readonly IFlightProviderService _service;

        public FlightController(IFlightProviderService service)
        {
            _service = service;
        }
        // GET: Flight
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetFlightsAsync());
        }

        // GET Flight/00000000-0000-0000-0000-000000000000
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _service.GetFlightAsync(id));
        }


        // POST Flight  Body { expecting to pass Flight instance in body }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Flight flight)
        {
            return Ok(await _service.CreateFlightAsync(flight));
        }

    }
}
