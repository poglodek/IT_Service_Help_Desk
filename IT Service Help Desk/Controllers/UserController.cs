﻿using IT_Service_Help_Desk.Dto.User;
using IT_Service_Help_Desk.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace IT_Service_Help_Desk.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        
        private readonly IUserServices _services;

        public UserController(IUserServices services)
        {
            _services = services;
        }
        

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] RegisterDto dto)
        {
            _services.RegisterUser(dto);
            return Ok("oki");
        }
    }
}
