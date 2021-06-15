using Message.Consumer.Model;
using Message.Producer;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Message.Domain
{
    public class MessageService : IMessageService
    {
        private readonly ILogger _logger;
        private readonly IPublisher _publisher;
        private readonly IMongoDatabase _database;

        public MessageService(ILogger logger, IPublisher publisher)
        {
            _logger = logger;
            _publisher = publisher;
            string connectionString = "mongodb://mongo:mongo@localhost:27017";
            var mongoClient = new MongoClient(connectionString);
            _database = mongoClient.GetDatabase("MessageDb");
        }

        public async Task Process(Person messageObject)
        {
            _logger.LogInformation("String consumer received message: {message}", messageObject);

            Person finalMessage = messageObject;
            if(messageObject.ArchitetureType == ArchitetureEnum.DatabaseState)
            {
                // save in database 
                IMongoCollection<Person> messageEventCollection = _database.GetCollection<Person>("MessageEvent");
                finalMessage = messageEventCollection.Find(p => p.Id == messageObject.Id).FirstOrDefault();
                if (!(finalMessage is null))
                {
                    _logger.LogInformation("String consumer - saving message to mongo.");
                    finalMessage.UpdatedAt = DateTime.Now;
                    messageEventCollection.InsertOne(messageObject);
                    _logger.LogInformation("String consumer - message saved to db");
                }
            }
            else
            {
                // save in database without failure
            }

            bool shouldFail = messageObject.ArchitetureType == ArchitetureEnum.DatabaseState;

            await _publisher.Send(finalMessage, shouldFail);
        }
    }
}
