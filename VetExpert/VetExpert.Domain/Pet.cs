using System.ComponentModel.DataAnnotations;

namespace VetExpert.Domain
{
    public class Pet
    {
        public Guid Id { get; set; }

		[Required(ErrorMessage = "Name is required.")]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = "Type of pet is required.")]
		public string TypeOfPet { get; set; } = string.Empty;

		[Required(ErrorMessage = "Age is required.")]
		public int Age { get; set; } = 0;

		[Required(ErrorMessage = "Weight is required.")]
		public int Weight { get; set; } = 0;

        public bool IsVaccinated { get; set; } = false;
	
		public DateTime? DateOfVaccine { get; set; }

        public Guid UserId { get; set; } = Guid.Empty;

        public virtual User User { get; set; } = new User();

        public Pet()
        {
            Id = Guid.NewGuid();
        }
    }
}
