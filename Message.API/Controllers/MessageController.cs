using Message.Consumer.Model;
using Message.Producer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Message.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<MessageController> _logger;
        private readonly IPublisher _publisher;
        public MessageController(ILogger<MessageController> logger, IPublisher publisher)
        {
            _logger = logger;
            _publisher = publisher;
        }

        [HttpPost]
        public Dictionary<bool, string> Post(Person model)
        {
            _publisher.Send(model);
            return new Dictionary<bool, string>();
        }
    }
}
