namespace IT_Service_Help_Desk.Dto.Ticket;

public class TicketDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Status { get; set; }
    public DateTime DateTime { get; set; }
    public string TypeName { get; set; }
}