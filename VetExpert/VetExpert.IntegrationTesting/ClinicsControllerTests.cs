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
    public class ClinicControllerTests : BaseIntegrationTests, IDisposable
    {
        private const string ApiURL = "/api/Clinics";


        [Fact]
        public async void When_CreatedClinic_Then_ShouldReturnClinicInTheGetRequest()
        {
            //CleanDatabases();
            CreateClinicDto ClinicDto = CreateSUT();
            // Act
            var createClinicResponse = await HttpClient.PostAsJsonAsync(ApiURL, ClinicDto);
            var getClinicResult = await HttpClient.GetStringAsync(ApiURL);
            // Assert
            createClinicResponse.EnsureSuccessStatusCode();
            createClinicResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);



            var Clinics = JsonConvert.DeserializeObject<List<CreateClinicDto>>(getClinicResult);

            Clinics.Should().NotBeNull();

            if (Clinics != null)
            {
                Clinics.Count.Should().Be(1);
                Clinics.Should().HaveCount(1);
            }

            Dispose();
        }

        private static CreateClinicDto CreateSUT()
        {
            // Arrange
            return new CreateClinicDto
            {
                Name = "Clinic 1",
                Address = "my address",
                Email = "myemail@email",
                WebsiteUrl = "@@@"


            };
        }

        public void Dispose()
        {
            CleanDatabases();
        }
    }
}
