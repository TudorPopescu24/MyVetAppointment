namespace VetExpert.Domain
{
	public class Appointment
	{
		public Guid Id { get; set; } = Guid.NewGuid();

		public DateTime DateTime { get; set; } = DateTime.Now;

		public Guid PetId { get; set; } = Guid.Empty;

		public virtual Pet Pet { get; set; } = new Pet();

		public Guid DoctorId { get; set; } = Guid.Empty;

		public virtual Doctor Doctor { get; set; } = new Doctor();
	}
}