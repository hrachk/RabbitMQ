using MassTransit;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System;
using System.Threading.Tasks;
using Tiarm.RabbitMQ.Consumer;

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

        [HttpGet]
        public async Task<ActionResult> GetMessage()
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")

            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            var message = QueueConsumer.Consume(channel) ;
            return new JsonResult(message);
        }
    }
}
