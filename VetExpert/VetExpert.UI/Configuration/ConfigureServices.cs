using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VetExpert.UI.Services.Implementations;
using VetExpert.UI.Services.Interfaces;

namespace VetExpert.UI.Configuration
{
	public static class ConfigureServices
	{
		public static IServiceCollection RegisterServices(this IServiceCollection services, IWebAssemblyHostEnvironment hostEnvironment)
		{
			services.AddHttpClient<IClinicService, ClinicService>
			(
				client => client.BaseAddress
				= new Uri(hostEnvironment.BaseAddress)
			);
			services.AddHttpClient<IUserService, UserService>
			(
				client => client.BaseAddress
				= new Uri(hostEnvironment.BaseAddress)
			);
			services.AddHttpClient<IDoctorService, DoctorService>
			(
				client => client.BaseAddress
				= new Uri(hostEnvironment.BaseAddress)
			);
			services.AddHttpClient<IPetService, PetService>
			(
				client => client.BaseAddress
				= new Uri(hostEnvironment.BaseAddress)
			);

			services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(hostEnvironment.BaseAddress) });

			return services;
		}
	}
}
