using VetExpert.Domain.Dto;

namespace VetExpert.UI.Services.Interfaces
{
	public interface IAuthenticationService
	{
		Task<string> Login(UserLoginDto user);
	}
}
