using AutoMapper;
using IT_Service_Help_Desk.Database;
using IT_Service_Help_Desk.Dto.Ticket;
using IT_Service_Help_Desk.Dto.TicketComment;
using IT_Service_Help_Desk.Exception;
using IT_Service_Help_Desk.Helpers;
using IT_Service_Help_Desk.Services.IServices;
using IT_Service_Help_Desk.Validator;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace IT_Service_Help_Desk.Services.Services;

public class TicketCommentServices : ITicketCommentServices
{
    private readonly IMapper _mapper;
     readonly IUserContextServices _contextServices;
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
        var cmd = new MySqlCommand();
        cmd.CommandText = "SELECT users.FirstName, users.LastName, users.Email, tickets_comments.Comment, tickets_comments.DateTime FROM tickets_comments INNER JOIN users WHERE tickets_comments.Id_user = users.Id  and tickets_comments.Id_Tickets = @id  LIMIT 10 OFFSET @offset;";
        cmd.Parameters.AddWithValue("@offset", (Math.Abs(--page)*10));
        cmd.Parameters.AddWithValue("@id", ticketId);
        var reader = _queryHelper.SendQuery(cmd);
        var str = _databaseHelper.GetJsonFromReader(reader);
        if(str is null)
            throw new NotFoundException("No comments found");
        return JsonConvert.DeserializeObject<IEnumerable<TicketCommentDto>>(str);
    }
}