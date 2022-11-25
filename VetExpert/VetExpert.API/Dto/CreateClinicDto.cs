namespace VetExpert.API.Dto
{
    public class CreateClinicDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string? WebsiteUrl { get; set; }

        public string? Address { get; set; }
    }
}
