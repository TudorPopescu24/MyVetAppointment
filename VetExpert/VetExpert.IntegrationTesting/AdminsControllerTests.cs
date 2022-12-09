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
    public class AdminControllerTests : BaseIntegrationTests, IDisposable
    {
        private const string ApiURL = "/api/admin";

      
        [Fact]
        public async void When_CreatedAdmin_Then_ShouldReturnadminInTheGetRequest()
        {

            CleanDatabases();
            CreateAdminDto adminDto = CreateSUT();
            // Act
            var createAdminResponse = await HttpClient.PostAsJsonAsync(ApiURL, adminDto);
            var getAdminResult = await HttpClient.GetStringAsync(ApiURL);
            // Assert
            createAdminResponse.EnsureSuccessStatusCode();
            createAdminResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);



            var admins = JsonConvert.DeserializeObject<List<CreateAdminDto>>(getAdminResult);

            admins.Count.Should().Be(1);
            admins.Should().HaveCount(1);
            admins.Should().NotBeNull();
            Dispose();
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
            CleanDatabases();
        }
    }
}
