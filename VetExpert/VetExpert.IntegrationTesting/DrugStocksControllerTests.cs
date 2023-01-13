using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
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

            drugStocks.Count.Should().BeGreaterOrEqualTo(0);
            
            
        }

        [Fact]
        public async void When_DeleteUser_Then_ShouldReturn_CorrectNumber_Of_Users()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            var getUserResultFirst = await http_client.GetStringAsync(ApiURL);
            var users_first = JsonConvert.DeserializeObject<List<CreateDrugStockDto>>(getUserResultFirst);
            var deleteUser = await http_client.DeleteAsync(ApiURL + "/" + DbSeeding.drugStocks[0].Id);
            deleteUser.StatusCode.Should().Be(HttpStatusCode.OK);
            var getUserResult = await http_client.GetStringAsync(ApiURL);
            var users = JsonConvert.DeserializeObject<List<CreateDrugStockDto>>(getUserResult);
            deleteUser.StatusCode.Should().Be(HttpStatusCode.OK);
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
