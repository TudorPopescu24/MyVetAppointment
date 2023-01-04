using VetExpert.Domain;
using VetExpert.Domain.Dto;

namespace VetExpert.UI.Services.Interfaces
{
	public interface IAuthenticationService
	{
		Task<(bool, string)> Login(UserLoginDto user);
        Task<ApplicationUser> RegisterClient(UserLoginDto user);
        Task<(bool, string)> RegisterClinic(UserLoginDto user);
    }
}
