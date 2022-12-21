using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace VetExpert.Domain
{
    public class Clinic
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Name of the clinic is required.")]
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? WebsiteUrl { get; set; }

        public string? Address { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
    }

	public class ClinicValidator : AbstractValidator<Clinic>
	{
		public ClinicValidator()
		{
			RuleFor(x => x.Id).NotEmpty();
			RuleFor(x => x.Name).NotEmpty();
			RuleFor(x => x.Email).EmailAddress();
		}
	}
}
