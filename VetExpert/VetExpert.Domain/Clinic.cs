using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace VetExpert.Domain
{
    public class Clinic
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name of the clinic is required.")]
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? WebsiteUrl { get; set; }

        public string? Address { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

        public Guid ApplicationUserId { get; set; } = Guid.Empty;

        public virtual ApplicationUser ApplicationUser { get; set; } = new ApplicationUser();

        public Clinic()
        {
            Id = Guid.NewGuid();
        }
    }
}
