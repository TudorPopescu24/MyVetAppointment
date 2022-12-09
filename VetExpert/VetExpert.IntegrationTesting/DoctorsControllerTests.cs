using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using VetExpert.API.Dto;
using VetExpert.Testing;
using Xunit;

namespace VetExpert.IntegrationTesting
{
    [Collection("Database tests")]
    public class DoctorControllerTests : BaseIntegrationTests, IDisposable
    {
        private const string ApiURL = "/api/Doctors";

        public DoctorControllerTests()
        {
            CleanDatabases();
        }
        [Fact]
        public async void When_CreatedDoctor_Then_ShouldReturnDoctorInTheGetRequest()
        {

            CreateDoctorDto DoctorDto = CreateSUT();
            // Act
            var createDoctorResponse = await HttpClient.PostAsJsonAsync(ApiURL, DoctorDto);
            var getDoctorResult = await HttpClient.GetStringAsync(ApiURL);
            // Assert
            createDoctorResponse.EnsureSuccessStatusCode();
            createDoctorResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);



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
            CleanDatabases();
        }
    }
}
