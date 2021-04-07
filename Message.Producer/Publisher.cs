using Microsoft.Extensions.Logging;
using Rebus.Bus;
using System;
using System.Threading.Tasks;

namespace Message.Producer
{
    public class Publisher : IPublisher
    {
        private readonly IBus _bus;
        private readonly ILogger<IBus> _logger;

        public Publisher(IBus bus, ILogger<IBus> logger)
        {
            this._bus = bus;
            this._logger = logger;
        }

        public async Task Send(object obj)
        {
            _logger.LogInformation("Publisher - About to publish the message to rabbitmq topic.");
            
            await _bus.Publish(obj);

            _logger.LogInformation("Publisher - Published the message to the topic.");
        }
    }
}
