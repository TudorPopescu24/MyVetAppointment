using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;
using VetExpert.API.Dto;
using Xunit;
namespace VetExpert.IntegrationTesting
{
    [Collection("Database tests")]
    public class DrugsControllerTests : IDisposable
    {
        private const string ApiURL = "/api/Drugs";

        [Fact]
        public async void When_CreatedDrug_Then_ShouldReturnDrugInTheGetRequest()
        {
            //CleanDatabases();
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            CreateDrugDto drugDto = CreateSUT();
            // Act
            var createDrugResponse = await http_client.PostAsJsonAsync(ApiURL, drugDto);
            var getDrugResult = await http_client.GetStringAsync(ApiURL);
            // Assert
            createDrugResponse.EnsureSuccessStatusCode();
            createDrugResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);


                 
            var drugs = JsonConvert.DeserializeObject<List<CreateDrugDto>>(getDrugResult);
           
            drugs.Count.Should().Be(1);
            drugs.Should().HaveCount(1);
            drugs.Should().NotBeNull();
            Dispose();
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
           // CleanDatabases();
        }
    }
}
