using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;
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

        private static CreateAdminDto CreateSUT()
        {
            // Arrange
            return new CreateAdminDto
            {
                UserName="user-admin"

            };
        }

        public void Dispose()
        {
            //CleanDatabases();
        }
    }
}
