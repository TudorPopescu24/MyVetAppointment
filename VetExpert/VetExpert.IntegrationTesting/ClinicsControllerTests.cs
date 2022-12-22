using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;
using VetExpert.API.Dto;
using Xunit;

namespace VetExpert.IntegrationTesting
{
    [Collection("Database tests")]
    public class ClinicControllerTests :  IDisposable
    {
        private const string ApiURL = "/api/Clinics";

        
        [Fact]
        public async void When_CreatedClinic_Then_ShouldReturnClinicInTheGetRequest()
        {
            //CleanDatabases();
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            
            // Act
           
            var getClinicResult = await http_client.GetStringAsync(ApiURL);
            // Assert
           

            var Clinics = JsonConvert.DeserializeObject<List<CreateClinicDto>>(getClinicResult);

            Clinics.Count.Should().Be(1);
            Clinics.Should().HaveCount(1);
            Clinics.Should().NotBeNull();

            
        }

        private static CreateClinicDto CreateSUT()
        {
            // Arrange
            return new CreateClinicDto
            {
                Name = "Clinic 1",
                Address = "my address",
                Email = "myemail@email",
               WebsiteUrl="@@@"
               

            };
        }

        public void Dispose()
        {
            //CleanDatabases();
        }
    }
}
