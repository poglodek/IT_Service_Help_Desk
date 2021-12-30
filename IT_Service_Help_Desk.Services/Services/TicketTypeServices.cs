using AutoMapper;
using IT_Service_Help_Desk.Database;
using IT_Service_Help_Desk.Database.Entity;
using IT_Service_Help_Desk.Dto.TicketStatus;
using IT_Service_Help_Desk.Services.IServices;

namespace IT_Service_Help_Desk.Services.Services;

public class TicketTypeServices : ITicketTypeServices
{
    private readonly IMapper _mapper;
    private readonly DatabaseManagement _management;
    
    public TicketTypeServices(IMapper mapper,
        DatabaseManagement management)
    {
        _mapper = mapper;
        _management = management;
    }
    public IEnumerable<TicketTypeDto> GetAll()
    {
        var ticketStatus = _management.GetResultsFromQuery<Tickets_Type>("SELECT * FROM Tickets_Type");
        return _mapper.Map<IEnumerable<TicketTypeDto>>(ticketStatus);
    }
}