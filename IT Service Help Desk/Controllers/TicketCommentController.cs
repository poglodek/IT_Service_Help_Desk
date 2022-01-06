using IT_Service_Help_Desk.Services.IServices;
using IT_Service_Help_Desk.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace IT_Service_Help_Desk.Controllers;

[ApiController]
[Route("[controller]")]
public class TicketCommentController : ControllerBase
{
    private readonly ITicketCommentServices _commentServices;

    public TicketCommentController(ITicketCommentServices commentServices)
    {
        _commentServices = commentServices;
    }
    [HttpGet]
    public IActionResult GetCommentFromTicketId([FromQuery] int ticketId, [FromQuery] int page = 0)
    {
        return Ok(_commentServices.GetTicketComments(ticketId,page));
    }
}