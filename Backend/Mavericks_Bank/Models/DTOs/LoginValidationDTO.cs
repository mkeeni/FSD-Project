﻿namespace Mavericks_Bank.Models.DTOs
{
    public class LoginValidationDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string Token { get; set; }
    }
}
