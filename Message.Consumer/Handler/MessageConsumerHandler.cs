using Message.Consumer.Model;
using Message.Producer;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Rebus.Bus;
using Rebus.Handlers;
using System.Threading.Tasks;

namespace Message.Consumer.Handler
{
    public class MessageConsumerHandler : IHandleMessages<string>
    {
        private readonly ILogger _logger;
        private readonly IPublisher _publisher;
        private readonly IBus _bus;

        public MessageConsumerHandler(ILogger<MessageConsumerHandler> logger, IPublisher publisher, IBus bus)
        {
            _logger = logger;
            _publisher = publisher;
            _bus = bus;
        }

        public Task Handle(string message)
        {
            _logger.LogInformation("String consumer received message: {message}", message);

            string connectionString = "mongodb://mongo:mongo@localhost:27017";
            var client = new MongoClient(connectionString);

            IMongoDatabase db = client.GetDatabase("MessageDb");
            IMongoCollection<object> messageEventCollection = db.GetCollection<object>("MessageEvent");

            _logger.LogInformation("String consumer - saving message to mongo.");
            Person finalmessage = new Person()
            {
                Name = "Person",
                LastName = "Last stop"
            };
            messageEventCollection.InsertOne(message);
            _logger.LogInformation("String consumer - message saved to db");

            _publisher.Send(finalmessage);

            return Task.CompletedTask;
        }
    }
}
