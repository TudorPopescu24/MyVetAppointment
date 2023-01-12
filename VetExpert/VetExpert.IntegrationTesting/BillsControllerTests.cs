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
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            CreateBillDto billDto = CreateSUT();

            var getBillResult = await http_client.GetStringAsync(ApiURL);

            var bills = JsonConvert.DeserializeObject<List<CreateBillDto>>(getBillResult);

            bills.Count.Should().Be(1);
            bills.Should().HaveCount(1);
            bills.Should().NotBeNull();
        }

        //[Fact]
        //public async void When_CreatingBill_Then_ShouldReturnCreatedBillInTheGetRequest()
        //{
        //    var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
        //    CreateBillDto billDto = CreateSUT();
        //    var postResult = await http_client.PostAsJsonAsync(ApiURL, billDto);
        //    postResult.EnsureSuccessStatusCode();
        //    var getResult = await http_client.GetStringAsync(ApiURL);
        //    var bills = JsonConvert.DeserializeObject<List<CreateBillDto>>(getResult);

        //    //bills.Should().Contain(billDto);
        //    bills.Should().HaveCount(1);
        //    bills.Should().NotBeNull();
        //}

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
