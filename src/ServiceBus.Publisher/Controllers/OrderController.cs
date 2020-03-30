using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceBus.Contract;
using ServiceBus.Publisher.Services;

namespace ServiceBus.Publisher.Controllers
{
    [ApiController]
    [Route("order")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IMessageService _messageService;

        public OrderController(ILogger<OrderController> logger, IMessageService messageService)
            => (_logger, _messageService) = (logger, messageService);

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Order order)
        {
            await _messageService.Publish(order);
            return Ok();
        }
    }
}
