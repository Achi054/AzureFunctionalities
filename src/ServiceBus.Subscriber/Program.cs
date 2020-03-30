using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ServiceBus.Subscriber
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var config = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: false)
                        .Build();

                    services.AddSingleton<ISubscriptionClient>(
                        new SubscriptionClient(
                            config.GetSection("ServiceBus:ConnectionString").Value,
                            config.GetSection("ServiceBus:TopicName").Value,
                            config.GetSection("ServiceBus:SubscriptionName").Value));

                    services.AddHostedService<CustomerSubscriber>();
                });
    }
}
