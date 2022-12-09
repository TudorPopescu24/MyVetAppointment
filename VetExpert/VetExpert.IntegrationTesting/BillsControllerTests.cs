using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using VetExpert.API.Dto;
using VetExpert.Testing;
using Xunit;

namespace VetExpert.IntegrationTesting
{
    [Collection("Database tests")]
    public class BillControllerTests : BaseIntegrationTests, IDisposable
    {
        private const string ApiURL = "/api/Bills";

        public BillControllerTests()
        {
            CleanDatabases();
        }
        [Fact]
        public async void When_CreatedBill_Then_ShouldReturnBillInTheGetRequest()
        {

            CreateBillDto BillDto = CreateSUT();
            // Act
            var createBillResponse = await HttpClient.PostAsJsonAsync(ApiURL, BillDto);
            var getBillResult = await HttpClient.GetStringAsync(ApiURL);
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
            CleanDatabases();
        }
    }
}
