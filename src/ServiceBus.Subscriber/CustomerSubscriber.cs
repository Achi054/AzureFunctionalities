using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceBus.Contract;

namespace ServiceBus.Subscriber
{
    public class CustomerSubscriber : BackgroundService
    {
        private readonly ILogger<CustomerSubscriber> _logger;
        private readonly ISubscriptionClient _subscriptionClient;

        public CustomerSubscriber(ILogger<CustomerSubscriber> logger, ISubscriptionClient subscriptionClient)
            => (_logger, _subscriptionClient) = (logger, subscriptionClient);

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                _subscriptionClient.RegisterSessionHandler((messageSession, message, token) =>
                {
                    var customer = JsonConvert.DeserializeObject<Customer>(Encoding.UTF8.GetString(message.Body));

                    Console.WriteLine($"Customer with Id {customer.Id} and name {customer.Name} was added.");

                    return Task.CompletedTask;
                },
                new SessionHandlerOptions(args => Task.CompletedTask) { AutoComplete = false });
            }

            return Task.CompletedTask;
        }
    }
}
