using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Tiarm.RabbitMQ.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        readonly IPublishEndpoint  publishEndpoint;
        public MessageController(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]string value)
        {
            await publishEndpoint.Publish<ValueEntered>(new
            {
                Value = value
            });

            return Ok();
        }
    }
}
