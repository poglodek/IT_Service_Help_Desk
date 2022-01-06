using IT_Service_Help_Desk.Dto.TicketComment;

namespace IT_Service_Help_Desk.Services.IServices;

public interface ITicketCommentServices
{
    IEnumerable<TicketCommentDto> GetTicketComments(int ticketId, int page);
}