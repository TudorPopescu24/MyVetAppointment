using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;
using VetExpert.API.Dto;
using Xunit;

namespace VetExpert.IntegrationTesting
{
    [Collection("Database tests")]
    public class AppointmentControllerTests :  IDisposable
    {
        private const string ApiURLToPost = "api/Appointments/1";
        private const string ApiURLToGet = "/api/Appointments";
        [Fact]
        public async void When_CreatedAppointment_Then_ShouldReturnAppointmentInTheGetRequest()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            
            var getAppointmentResult = await http_client.GetStringAsync(ApiURLToGet);
            // Assert
           
            var Appointments = JsonConvert.DeserializeObject<List<CreateAppointmentDto>>(getAppointmentResult);

            Appointments.Count.Should().Be(1);
            Appointments.Should().HaveCount(1);
            Appointments.Should().NotBeNull();

            
        }

        private static CreateAppointmentDto CreateSUT()
        {
            // Arrange
            return new CreateAppointmentDto
            {
              DoctorId=Guid.NewGuid(),
              PetId=Guid.NewGuid()

            };
        }

        public void Dispose()
        {
            //CleanDatabases();
        }
    }
}
