using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using VetExpert.Infrastructure;
using FluentValidation;
using System.Reflection;
using System;
using VetExpert.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
	.AddJsonOptions(x =>
		x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.AddDbContext<MainDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("MyVetAppointmentDb"), b=>b.MigrationsAssembly(typeof(MainDbContext).Assembly.FullName)));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddCors(options =>
{
	options.AddPolicy("vetExpertCors",
		policy =>
		{
			policy.WithOrigins("https://localhost:7116",
								"http://localhost:5116")
								.AllowAnyHeader()
								.AllowAnyMethod();
		});
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IValidator<Clinic>, ClinicValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("vetExpertCors");

app.UseAuthorization();

app.MapControllers();

app.Run();


public partial class Program { }
