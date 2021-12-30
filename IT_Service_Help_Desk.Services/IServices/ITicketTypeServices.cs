using IT_Service_Help_Desk.Dto.TicketStatus;

namespace IT_Service_Help_Desk.Services.IServices;

public interface ITicketTypeServices
{
    IEnumerable<TicketTypeDto> GetAll();
}