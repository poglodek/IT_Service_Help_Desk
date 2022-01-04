using Microsoft.AspNetCore.Mvc;

namespace IT_Service_Help_Desk.Controllers;

[ApiController]
[Route("[controller]")]
public class TicketCommentController : ControllerBase
{
    
    [HttpGet]
    public IActionResult GetCommentFromTicketId([FromQuery] int ticketId, [FromQuery] int page = 0)
    {
        return Ok();
    }
}