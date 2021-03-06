using IT_Service_Help_Desk.Dto.Ticket;
using IT_Service_Help_Desk.Services.Services;
using Microsoft.AspNetCore.Mvc;


namespace IT_Service_Help_Desk.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketServices _services;

        public TicketController(ITicketServices services)
        {
            _services = services;
        }   
        [HttpGet]
        public IActionResult Index([FromQuery] int page = 0 )
        {
            var tickets = _services.GetAllTicketsFromPage(page);
            return Ok(tickets);
        }
        [HttpGet("{id}")]
        public IActionResult GetTicketById(int id)
        {
            var ticket = _services.GetTicketById(id);
            return Ok(ticket);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteTicketById(int id)
        {
            _services.DeleteTicketById(id);
            return Ok();
        }
        [HttpPost]
        public IActionResult CreateTicket([FromBody] CreateTicketDto ticket)
        {
            return _services.CreateTicket(ticket) ? Created(string.Empty,null) : Conflict();
        }

    }
}
