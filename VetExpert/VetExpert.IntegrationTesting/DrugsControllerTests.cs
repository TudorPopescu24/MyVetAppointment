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
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            CreateDrugDto drugDto = CreateSUT();

            var getDrugResult = await http_client.GetStringAsync(ApiURL);

            var drugs = JsonConvert.DeserializeObject<List<CreateDrugDto>>(getDrugResult);
           
            drugs.Count.Should().Be(1);
            drugs.Should().HaveCount(1);
            drugs.Should().NotBeNull();
            Dispose();
        }

        [Fact]
        public async void When_CreatingDrug_Then_ShouldReturnCreatedDrugInTheGetRequest()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            CreateDrugDto drugDto = CreateSUT();
            var postResult = await http_client.PostAsJsonAsync(ApiURL, drugDto);
            postResult.EnsureSuccessStatusCode();
            var getResult = await http_client.GetStringAsync(ApiURL);
            var drugs = JsonConvert.DeserializeObject<List<CreateDrugDto>>(getResult);

            //drugs.Should().Contain(drugDto);
            drugs.Should().HaveCount(2);
            drugs.Should().NotBeNull();
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
