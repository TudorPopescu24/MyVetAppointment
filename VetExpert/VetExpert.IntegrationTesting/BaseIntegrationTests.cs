using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using VetExpert.API.Controllers;
using VetExpert.Infrastructure;

namespace VetExpert.Testing
{
    public class BaseIntegrationTests
    {
        private DbContextOptions<MainDbContext> options = new DbContextOptionsBuilder<MainDbContext>()
                .UseSqlite("Data Source = MyTests.db").Options;

        protected HttpClient HttpClient { get; private set; }

        private MainDbContext databaseContext;

        protected BaseIntegrationTests()
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder => { });
            HttpClient = application.CreateClient();
            databaseContext = new MainDbContext(options);
            CleanDatabases();
        }

        protected void CleanDatabases()
        {
            databaseContext.Users.RemoveRange(databaseContext.Users.ToList());
            databaseContext.Admins.RemoveRange(databaseContext.Admins.ToList());
            databaseContext.Pets.RemoveRange(databaseContext.Pets.ToList());
            databaseContext.Clinics.RemoveRange(databaseContext.Clinics.ToList());
            databaseContext.Appointments.RemoveRange(databaseContext.Appointments.ToList());
            databaseContext.SaveChanges();
            

        }
    }
}