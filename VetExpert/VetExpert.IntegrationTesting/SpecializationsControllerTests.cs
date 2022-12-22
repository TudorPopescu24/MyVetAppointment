using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;
using VetExpert.API.Dto;
using Xunit;

namespace VetExpert.IntegrationTesting
{
    [Collection("Database tests")]
    public class SpecializationsControllerTests :  IDisposable
    {
        private const string ApiURL = "/api/specializations";

        [Fact]
        public async void When_CreatedSpecialization_Then_ShouldReturnSpecializationInTheGetRequest()
        {
            //CleanDatabases();
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            
            var getSpecializationResult = await http_client.GetStringAsync(ApiURL);
            // Assert
            
            var specializations = JsonConvert.DeserializeObject<List<CreateSpecializationDto>>(getSpecializationResult);

            specializations.Count.Should().Be(1);
            specializations.Should().HaveCount(1);
            specializations.Should().NotBeNull();
            Dispose();
        }

        private static CreateSpecializationDto CreateSUT()
        {
            // Arrange
            return new CreateSpecializationDto
            {
                Name = "Neurologie",
                Description = "..."
            };
        }

        public void Dispose()
        {
           // CleanDatabases();
        }
    }
}
