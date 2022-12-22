namespace VetExpert.API.Dto
{
    public class CreateAppointmentDto
    {
        public Guid PetId { get; set; }

        public Guid DoctorId { get; set; }

		public DateTime DateTime { get; set; } = DateTime.Now;
	}
}
