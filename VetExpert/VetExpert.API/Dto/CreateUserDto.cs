namespace VetExpert.API.Dto
{
    public class CreateUserDto
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

		public string? PhoneNumber { get; set; }

        public string? Address { get; set; }
    }
}
