﻿namespace MyVetAppointment.API.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public ICollection<Animal>? Animals { get; set; }
    }
}
