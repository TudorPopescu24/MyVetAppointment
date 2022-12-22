using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;
using VetExpert.API.Dto;
using Xunit;

namespace VetExpert.IntegrationTesting
{
    [Collection("Database tests")]
    public class DoctorControllerTests :  IDisposable
    {
        private const string ApiURL = "/api/Doctors";

       
        [Fact]
        public async void When_CreatedDoctor_Then_ShouldReturnDoctorInTheGetRequest()
        {
            //CleanDatabases();
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
           
            var getDoctorResult = await http_client.GetStringAsync(ApiURL);
            // Assert

            var Doctors = JsonConvert.DeserializeObject<List<CreateDoctorDto>>(getDoctorResult);

            Doctors.Count.Should().Be(1);
            Doctors.Should().HaveCount(1);
            Doctors.Should().NotBeNull();

            Dispose();
        }

        private static CreateDoctorDto CreateSUT()
        {
            // Arrange
            return new CreateDoctorDto
            {
                ClinicId= Guid.NewGuid(),
                Email="!!",
                FirstName= "Test",
                LastName= "Test"


            };
           
        }

        public void Dispose()
        {
            //CleanDatabases();
        }
    }
}
