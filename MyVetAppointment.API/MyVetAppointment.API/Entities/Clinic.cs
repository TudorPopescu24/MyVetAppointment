﻿namespace MyVetAppointment.API.Entities
{
    public class Clinic
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string? WebsiteUrl { get; set; }

        public string? Address { get; set; }

        public ICollection<Doctor> Doctors { get; set; }
    }
}
