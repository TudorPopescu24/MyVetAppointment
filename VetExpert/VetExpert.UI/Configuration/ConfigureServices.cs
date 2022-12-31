using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VetExpert.UI.Authentication;
using VetExpert.UI.Services.Implementations;
using VetExpert.UI.Services.Interfaces;

namespace VetExpert.UI.Configuration
{
	public static class ConfigureServices
	{
		public static IServiceCollection RegisterServices(this IServiceCollection services, IWebAssemblyHostEnvironment hostEnvironment)
		{
			services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
			services.AddAuthorizationCore();

			services.AddBlazoredLocalStorage();

			services.AddScoped<IAuthenticationService, AuthenticationService>();
			services.AddScoped<IClinicService, ClinicService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IDoctorService, DoctorService>();
			services.AddScoped<IPetService, PetService>();

			services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(hostEnvironment.BaseAddress) });

			return services;
		}
	}
}
