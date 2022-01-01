using AutoMapper;
using IT_Service_Help_Desk.Database;
using IT_Service_Help_Desk.Database.Entity;
using IT_Service_Help_Desk.Dto;
using IT_Service_Help_Desk.Dto.TicketStatus;
using IT_Service_Help_Desk.Services.IServices;

namespace IT_Service_Help_Desk.Services.Services;

public class TicketStatusServices : ITicketStatusServices
{
    private readonly IMapper _mapper;
    private readonly DatabaseManagement _management;

    public TicketStatusServices(IMapper mapper,
        DatabaseManagement management)
    {
        _mapper = mapper;
        _management = management;
    }
    public IEnumerable<TicketStatusDto> GetAll()
    {
        var ticketStatus = _management.GetResultsFromQuery<Tickets_Status>("SELECT * FROM Tickets_Status");
        return _mapper.Map<IEnumerable<TicketStatusDto>>(ticketStatus);
    }

    public bool AddStatus(string status)
    {
        var model = new Tickets_Status
        {
            Status = status
        };
        _management.InsertObject<Tickets_Status>("Tickets_Status", model);
        return true;
    }
}