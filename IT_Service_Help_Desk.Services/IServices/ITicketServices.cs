﻿using IT_Service_Help_Desk.Dto.Ticket;

namespace IT_Service_Help_Desk.Services.Services;

public interface ITicketServices
{
    IEnumerable<TicketInListDto> GetAllTicketsFromPage(int page);
}