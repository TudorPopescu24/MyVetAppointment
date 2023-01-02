using System.Text.Json.Serialization;
using VetExpert.API.Configuration;
using VetExpert.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
	.AddJsonOptions(x =>
		x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.RegisterServices(builder.Configuration);
builder.Services.AddApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("vetExpertCors");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }