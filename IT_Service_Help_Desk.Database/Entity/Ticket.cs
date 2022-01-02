namespace IT_Service_Help_Desk.Database.Entity
{
    public class Ticket : EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Id_user_Created { get; set; }
        public int Id_tickets_status { get; set; }
        public DateTime DateTime { get; set; }
        public int Id_tickets_type { get; set; }
    }
}
