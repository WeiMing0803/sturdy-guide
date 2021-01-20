using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Contract.Entity;
using Contract.Event;
using MassTransit;
using System;

namespace MassTransitRabbitMq.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;
        public ValuesController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<ActionResult> Post(string department)
        {
            await _publishEndpoint.Publish<IOffDutyEvent>(new 
            {
                ClosingTime = DateTime.Now,
                Department = department
            });
            return Ok();
        }
    }
}
