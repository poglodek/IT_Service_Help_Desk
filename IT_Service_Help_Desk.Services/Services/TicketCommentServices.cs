using AutoMapper;
using IT_Service_Help_Desk.Database;
using IT_Service_Help_Desk.Dto.Ticket;
using IT_Service_Help_Desk.Dto.TicketComment;
using IT_Service_Help_Desk.Helpers;
using IT_Service_Help_Desk.Services.IServices;
using IT_Service_Help_Desk.Validator;

namespace IT_Service_Help_Desk.Services.Services;

public class TicketCommentServices
{
    private readonly IMapper _mapper;
    private readonly IUserContextServices _contextServices;
    private readonly DatabaseHelper _databaseHelper;
    private readonly DatabaseQueryHelper _queryHelper;
    private readonly DatabaseManagement _databaseManagement;

    public TicketCommentServices(IMapper mapper,
        IUserContextServices contextServices,
        DatabaseHelper databaseHelper,
        DatabaseQueryHelper queryHelper,
        DatabaseManagement databaseManagement)
    {
        _mapper = mapper;
        _contextServices = contextServices;
        _databaseHelper = databaseHelper;
        _queryHelper = queryHelper;
        _databaseManagement = databaseManagement;
    }
    public IEnumerable<TicketCommentDto> GetTicketComments(int ticketId, int page)
    {
        //TODO: Add pagination
        return null;
    }
}