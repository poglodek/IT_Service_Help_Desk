using Microsoft.AspNetCore.Mvc;
using ILogger = IT_Service_Help_Desk.Services.IServices.ILogger;

namespace IT_Service_Help_Desk.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ILogger _logger;

        public TicketController(ILogger logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInfo("Test");
            return Ok("okii");
        }
    }
}
