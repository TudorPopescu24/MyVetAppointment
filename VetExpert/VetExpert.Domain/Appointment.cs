namespace VetExpert.Domain
{
	public class Appointment
	{
		public Guid Id { get; set; }

		public DateTime DateTime { get; set; } = DateTime.Now;

		public bool IsConfirmed { get; set; } = false;

		public Guid UserId { get; set; } = Guid.Empty;

		public virtual User User { get; set; } = new User();

		public Guid PetId { get; set; } = Guid.Empty;

		public virtual Pet Pet { get; set; } = new Pet();

		public Guid ClinicId { get; set; } = Guid.Empty;

		public virtual Clinic Clinic { get; set; } = new Clinic();

		public Guid DoctorId { get; set; } = Guid.Empty;

		public virtual Doctor Doctor { get; set; } = new Doctor();

		public Appointment()
		{
			Id = Guid.NewGuid();
		}
	}
}