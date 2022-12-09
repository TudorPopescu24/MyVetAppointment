using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
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
            var application = new WebApplicationFactory<DrugsController>()
                .WithWebHostBuilder(builder => { });
            HttpClient = application.CreateClient();
            databaseContext = new MainDbContext(options);
            CleanDatabases();
        }

        protected void CleanDatabases()
        {
               databaseContext.DrugStocks.RemoveRange(databaseContext.DrugStocks.ToList());
               databaseContext.Drugs.RemoveRange(databaseContext.Drugs.ToList());
               databaseContext.SaveChanges();
               databaseContext.Database.EnsureDeleted();
        }
    }
}