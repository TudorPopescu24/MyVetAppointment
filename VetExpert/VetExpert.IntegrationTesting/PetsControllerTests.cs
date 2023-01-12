using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;
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

            pets.Count.Should().Be(1);
            pets.Should().HaveCount(1);
            pets.Should().NotBeNull();
            Dispose();
            
        }

        //[Fact]
        //public async void When_CreatingPet_Then_ShouldReturnCreatedPetInTheGetRequest()
        //{
        //    var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
        //    CreatePetDto petDto = CreateSUT();
        //    var postResult = await http_client.PostAsJsonAsync(ApiURL, petDto);
        //    postResult.EnsureSuccessStatusCode();
        //    var getResult = await http_client.GetStringAsync(ApiURL);
        //    var pets = JsonConvert.DeserializeObject<List<CreatePetDto>>(getResult);

        //    //pets.Should().Contain(petDto);
        //    pets.Should().HaveCount(1);
        //    pets.Should().NotBeNull();
        //}

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
    }
}
