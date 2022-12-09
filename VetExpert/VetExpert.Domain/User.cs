using System.ComponentModel.DataAnnotations;

namespace VetExpert.Domain
{
    public class User
    {
        public Guid Id { get; set; }

		[Required(ErrorMessage = "Name is required.")]
		public string Name { get; set; }

        public string Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();

        public User()
        {
            Id=Guid.NewGuid();
        }
		public static implicit operator User(Clinic v)
		{
			throw new NotImplementedException();
		}
	}
}
