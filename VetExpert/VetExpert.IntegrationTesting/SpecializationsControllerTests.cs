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
    public class SpecializationsControllerTests :  IDisposable
    {
        private const string ApiURL = "/api/specializations";

        [Fact]
        public async void When_CreatedSpecialization_Then_ShouldReturnSpecializationInTheGetRequest()
        {
            //CleanDatabases();
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            
            var getSpecializationResult = await http_client.GetStringAsync(ApiURL);
            // Assert
            
            var specializations = JsonConvert.DeserializeObject<List<CreateSpecializationDto>>(getSpecializationResult);

            specializations.Count.Should().Be(1);
            specializations.Should().HaveCount(1);
            specializations.Should().NotBeNull();
            Dispose();
        }

        [Fact]
        public async void When_CreatingSpecialization_Then_ShouldReturnCreatedSpecializationInTheGetRequest()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            CreateSpecializationDto specializationDto = CreateSUT();
            var postResult = await http_client.PostAsJsonAsync(ApiURL, specializationDto);
            postResult.EnsureSuccessStatusCode();
            var getResult = await http_client.GetStringAsync(ApiURL);
            var specializations = JsonConvert.DeserializeObject<List<CreateSpecializationDto>>(getResult);

            //specializations.Should().Contain(specializationDto);
            specializations.Should().HaveCount(2);
            specializations.Should().NotBeNull();
        }

        [Fact]
        public async void When_UpdateUser_Then_ShouldReturn_CorrectUser()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            var user_modified = CreateSpecializationDTO(DbSeeding.specializations[0]);
            var json = JsonConvert.SerializeObject(user_modified, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
            );
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var updateUser = await http_client.PutAsync(ApiURL + "/" + DbSeeding.specializations[0].Id, stringContent);
            var getUserResult = await http_client.GetStringAsync(ApiURL);
            var users = JsonConvert.DeserializeObject<List<CreateSpecializationDto>>(getUserResult);
            int counter = 0;
            foreach (var user in users)
            {
                if (user.Name.Equals("TEST_SPEC"))
                    counter++;
                if (user.Name.Equals("name"))
                    counter = 100;
            }

            updateUser.StatusCode.Should().Be(HttpStatusCode.OK);

            counter.Should().Be(1);

        }


        [Fact]
        public async void When_CreatingAdmin_Then_ShouldReturnCreatedAdminInTheGetRequest()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            CreateSpecializationDto adminDto = CreateSUT();
            var postResult = await http_client.PostAsJsonAsync(ApiURL, adminDto);
            postResult.EnsureSuccessStatusCode();
            var getResult = await http_client.GetStringAsync(ApiURL);
            var admins = JsonConvert.DeserializeObject<List<CreateAdminDto>>(getResult);

            postResult.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        
        public CreateSpecializationDto CreateSpecializationDTO(Specialization d)
        {
            return new CreateSpecializationDto
            {
               Description = d.Description,
               Name = "TEST_SPEC"

            };

        }

        private static CreateSpecializationDto CreateSUT()
        {
            // Arrange
            return new CreateSpecializationDto
            {
                Name = "Neurologie",
                Description = "..."
            };
        }

        public void Dispose()
        {
           // CleanDatabases();
        }
    }
}
