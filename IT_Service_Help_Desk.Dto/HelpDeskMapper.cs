using AutoMapper;
using IT_Service_Help_Desk.Database.Entity;
using IT_Service_Help_Desk.Dto.Role;
using IT_Service_Help_Desk.Dto.TicketStatus;
using IT_Service_Help_Desk.Dto.User;

namespace IT_Service_Help_Desk.Dto;

public class HelpDeskMapper : Profile
{
    public HelpDeskMapper()
    {
        CreateMap<RegisterDto, Database.Entity.User>()
            .ReverseMap();
        CreateMap<RoleDto,Database.Entity.Role>()
            .ReverseMap();
        CreateMap<TicketStatusDto,Tickets_Status>()
            .ReverseMap();
    }
}