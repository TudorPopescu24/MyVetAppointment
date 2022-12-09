using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;
using VetExpert.API.Dto;
using Xunit;

namespace VetExpert.Testing
{
    public class DrugStocksControllerTests : BaseIntegrationTests, IDisposable
    {
        private const string ApiURL = "/api/drugstocks";

        [Fact]
        public async void When_CreatedDrugStock_Then_ShouldReturnDrugStockInTheGetRequest()
        {
            CreateDrugStockDto drugStockDto = CreateSUT();
            // Act
            var createDrugStockResponse = await HttpClient.PostAsJsonAsync(ApiURL, drugStockDto);
            var getDrugStockResult = await HttpClient.GetStringAsync(ApiURL);
            // Assert
            createDrugStockResponse.EnsureSuccessStatusCode();
            createDrugStockResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            //object value = getDrugStockResult.EnsureSuccessStatusCode();

            /*      var drugs = await getDrugResult.Content
                      .ReadFromJsonAsync<List<CreateDrugDto>>();*/
            var drugStocks = JsonConvert.DeserializeObject<List<CreateDrugStockDto>>(getDrugStockResult);

            drugStocks.Count.Should().Be(1);
            drugStocks.Should().HaveCount(1);
            drugStocks.Should().NotBeNull();
            CleanDatabases();
        }

        private static CreateDrugStockDto CreateSUT()
        {
            // Arrange
            return new CreateDrugStockDto
            {
                Quantity = 12,
                ExpirationDate = new DateTime(2023, 1, 23)
            };
        }

        public void Dispose()
        {
            CleanDatabases();
        }
    }
}
