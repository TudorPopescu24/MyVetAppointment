﻿namespace VetExpert.API.Dto
{
    public class CreateAppointmentDto
    {
        public Guid PetId { get; set; }

        public Guid ClinicId { get; set; }

        public Guid UserId { get; set; }

        public Guid DoctorId { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;

        public bool IsConfirmed { get; set; }

        public string Details { get; set; } = string.Empty;
    }
}
