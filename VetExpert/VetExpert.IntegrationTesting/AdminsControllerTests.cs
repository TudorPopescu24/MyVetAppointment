using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using VetExpert.API.Dto;
using Xunit;

namespace VetExpert.IntegrationTesting
{
    [Collection("Database tests")]
    public class AdminControllerTests : IDisposable
    {
        private const string ApiURL = "/api/admin";


        [Fact]
        public async void When_CreatedAdmin_Then_ShouldReturnadminInTheGetRequest()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();

            CreateAdminDto adminDto = CreateSUT();
            var getAdminResult = await http_client.GetStringAsync(ApiURL);

            var admins = JsonConvert.DeserializeObject<List<CreateAdminDto>>(getAdminResult);

            admins.Count.Should().Be(1);
            admins.Should().HaveCount(1);
            admins.Should().NotBeNull();
        }

        [Fact]
        public async void When_CreatingAdmin_Then_ShouldReturnCreatedAdminInTheGetRequest()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            CreateAdminDto adminDto = CreateSUT();
            var postResult = await http_client.PostAsJsonAsync(ApiURL, adminDto);
            postResult.EnsureSuccessStatusCode();
            var getResult = await http_client.GetStringAsync(ApiURL);
            var admins = JsonConvert.DeserializeObject<List<CreateAdminDto>>(getResult);

            //admins.Should().Contain(adminDto);
            admins.Should().HaveCount(2);
            admins.Should().NotBeNull();
        }

        //[Fact]
        //public async void When_Admin_Then_ShouldReturnUpdatedAdminInTheGetRequest()
        //{
        //    var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
        //    CreateAdminDto adminDto = CreateSUT();
        //    var updateDto = new CreateAdminDto { UserName = "updated username" };

        //    var updateResult = await http_client.PutAsync(ApiURL + "/" + adminDto.UserName, new StringContent(JsonConvert.SerializeObject(updateDto), Encoding.UTF8, "application/json"));
        //    updateResult.EnsureSuccessStatusCode();

        //    var getAdminResult = await http_client.GetStringAsync(ApiURL + "/" + adminDto.UserName);

        //    var updatedAdmin = JsonConvert.DeserializeObject<CreateAdminDto>(getAdminResult);

        //    updatedAdmin.Should().NotBeNull();
        //    updatedAdmin.UserName.Should().Be(updateDto.UserName);
        //}


        private static CreateAdminDto CreateSUT()
        {
            // Arrange
            return new CreateAdminDto
            {
                UserName = "user-admin"

            };
        }

        public void Dispose()
        {
            //CleanDatabases();
        }
    }
}
