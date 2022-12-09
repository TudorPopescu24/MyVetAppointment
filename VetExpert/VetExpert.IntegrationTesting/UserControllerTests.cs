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
    public class UserControllerTests : BaseIntegrationTests, IDisposable
    {
        private const string ApiURL = "/api/User";

        [Fact]
        public async void When_CreatedUser_Then_ShouldReturnUserInTheGetRequest()
        {
            Dispose();
            CreateUserDto userDto = CreateSUT();
            // Act
            var createUserResponse = await HttpClient.PostAsJsonAsync(ApiURL, userDto);
            var getUserResult = await HttpClient.GetStringAsync(ApiURL);
            // Assert
            createUserResponse.EnsureSuccessStatusCode();
            createUserResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);


          
            var users = JsonConvert.DeserializeObject<List<CreateUserDto>>(getUserResult);

            users.Count.Should().Be(1);
            users.Should().HaveCount(1);
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
            CleanDatabases();
        }
    }
}
