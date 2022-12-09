using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;
using VetExpert.API.Dto;
using VetExpert.Testing;
using Xunit;

namespace VetExpert.IntegrationTesting
{
    public class DrugsControllerTests : BaseIntegrationTests, IDisposable
    {
        private const string ApiURL = "/api/drugs";

        [Fact]
        public async void When_CreatedDrug_Then_ShouldReturnDrugInTheGetRequest()
        {
            CreateDrugDto drugDto = CreateSUT();
            // Act
            var createDrugResponse = await HttpClient.PostAsJsonAsync(ApiURL, drugDto);
            var getDrugResult = await HttpClient.GetStringAsync(ApiURL);
            // Assert
            createDrugResponse.EnsureSuccessStatusCode();
            createDrugResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);


            /*      var drugs = await getDrugResult.Content
                      .ReadFromJsonAsync<List<CreateDrugDto>>();*/
            var drugs = JsonConvert.DeserializeObject<List<CreateDrugDto>>(getDrugResult);
           
            drugs.Count.Should().Be(1);
            drugs.Should().HaveCount(1);
            drugs.Should().NotBeNull();
            CleanDatabases();
        }

        private static CreateDrugDto CreateSUT()
        {
            // Arrange
            return new CreateDrugDto
            {
                Name = "Vetomune Twist Off",
                Manufacturer = "VetExpert",
                Weight = 120,
                Prospect = "VetoMune 120mg este un produs care sprijina imbunatatirea imunitatii non-specifice la caini si pisici",
                Price = 130
            };
        }

        public void Dispose()
        {
            CleanDatabases();
        }
    }
}
