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

            Clinics.Count.Should().Be(2);
            Clinics.Should().HaveCount(2);
            Clinics.Should().NotBeNull();

            
        }

        //[Fact]
        //public async void When_CreatingClinic_Then_ShouldReturnCreatedClinicInTheGetRequest()
        //{
        //    var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
        //    CreateClinicDto clinicDto = CreateSUT();
        //    var postResult = await http_client.PostAsJsonAsync(ApiURL, clinicDto);
        //    postResult.EnsureSuccessStatusCode();
        //    var getResult = await http_client.GetStringAsync(ApiURL);
        //    var clinics = JsonConvert.DeserializeObject<List<CreateClinicDto>>(getResult);

        //    //clinics.Should().Contain(clinicDto);
        //    clinics.Should().HaveCount(1);
        //    clinics.Should().NotBeNull();
        //}

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
