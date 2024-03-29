﻿using System.ComponentModel.DataAnnotations;

namespace VetExpert.Domain
{
    public class Doctor
    {
        public Guid Id { get; set; }

		[Required(ErrorMessage = "First Name is required.")]
		public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;

        public Guid ClinicId { get; set; } = Guid.Empty;

        public virtual Clinic Clinic { get; set; } = new Clinic();

        public virtual ICollection<DoctorSpecialization> DoctorSpecializations { get; set; } = new List<DoctorSpecialization>();

        public Doctor()
        {
            Id = Guid.NewGuid();
        }
    }
}
