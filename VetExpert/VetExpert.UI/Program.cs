using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VetExpert.UI;
using VetExpert.UI.Services.Implementations;
using VetExpert.UI.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<IClinicService, ClinicService>
	(
		client => client.BaseAddress
		= new Uri(builder.HostEnvironment.BaseAddress)
	);

builder.Services.AddHttpClient<IUserService, UserService>
	(
		client => client.BaseAddress
		= new Uri(builder.HostEnvironment.BaseAddress)
	);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
