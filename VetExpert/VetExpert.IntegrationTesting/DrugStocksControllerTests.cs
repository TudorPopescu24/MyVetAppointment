using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;
using VetExpert.API.Dto;
using Xunit;

namespace VetExpert.IntegrationTesting
{
    [Collection("Database tests")]
    public class DrugStocksControllerTests : IDisposable
    {
        private const string ApiURL = "/api/drugstocks";

        [Fact]
        public async void When_CreatedDrugStock_Then_ShouldReturnDrugStockInTheGetRequest()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            CreateDrugStockDto drugStockDto = CreateSUT();
 
            var getDrugStockResult = await http_client.GetStringAsync(ApiURL);
 
            var drugStocks = JsonConvert.DeserializeObject<List<CreateDrugStockDto>>(getDrugStockResult);

            drugStocks.Count.Should().Be(1);
            drugStocks.Should().HaveCount(1);
            drugStocks.Should().NotBeNull();
            
        }

        private static CreateDrugStockDto CreateSUT()
        {
            // Arrange
            return new CreateDrugStockDto
            {
                DrugId= Guid.NewGuid(),
                Quantity = 12,
                ExpirationDate = new DateTime(2023, 1, 23)
            };
        }

        public void Dispose()
        {
           // CleanDatabases();
        }
    }
}
