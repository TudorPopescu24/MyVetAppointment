using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;
using VetExpert.API.Dto;
using Xunit;

namespace VetExpert.IntegrationTesting
{
    [Collection("Database tests")]
    public class UserControllerTests :  IDisposable
    {
        
        private const string ApiURL = "/api/User";

        [Fact]
        public async void When_CreatedUser_Then_ShouldReturnUserInTheGetRequest()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            CreateUserDto userDto = CreateSUT();
            CreateUserDto userDto1 = CreateSUT();
  
            var getUserResult = await http_client.GetStringAsync(ApiURL);
            
            var users = JsonConvert.DeserializeObject<List<CreateUserDto>>(getUserResult);

            users.Count.Should().Be(2);
            users.Should().HaveCount(2);
            users.Should().NotBeNull();

        }

        private static CreateUserDto CreateSUT()
        {
            // Arrange
            return new CreateUserDto
            {
                Name = "user 1",
                Address="my address",
                Email= "myemail@email",
                PhoneNumber="***"
                
            };
        }

        public void Dispose()
        {
            //CleanDatabases();
        }
    }
}
