using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using VetExpert.API.Dto;
using VetExpert.Domain;
using VetExpert.Testing;
using Xunit;

namespace VetExpert.IntegrationTesting
{
    [Collection("Database tests")]
    public class AppointmentControllerTests : BaseIntegrationTests, IDisposable
    {
        private const string ApiURL = "/api/Appointments";

        [Fact]
        public async void When_CreatedAppointment_Then_ShouldReturnAppointmentInTheGetRequest()
        {
            //CleanDatabases();
            CreateAppointmentDto AppointmentDto = CreateSUT();
            // Act
            var createAppointmentResponse = await HttpClient.PostAsJsonAsync(ApiURL, AppointmentDto);
            var getAppointmentResult = await HttpClient.GetStringAsync(ApiURL);
            // Assert
            createAppointmentResponse.EnsureSuccessStatusCode();
            createAppointmentResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);



            var Appointments = JsonConvert.DeserializeObject<List<CreateAppointmentDto>>(getAppointmentResult);

            Appointments.Count.Should().Be(1);
            Appointments.Should().HaveCount(1);
            Appointments.Should().NotBeNull();

            Dispose();
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
            CleanDatabases();
        }
    }
}
