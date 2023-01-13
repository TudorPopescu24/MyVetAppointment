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
            admins.Count.Should().BeGreaterOrEqualTo(1);
            admins.Should().NotBeNull();
        }



        [Fact]
        public async void When_UpdateUser_Then_ShouldReturn_CorrectUser()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            var user_modified = CreateAdminDTO(DbSeeding.admins[0]);
            var json = JsonConvert.SerializeObject(user_modified, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
            );
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var updateUser = await http_client.PutAsync(ApiURL + "/" + DbSeeding.admins[0].Id, stringContent);
            var getUserResult = await http_client.GetStringAsync(ApiURL);
            var users = JsonConvert.DeserializeObject<List<CreateAdminDto>>(getUserResult);
            int counter = 0;
            foreach (var user in users)
            {
                if (user.UserName.Equals("TEST_ADMIN"))
                    counter++;
                if (user.UserName.Equals("name"))
                    counter = 100;
            }

            updateUser.StatusCode.Should().Be(HttpStatusCode.OK);

            counter.Should().Be(1);

        }


        public CreateAdminDto CreateAdminDTO(Admin d)
        {
            return new CreateAdminDto
            {
                UserName = "TEST_ADMIN"

            };

        }

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
