namespace IT_Service_Help_Desk.Database.Entity
{
    public class User : EntityBase
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsEnabled { get; set; }
        public int Id_role { get; set; }

    }
}
