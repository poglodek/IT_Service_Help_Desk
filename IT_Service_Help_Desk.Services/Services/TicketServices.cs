using AutoMapper;
using IT_Service_Help_Desk.Database;
using IT_Service_Help_Desk.Database.Entity;
using IT_Service_Help_Desk.Dto.Ticket;
using IT_Service_Help_Desk.Helpers;
using JsonConverters;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace IT_Service_Help_Desk.Services.Services;

public class TicketServices : ITicketServices
{
    private readonly IMapper _mapper;
    private readonly DatabaseHelper _databaseHelper;
    private readonly DatabaseQueryHelper _queryHelper;
    private readonly DatabaseManagement _databaseManagement;

    public TicketServices(IMapper mapper,
        DatabaseHelper databaseHelper,
        DatabaseQueryHelper queryHelper,
        DatabaseManagement databaseManagement)
    {
        _mapper = mapper;
        _databaseHelper = databaseHelper;
        _queryHelper = queryHelper;
        _databaseManagement = databaseManagement;
    }
    public IEnumerable<TicketInListDto> GetAllTicketsFromPage(int page)
    {
        var cmd = new MySqlCommand();
        cmd.CommandText = "SELECT tickets.Id, Title, users.FirstName,users.LastName, tickets_type.TypeName, tickets_status.Status, DateTime FROM `tickets` INNER JOIN tickets_type, tickets_status, users WHERE tickets_status.Id = tickets.Id_tickets_type and tickets_type.Id = tickets.Id_tickets_type and users.Id = tickets.Id_user_Created LIMIT 10 OFFSET @offset";
        cmd.Parameters.AddWithValue("@offset", (Math.Abs(--page)*10));
        var reader = _queryHelper.SendQuery(cmd);
        var str = _databaseHelper.GetJsonFromReader(reader);
        return JsonConvert.DeserializeObject<List<TicketInListDto>>(str);
        
    }
}