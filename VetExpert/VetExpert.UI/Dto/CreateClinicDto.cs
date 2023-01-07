using System.ComponentModel.DataAnnotations;

namespace VetExpert.UI.Dto
{
    public class CreateClinicDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email is not valid.")]
        public string Email { get; set; } = string.Empty;

        public string? WebsiteUrl { get; set; }

        public string? Address { get; set; }

        public Guid ApplicationUserId { get; set; } = Guid.Empty;

        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "Password must have a minimum length of 6.", MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm password is required.")]
        [Compare(nameof(Password), ErrorMessage = "Password and Confirm password are not identical.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
