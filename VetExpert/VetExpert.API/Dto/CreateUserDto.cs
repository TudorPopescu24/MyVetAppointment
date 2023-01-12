﻿namespace VetExpert.API.Dto
{
    public class CreateUserDto
    {
        public object Id;

        //public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

		public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public Guid ApplicationUserId { get; set; }
    }
}
