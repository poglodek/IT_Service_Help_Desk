using IT_Service_Help_Desk.Services.IServices;
using IT_Service_Help_Desk.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace IT_Service_Help_Desk.Controllers;

[ApiController]
[Route("[controller]")]
public class TicketStatusController : ControllerBase
{
    private readonly ITicketStatusServices _services;

    public TicketStatusController(ITicketStatusServices services)
    {
        _services = services;
    }
    [HttpGet]
    public IActionResult GetAllStatus()
    {
        return Ok(_services.GetAll());
    }
    [HttpPost]
    public IActionResult AddStatus([FromBody] string status)
    {
        return _services.AddStatus(status) ? Created(String.Empty, null) : Conflict();
    }
}