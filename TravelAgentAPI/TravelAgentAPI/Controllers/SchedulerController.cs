using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelAgentAPI.Business.Providers;
using TravelAgentAPI.DataModels;


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

        // GET: Scheduler/{flightId}
        [HttpGet("{flightId}")]
        public async Task<IActionResult> Get(Guid flightId)
        {
            return Ok(await _service.GetFlightSchedulersAsync(flightId));
        }

        // Post: Scheduler Body { expecting to pass FlightScheduler instance in body }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FlightScheduler flightScheduler)
        {
            return Ok(await _service.CreateFlightSchedulerAsync(flightScheduler));
        } 
    }
}
