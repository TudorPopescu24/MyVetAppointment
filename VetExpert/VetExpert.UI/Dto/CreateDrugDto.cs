using System.ComponentModel.DataAnnotations;

namespace VetExpert.UI.Dto
{
	public class CreateDrugDto
	{
		public Guid Id { get; set; }
		public Guid ClinicId { get; set; } = Guid.Empty;

		[Required(ErrorMessage = "Name is required.")]
		public string Name { get; set; } = string.Empty;

		public string Manufacturer { get; set; } = string.Empty;

		public int Weight { get; set; } = 0;

		public string Prospect { get; set; } = string.Empty;

		[Required(ErrorMessage = "Price is required.")]
		public int Price { get; set; } = 0;
	}
}
