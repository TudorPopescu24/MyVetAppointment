namespace VetExpert.API.Dto
{
    public class CreateAppointmentDto
    {
        public Guid PetId { get; set; }

        public Guid ClinicId { get; set; }

        public Guid UserId { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;
	}
}
