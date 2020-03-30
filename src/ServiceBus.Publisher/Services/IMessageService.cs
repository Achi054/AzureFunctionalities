using System.Threading.Tasks;

namespace ServiceBus.Publisher.Services
{
    public interface IMessageService
    {
        Task Publish<T>(T obj);

        Task Publish(string message);
    }
}
