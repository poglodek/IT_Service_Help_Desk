namespace IT_Service_Help_Desk.Database.Entity;

public class Tickets_Comments : EntityBase
{
    
    public int Id_user { get; set; }
    public int Id_Tickets { get; set; }
    public DateTime  DateTime { get; set; }
    public string  Comment { get; set; }
}