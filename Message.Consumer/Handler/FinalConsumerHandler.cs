using Message.Consumer.Model;
using Microsoft.Extensions.Logging;
using Rebus.Handlers;
using System.Threading.Tasks;

namespace Message.Consumer.Handler
{
    public class FinalConsumerHandler : IHandleMessages<Person>
    {
        private readonly ILogger _logger;

        public FinalConsumerHandler(ILogger<FinalConsumerHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(Person message)
        {
            _logger.LogInformation("Final consumer received message: {message}", message);

            return Task.CompletedTask;
        }
    }
}
