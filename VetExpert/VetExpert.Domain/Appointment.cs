namespace VetExpert.Domain
{
	public class Appointment
	{
		public int Id { get; set; }

		public DateTime DateTime { get; set; }

		public Pet Pet { get; set; }

		public Doctor Doctor { get; set; }

		public Clinic Clinic { get; set; }
	}
}