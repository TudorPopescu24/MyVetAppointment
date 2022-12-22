namespace VetExpert.API.Dto
{
    public class CreateDoctorDto
    {
        public Guid ClinicId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
