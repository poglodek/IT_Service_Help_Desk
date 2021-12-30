namespace IT_Service_Help_Desk.Dto.Ticket;

public class TicketInListDto : BaseDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Status { get; set; }
    public string TypeName { get; set; }
    public DateTime DateTime { get; set; }
    
}