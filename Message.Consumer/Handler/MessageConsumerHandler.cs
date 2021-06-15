using Message.Consumer.Model;
using Message.Domain;
using Message.Producer;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Rebus.Bus;
using Rebus.Handlers;
using System.Threading.Tasks;

namespace Message.Consumer.Handler
{
    public class MessageConsumerHandler : IHandleMessages<Person>
    {
        private readonly ILogger _logger;
        private readonly IMessageService _service;
        private readonly IBus _bus;

        public MessageConsumerHandler(ILogger<MessageConsumerHandler> logger, IMessageService service, IBus bus)
        {
            _logger = logger;
            _service = service;
            _bus = bus;
        }

        public async Task Handle(Person message)
        {
            await _service.Process(message);
        }
    }
}
