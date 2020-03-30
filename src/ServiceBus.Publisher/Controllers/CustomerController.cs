using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceBus.Contract;
using ServiceBus.Publisher.Services;

namespace ServiceBus.Publisher.Controllers
{
    [ApiController]
    [Route("customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IMessageService _messageService;

        public CustomerController(ILogger<CustomerController> logger, IMessageService messageService)
            => (_logger, _messageService) = (logger, messageService);

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            await _messageService.Publish(customer);
            return Ok();
        }
    }
}
