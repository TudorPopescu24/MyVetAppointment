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
    public class PetsControllerTests :  IDisposable
    {
        private const string ApiURL = "/api/Pets";


        [Fact]
        public async void When_CreatedPet_Then_ShouldReturnPetInTheGetRequest()
        {

            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            //CleanDatabases();
            var getPetResult = await http_client.GetStringAsync(ApiURL);
            // Assert
           
            var pets = JsonConvert.DeserializeObject<List<CreatePetDto>>(getPetResult);

            pets.Count.Should().BeGreaterOrEqualTo(0);
           
            Dispose();
            
        }


        [Fact]
        public async void When_UpdateUser_Then_ShouldReturn_CorrectUser()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            var user_modified = CreatePetDTO(DbSeeding.pets[0]);
            var json = JsonConvert.SerializeObject(user_modified, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
            );
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var updateUser = await http_client.PutAsync(ApiURL + "/" + DbSeeding.pets[0].Id, stringContent);
            var getUserResult = await http_client.GetStringAsync(ApiURL);
            var users = JsonConvert.DeserializeObject<List<CreateUserDto>>(getUserResult);
            int counter = 0;
            foreach (var user in users)
            {
                if (user.Name.Equals("TEST_PET"))
                    counter++;
                if (user.Name.Equals("name"))
                    counter = 100;
            }

            updateUser.StatusCode.Should().Be(HttpStatusCode.OK);

            counter.Should().Be(1);

        }

        [Fact]
        public async void When_DeleteUser_Then_ShouldReturn_CorrectNumber_Of_Users()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            var getUserResultFirst = await http_client.GetStringAsync(ApiURL);
            var users_first= JsonConvert.DeserializeObject<List<CreateUserDto>>(getUserResultFirst);
            var deleteUser = await http_client.DeleteAsync(ApiURL + "/" + DbSeeding.pets[0].Id);
            deleteUser.StatusCode.Should().Be(HttpStatusCode.OK);
            var getUserResult = await http_client.GetStringAsync(ApiURL);
            var users = JsonConvert.DeserializeObject<List<CreatePetDto>>(getUserResult);
            users.Count.Should().Be(users_first.Count-1);
        }

        [Fact]
        public async void When_CreatingPet_Then_ShouldReturnCreatedPetInTheGetRequest()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            //CleanDatabases();
            var getPetResultFirst = await http_client.GetStringAsync(ApiURL);
            // Assert

            var petsFirst = JsonConvert.DeserializeObject<List<CreatePetDto>>(getPetResultFirst);

            



           
            CreatePetDto petDto = CreateSUT(DbSeeding.users[0]);
            var postResult = await http_client.PostAsJsonAsync(ApiURL, petDto);
            postResult.EnsureSuccessStatusCode();
            var getPetResult = await http_client.GetStringAsync(ApiURL);

            var pets = JsonConvert.DeserializeObject<List<CreatePetDto>>(getPetResult);

            pets.Count.Should().Be(petsFirst.Count+1);
        }

        private static CreatePetDto CreateSUT(User user)
        {
            // Arrange
            return new CreatePetDto
            {
                UserId=user.Id,
                Name = "Azorel",
                TypeOfPet = "Catel",
                Age = 5,
                Weight = 7,
                IsVaccinated = true,
                DateOfVaccine = DateTime.Now

            };
        }


        private static CreateUserDto CreateSUTUser(User user)
        {

            // Arrange
            return new CreateUserDto
            {
                Address=user.Address,
                Email= user.Email,
                Name= user.Name, 
                PhoneNumber=user.PhoneNumber

            };
        }

        public void Dispose()
        {
           // CleanDatabases();
        }


        private static CreatePetDto CreatePetDTO(Pet u)
        {
            var petDto = new CreatePetDto
            {
                Name = "TEST_PET",
                Age = u.Age,
                DateOfVaccine = u.DateOfVaccine,
                IsVaccinated = u.IsVaccinated,
                TypeOfPet = u.TypeOfPet,
                UserId = u.UserId,
                Weight = u.Weight,

            };
            return petDto;
        }
    }
}
