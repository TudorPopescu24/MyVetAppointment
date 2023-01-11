using System.ComponentModel.DataAnnotations;

namespace VetExpert.UI.Dto
{
	public class CreateDoctorDto
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "First Name is required.")]
		public string FirstName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Last Name is required.")]
		public string LastName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;

		public Guid ClinicId { get; set; } = Guid.Empty;
	}
}
