using Microsoft.AspNetCore.Mvc;

namespace IT_Service_Help_Desk.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : ControllerBase
    {
        public TicketController()
        {
            
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("okii");
        }
    }
}
