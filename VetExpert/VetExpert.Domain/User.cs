using System.ComponentModel.DataAnnotations;

namespace VetExpert.Domain
{
    public class User
    {
        public Guid Id { get; set; }

		[Required(ErrorMessage = "Name is required.")]
		public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; } = string.Empty;

		public string? PhoneNumber { get; set; }

		public string? Address { get; set; }

		public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();

		public Guid ApplicationUserId { get; set; } = Guid.Empty;

		public virtual ApplicationUser ApplicationUser { get; set; } = new ApplicationUser();

		public User()
		{
			Id = Guid.NewGuid();
		}
	}
}
