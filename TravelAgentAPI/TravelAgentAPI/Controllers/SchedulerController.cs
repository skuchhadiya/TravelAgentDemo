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
    public class SchedulerController : ControllerBase
    {
        private readonly IFlightProviderService _service;

        public SchedulerController(IFlightProviderService service)
        {
            _service = service;
        }

        [HttpGet("{flightId}")]
        public async Task<IActionResult> Get(Guid flightId)
        {
            return Ok(await _service.GetFlightSchedulersAsync(flightId));
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FlightScheduler flightScheduler)
        {
            return Ok(await _service.CreateFlightSchedulerAsync(flightScheduler));
        } 
    }
}
