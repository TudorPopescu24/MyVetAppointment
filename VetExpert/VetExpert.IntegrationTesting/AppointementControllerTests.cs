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


        [Fact]
        public async void When_DeleteUser_Then_ShouldReturn_CorrectNumber_Of_Users()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            var getUserResultFirst = await http_client.GetStringAsync(ApiURLToGet);
            var users_first = JsonConvert.DeserializeObject<List<CreateUserDto>>(getUserResultFirst);
            var deleteUser = await http_client.DeleteAsync(ApiURLToGet + "/" + DbSeeding.appointments[0].Id);
            deleteUser.StatusCode.Should().Be(HttpStatusCode.OK);
            var getUserResult = await http_client.GetStringAsync(ApiURLToGet);
            var users = JsonConvert.DeserializeObject<List<CreateClinicDto>>(getUserResult);
            users.Count.Should().Be(users_first.Count - 1);
        }


        [Fact]
        public async void When_UpdateUser_Then_ShouldReturn_CorrectUser()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            var user_modified = CreateAppointmentDTO(DbSeeding.appointments[0]);
            var json = JsonConvert.SerializeObject(user_modified, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
            );
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var updateUser = await http_client.PutAsync(ApiURLToGet + "/" + DbSeeding.appointments[0].Id, stringContent);
            var getUserResult = await http_client.GetStringAsync(ApiURLToGet);
            var users = JsonConvert.DeserializeObject<List<CreateAppointmentDto>>(getUserResult);
            int counter = 0;
            foreach (var user in users)
            {
                if (user.Details.Equals("TEST_DETAILS"))
                    counter++;
                if (user.Details.Equals("name"))
                    counter = 100;
            }

            updateUser.StatusCode.Should().Be(HttpStatusCode.OK);

            counter.Should().Be(1);

        }


        [Fact]
        public async void When_CreatingAppointment_Then_ShouldReturnCreatedAppointmentInTheGetRequest()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            CreateAppointmentDto appointmentDto = CreateSUT();
            var postResult = await http_client.PostAsJsonAsync(ApiURLToGet, appointmentDto);
           postResult.StatusCode.Should().Be(HttpStatusCode.Created);

           
        }

        private static CreateAppointmentDto CreateSUT()
        {
            // Arrange
            return new CreateAppointmentDto
            {
              ClinicId= DbSeeding.clinics[0].Id,
                PetId = DbSeeding.pets[0].Id,
                Details = "no",
                IsConfirmed = true,
                DateTime = DateTime.Now,
                UserId = DbSeeding.users[0].Id,

            };
        }

        public CreateAppointmentDto CreateAppointmentDTO(Appointment d)
        {
            return new CreateAppointmentDto
            {
                ClinicId = d.ClinicId,
                UserId = d.UserId,
                DateTime = d.DateTime,
                Details = "TEST_DETAILS",
                IsConfirmed = d.IsConfirmed,
                PetId = d.PetId,

            };

        }

        public void Dispose()
        {
            //CleanDatabases();
        }
    }
}
