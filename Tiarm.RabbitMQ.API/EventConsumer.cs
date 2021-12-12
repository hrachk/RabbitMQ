using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Tiarm.RabbitMQ.API.Controllers;

namespace Tiarm.RabbitMQ.API
{
    class EventConsumer :
         IConsumer<ValueEntered>
    {
        ILogger<EventConsumer> _logger;

        public EventConsumer(ILogger<EventConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ValueEntered> context)
        {
            _logger.LogInformation("Value: {Value}", context.Message.Value);
        }
    }
}