using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelAgentAPI.Business.Providers;
using TravelAgentAPI.DataModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        // GET: Flight>
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

 
        // POST Flight
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Flight flight)
        {
            return Ok(await _service.CreateFlightAsync(flight));
        }

    }
}
