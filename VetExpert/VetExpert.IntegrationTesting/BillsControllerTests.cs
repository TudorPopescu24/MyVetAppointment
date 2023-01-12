using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using VetExpert.API.Dto;
using VetExpert.Domain;
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

            var getBillResult = await http_client.GetStringAsync(ApiURL);

            var bills = JsonConvert.DeserializeObject<List<CreateBillDto>>(getBillResult);

            bills.Count.Should().BeGreaterOrEqualTo(0);
           
        }
        [Fact]
        public async void When_DeleteUser_Then_ShouldReturn_CorrectNumber_Of_Users()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            var getUserResultFirst = await http_client.GetStringAsync(ApiURL);
            var usersFirst = JsonConvert.DeserializeObject<List<CreateUserDto>>(getUserResultFirst);
            
            var deleteUser = await http_client.DeleteAsync(ApiURL + "/" + DbSeeding.bills[0].Id);
            deleteUser.StatusCode.Should().Be(HttpStatusCode.OK);
            var getUserResult = await http_client.GetStringAsync(ApiURL);
            var users = JsonConvert.DeserializeObject<List<CreateUserDto>>(getUserResult);
            users.Count.Should().Be(usersFirst.Count-1);
        }

        [Fact]
        public async void When_AddBill_Then_ShouldReturn_CorrectNumber_Of_Bills()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            var getBillsResultFirst = await http_client.GetStringAsync(ApiURL);
            var billsFirst = JsonConvert.DeserializeObject<List<CreateUserDto>>(getBillsResultFirst);

            var billToAdd = CreateSUT();
            var json = JsonConvert.SerializeObject(billToAdd, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
            );
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var addBill = await http_client.PostAsync(ApiURL, stringContent);
            addBill.StatusCode.Should().Be(HttpStatusCode.Created);

        }

        private static CreateBillDto CreateSUT()
        {
            // Arrange
            return new CreateBillDto
            {
                

                Id = Guid.NewGuid(),
                Currency = "lei",
                DateTime = DateTime.Today,
                Value=100,
                UserId = DbSeeding.users[0].Id,
                ClinicId = DbSeeding.clinics[0].Id

            };
        }

        public void Dispose()
        {
            //CleanDatabases();
        }
    }
}
