﻿namespace VetExpert.API.Dto
{
    public class CreateUserDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }
    }
}
