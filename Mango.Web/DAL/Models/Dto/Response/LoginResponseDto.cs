﻿namespace Mango.Web.DAL.Models.Dto.Response
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
