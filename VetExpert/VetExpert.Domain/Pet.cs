using System.ComponentModel.DataAnnotations;

namespace VetExpert.Domain
{
    public class Pet
    {
        public Guid Id { get; set; } = Guid.NewGuid();

		[Required(ErrorMessage = "Name is required.")]
		public string Name { get; set; } = string.Empty;

        public string TypeOfPet { get; set; } = string.Empty;

        public int Age { get; set; } = 0;

        public int Weight { get; set; } = 0;

        public bool IsVaccinated { get; set; } = false;
	
		public DateTime? DateOfVaccine { get; set; }

        public Guid UserId { get; set; } = Guid.Empty;

        public virtual User User { get; set; } = new User();
    }
}
