using System.ComponentModel.DataAnnotations;

namespace VetExpert.Domain
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

		[Required(ErrorMessage = "Name is required.")]
		public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

		public string? PhoneNumber { get; set; }

		public string? Address { get; set; }

		public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();
	}
}
