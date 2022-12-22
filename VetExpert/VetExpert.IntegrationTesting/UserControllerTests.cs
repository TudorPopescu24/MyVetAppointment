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
            //Dispose();
            CreateUserDto userDto = CreateSUT();
            CreateUserDto userDto1 = CreateSUT();
            // Act
            var createUserResponse = await http_client.PostAsJsonAsync(ApiURL, userDto);
            var createUserResponse1 = await http_client.PostAsJsonAsync(ApiURL, userDto1);
            // Assert
            createUserResponse.EnsureSuccessStatusCode();
            createUserResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

           createUserResponse1.EnsureSuccessStatusCode();
           createUserResponse1.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
            var getUserResult = await http_client.GetStringAsync(ApiURL);
            // Assert
           

            var users = JsonConvert.DeserializeObject<List<CreateUserDto>>(getUserResult);

            users.Count.Should().Be(3);
            users.Should().HaveCount(3);
            users.Should().NotBeNull();

            Dispose();
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
