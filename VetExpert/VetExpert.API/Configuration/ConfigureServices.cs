using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VetExpert.Domain;
using VetExpert.Infrastructure;

namespace VetExpert.API.Configuration
{
	public static class ConfigureServices
	{
		public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddMediatR(Assembly.GetExecutingAssembly());
			services.AddFluentValidationAutoValidation();
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

			services.AddCors(options =>
			{
				options.AddPolicy("vetExpertCors",
					policy =>
					{
						policy.WithOrigins("https://localhost:7116",
											"http://localhost:5116",
											"https://localhost:44338")
											.AllowAnyHeader()
											.AllowAnyMethod();
					});
			});

			services.AddDbContext<MainDbContext>(options => options.UseSqlite(configuration.GetConnectionString("MyVetAppointmentDb"), b => b.MigrationsAssembly(typeof(MainDbContext).Assembly.FullName)));
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

			return services;
		}
	}
}
