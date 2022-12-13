using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;
using VetExpert.API.Dto;
using VetExpert.Testing;
using Xunit;

namespace VetExpert.IntegrationTesting
{
    public class SpecializationsControllerTests : BaseIntegrationTests, IDisposable
    {
        private const string ApiURL = "/api/specializations";

        [Fact]
        public async void When_CreatedSpecialization_Then_ShouldReturnSpecializationInTheGetRequest()
        {
            CleanDatabases();
            CreateSpecializationDto specializationDto = CreateSUT();
            // Act
            var createSpecializationResponse = await HttpClient.PostAsJsonAsync(ApiURL, specializationDto);
            var getSpecializationResult = await HttpClient.GetStringAsync(ApiURL);
            // Assert
            createSpecializationResponse.EnsureSuccessStatusCode();
            createSpecializationResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            //getDrugResult.EnsureSuccessStatusCode();

            //var drugs = await getDrugResult.Content
            //      .ReadFromJsonAsync<List<CreateDrugDto>>();
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
            CleanDatabases();
        }
    }
}
