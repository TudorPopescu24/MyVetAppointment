namespace VetExpert.Domain
{
	public class Appointment
	{
		public Guid Id { get; set; }

		public DateTime DateTime { get; set; }

		public Guid PetId { get; set; }

		public virtual Pet Pet { get; set; }

		public Guid DoctorId { get; set; }

		public virtual Doctor Doctor { get; set; }

		public Appointment()
		{
			DateTime = DateTime.Now();
			Id= Guid.NewGuid();
		}
	}
}