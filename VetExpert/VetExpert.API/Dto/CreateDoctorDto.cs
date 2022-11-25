namespace VetExpert.API.Dto
{
    public class CreateDoctorDto
    {
        public Guid ClinicId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
