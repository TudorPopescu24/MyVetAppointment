namespace VetExpert.API.Dto
{
    public class CreateClinicDto
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? WebsiteUrl { get; set; }

        public string? Address { get; set; }

        public Guid ApplicationUserId { get; set; }
    }
}
