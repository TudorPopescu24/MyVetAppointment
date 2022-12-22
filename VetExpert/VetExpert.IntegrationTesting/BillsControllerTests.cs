using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;
using VetExpert.API.Dto;
using Xunit;

namespace VetExpert.IntegrationTesting
{
    [Collection("Database tests")]
    public class BillControllerTests :  IDisposable
    {
        private const string ApiURL = "/api/Bills";

       
        [Fact]
        public async void When_CreatedBill_Then_ShouldReturnBillInTheGetRequest()
        {
            //CleanDatabases();
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            CreateBillDto BillDto = CreateSUT();
            // Act
            var createBillResponse = await http_client.PostAsJsonAsync(ApiURL, BillDto);
            var getBillResult = await http_client.GetStringAsync(ApiURL);
            // Assert
            createBillResponse.EnsureSuccessStatusCode();
            createBillResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);



            var Bills = JsonConvert.DeserializeObject<List<CreateBillDto>>(getBillResult);

            Bills.Count.Should().Be(1);
            Bills.Should().HaveCount(1);
            Bills.Should().NotBeNull();

            Dispose();
            
        }

        private static CreateBillDto CreateSUT()
        {
            // Arrange
            return new CreateBillDto
            {
                Currency = "lei",
                DateTime = new DateTime(),
                Value=100

            };
        }

        public void Dispose()
        {
            //CleanDatabases();
        }
    }
}
