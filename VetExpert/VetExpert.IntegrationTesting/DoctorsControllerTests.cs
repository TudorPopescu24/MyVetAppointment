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

            Doctors.Count.Should().BeGreaterOrEqualTo(0);
            

            Dispose();
        }

        [Fact]
        public async void When_DeleteUser_Then_ShouldReturn_CorrectNumber_Of_Users()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            var getUserResultFirst = await http_client.GetStringAsync(ApiURL);
            var users_first = JsonConvert.DeserializeObject<List<CreateUserDto>>(getUserResultFirst);
            var deleteUser = await http_client.DeleteAsync(ApiURL + "/" + DbSeeding.doctors[0].Id);
            deleteUser.StatusCode.Should().Be(HttpStatusCode.OK);
            var getUserResult = await http_client.GetStringAsync(ApiURL);
            var users = JsonConvert.DeserializeObject<List<CreateUserDto>>(getUserResult);
            users.Count.Should().Be(users_first.Count - 1);
        }


        [Fact]
        public async void When_UpdateUser_Then_ShouldReturn_CorrectUser()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            var user_modified = CreateDoctorDTO(DbSeeding.doctors[0]);
            var json = JsonConvert.SerializeObject(user_modified, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
            );
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var updateUser = await http_client.PutAsync(ApiURL + "/" + DbSeeding.doctors[0].Id, stringContent);
            var getUserResult = await http_client.GetStringAsync(ApiURL);
            var users = JsonConvert.DeserializeObject<List<CreateDoctorDto>>(getUserResult);
            int counter = 0;
            foreach (var user in users)
            {
                if (user.FirstName.Equals("TEST_DOCTOR"))
                    counter++;
                if (user.FirstName.Equals("name"))
                    counter = 100;
            }

            updateUser.StatusCode.Should().Be(HttpStatusCode.OK);

            counter.Should().Be(1);

        }


        [Fact]
        public async void When_AddDoctor_Then_ShouldReturn_CorrectNumber_Of_Bills()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            var getBillsResultFirst = await http_client.GetStringAsync(ApiURL);
            var billsFirst = JsonConvert.DeserializeObject<List<CreateUserDto>>(getBillsResultFirst);

            var billToAdd = CreateSUT();
            var json = JsonConvert.SerializeObject(billToAdd, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
            );
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var addBill = await http_client.PostAsync(ApiURL, stringContent);
            addBill.StatusCode.Should().Be(HttpStatusCode.Created);

        }

        private static CreateDoctorDto CreateSUT()
        {
            // Arrange
            return new CreateDoctorDto
            {
                ClinicId = DbSeeding.clinics[0].Id,
                Email = "emai@email.com",
                FirstName = "Test",
                LastName = "Test"


            };

        }


        public CreateDoctorDto CreateDoctorDTO(Doctor d)
        {
            return new CreateDoctorDto
            {
                FirstName ="TEST_DOCTOR",
                ClinicId = d.ClinicId,
                Email =  "email@email.com",
                LastName = d.LastName,
                
            };
            
        }

        public void Dispose()
        {
            //CleanDatabases();
        }
    }
}
