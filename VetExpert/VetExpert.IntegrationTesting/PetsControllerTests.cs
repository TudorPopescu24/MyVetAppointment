using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;
using VetExpert.API.Dto;
using VetExpert.Testing;
using Xunit;

namespace VetExpert.IntegrationTesting
{
    [Collection("Database tests")]
    public class PetsControllerTests : BaseIntegrationTests, IDisposable
    {
        private const string ApiURL = "/api/pets";


        [Fact]
        public async void When_CreatedPet_Then_ShouldReturnPetInTheGetRequest()
        {

            //CleanDatabases();
            CreatePetDto petDto = CreateSUT();
            // Act
            var createPetResponse = await HttpClient.PostAsJsonAsync(ApiURL, petDto);
            var getPetResult = await HttpClient.GetStringAsync(ApiURL);
            // Assert
            createPetResponse.EnsureSuccessStatusCode();
            createPetResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);



            var pets = JsonConvert.DeserializeObject<List<CreatePetDto>>(getPetResult);

            pets.Should().NotBeNull();

            if (pets != null)
            {
                pets.Count.Should().Be(1);
                pets.Should().HaveCount(1);
            }

            Dispose();
        }

        private static CreatePetDto CreateSUT()
        {
            // Arrange
            return new CreatePetDto
            {
                Name = "Azorel",
                TypeOfPet = "Catel",
                Age = 5,
                Weight = 7,
                IsVaccinated = true,
                DateOfVaccine = DateTime.Now

            };
        }

        public void Dispose()
        {
            CleanDatabases();
        }
    }
}
