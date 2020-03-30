using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ServiceBus.Publisher.Services
{
    public class MessageService : IMessageService
    {
        private readonly ILogger<MessageService> _logger;
        private readonly ITopicClient _topicClient;

        public MessageService(ILogger<MessageService> logger, ITopicClient topicClient)
            => (_logger, _topicClient) = (logger, topicClient);

        public async Task Publish<T>(T obj)
        {
            var jsonObject = JsonConvert.SerializeObject(obj);
            var message = new Message(Encoding.UTF8.GetBytes(jsonObject));
            message.UserProperties["messageType"] = typeof(T).Name;
            await _topicClient.SendAsync(message);
        }

        public async Task Publish(string message)
        {
            var msg = new Message(Encoding.UTF8.GetBytes(message));
            msg.UserProperties["messageType"] = "Raw";
            await _topicClient.SendAsync(msg);
        }
    }
}
