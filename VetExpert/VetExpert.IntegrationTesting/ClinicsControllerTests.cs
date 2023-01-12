using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using VetExpert.API.Dto;
using VetExpert.Domain;
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

        [Fact]
        public async void When_DeleteUser_Then_ShouldReturn_CorrectNumber_Of_Users()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            var getUserResultFirst = await http_client.GetStringAsync(ApiURL);
            var users_first = JsonConvert.DeserializeObject<List<CreateUserDto>>(getUserResultFirst);
            var deleteUser = await http_client.DeleteAsync(ApiURL + "/" + DbSeeding.clinics[0].Id);
            deleteUser.StatusCode.Should().Be(HttpStatusCode.OK);
            var getUserResult = await http_client.GetStringAsync(ApiURL);
            var users = JsonConvert.DeserializeObject<List<CreateClinicDto>>(getUserResult);
            users.Count.Should().Be(users_first.Count - 1);
        }

        [Fact]
        public async void When_UpdateUser_Then_ShouldReturn_CorrectUser()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            var user_modified = CreateClinicDTO(DbSeeding.clinics[0]);
            var json = JsonConvert.SerializeObject(user_modified, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
            );
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var updateUser = await http_client.PutAsync(ApiURL + "/" + DbSeeding.clinics[0].Id, stringContent);
            var getUserResult = await http_client.GetStringAsync(ApiURL);
            var users = JsonConvert.DeserializeObject<List<CreateClinicDto>>(getUserResult);
            int counter = 0;
            foreach (var user in users)
            {
                if (user.Name.Equals("TEST_CLINIC"))
                    counter++;
                if (user.Name.Equals("name"))
                    counter = 100;
            }

            updateUser.StatusCode.Should().Be(HttpStatusCode.OK);

            counter.Should().Be(1);

        }

        public CreateClinicDto CreateClinicDTO(Clinic d)
        {
            return new CreateClinicDto
            {
                Email = d.Email,
                Address = d.Address,
                ApplicationUserId = d.ApplicationUserId,
                Name = "TEST_CLINIC",
                WebsiteUrl = "http.site"

            };

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
