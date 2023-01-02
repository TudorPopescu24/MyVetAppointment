using VetExpert.Domain.Dto;

namespace VetExpert.UI.Services.Interfaces
{
	public interface IAuthenticationService
	{
		Task<(bool, string)> Login(UserLoginDto user);
	}
}
