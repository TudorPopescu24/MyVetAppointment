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

        [Fact]
        public async void When_CreatingSpecialization_Then_ShouldReturnCreatedSpecializationInTheGetRequest()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            CreateSpecializationDto specializationDto = CreateSUT();
            var postResult = await http_client.PostAsJsonAsync(ApiURL, specializationDto);
            postResult.EnsureSuccessStatusCode();
            var getResult = await http_client.GetStringAsync(ApiURL);
            var specializations = JsonConvert.DeserializeObject<List<CreateSpecializationDto>>(getResult);

            //specializations.Should().Contain(specializationDto);
            specializations.Should().HaveCount(2);
            specializations.Should().NotBeNull();
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
