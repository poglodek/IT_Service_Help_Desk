using IT_Service_Help_Desk.Dto.Ticket;

namespace IT_Service_Help_Desk.Validator.Ticket;

public class CreateTicketDtoValidator : IValid<CreateTicketDto>
{
    public (bool, string) IsValid(CreateTicketDto obj)
    {
        if (string.IsNullOrWhiteSpace(obj.Description) || obj.Description.Length < 5)
            return (false, "Description is too short");
        if(obj.Description.Length > 500)
            return (false, "Description is too long");
        if (obj.DateTime.Date < DateTime.Now.Date.AddYears(-50) || obj.DateTime.Date > DateTime.Now.Date.AddYears(1))
            return (false, "Date is invalid");
        if(string.IsNullOrWhiteSpace(obj.Title) || obj.Title.Length < 5)
            return (false, "Title is too short");
        if(obj.Title.Length > 50)
            return (false, "Title is too long");
        return (true,"Object Valid");
    }
}