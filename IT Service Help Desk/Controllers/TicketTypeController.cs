using IT_Service_Help_Desk.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace IT_Service_Help_Desk.Controllers;

[ApiController]
[Route("[controller]")]
public class TicketTypeController : ControllerBase
{
    private readonly ITicketTypeServices _services;

    public TicketTypeController(ITicketTypeServices services)
    {
        _services = services;
    }
    [HttpGet]
    public IActionResult GetAllStatus()
    {
        return Ok(_services.GetAll());
    }
    [HttpPost]
    public IActionResult AddType([FromBody] string type)
    {
        return _services.AddType(type) ? Created(String.Empty, null) : Conflict();
    }
}