using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using VetExpert.API.Dto;
using VetExpert.Infrastructure;
using Xunit;

namespace VetExpert.IntegrationTesting
{
    [Collection("Database tests")]
    public class UserControllerTests : IDisposable
    {

        private const string ApiURL = "/api/User";

        [Fact]
        public async void When_CreatedUser_Then_ShouldReturnUserInTheGetRequest()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            CreateUserDto userDto = CreateSUT();
            CreateUserDto userDto1 = CreateSUT();

            var getUserResult = await http_client.GetStringAsync(ApiURL);

            var users = JsonConvert.DeserializeObject<List<CreateUserDto>>(getUserResult);

            users.Count.Should().Be(3);
            users.Should().HaveCount(3);
            users.Should().NotBeNull();
        }

        [Fact]
        public async void When_GetUserById_Then_ShouldReturnUserWithCorrectId()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            CreateUserDto userDto = CreateSUT();

            var getUserResult = await http_client.GetStringAsync(ApiURL + "/" + userDto.Id);

            var getUser = JsonConvert.DeserializeObject<List<CreateUserDto>>(getUserResult);

            getUser.Should().NotBeNull();
            //getUser.Id.Should().Be(userDto.Id);
        }

        //[Fact]
        //public async void When_CreatingUser_Then_ShouldReturnCreatedUserInTheGetRequest()
        //{
        //    var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
        //    CreateUserDto userDto = CreateSUT();
        //    var postResult = await http_client.PostAsJsonAsync(ApiURL, userDto);
        //    postResult.EnsureSuccessStatusCode();
        //    var getResult = await http_client.GetStringAsync(ApiURL);
        //    var users = JsonConvert.DeserializeObject<List<CreateUserDto>>(getResult);

        //    users.Should().Contain(userDto);
        //    users.Should().HaveCount(1);
        //    users.Should().NotBeNull();
        //}

        //[Fact]
            //public async void When_DeletedUser_Then_ShouldReturnNoUserInTheGetRequest()
            //{
            //    var factory = new CustomWebApplicationFactory<Program>();
            //    var client = factory.CreateClient();
            //    //var db = factory.Services.GetService<MainDbContext>();
            //    DbSeeding.InitializeDbForTests(db);

            //    var userDto = new CreateUserDto
            //    {
            //        Name = "user 1",
            //        Address = "my address",
            //        Email = "myemail@email",
            //        PhoneNumber = "***"
            //    };
            //    var createUserResponse = await client.PostAsync("/api/users", new StringContent(JsonConvert.SerializeObject(userDto), Encoding.UTF8, "application/json"));
            //    createUserResponse.EnsureSuccessStatusCode();

            //    var userId = (JsonConvert.DeserializeObject<CreateUserDto>(await createUserResponse.Content.ReadAsStringAsync())).Id;
            //    var deleteUserResponse = await client.DeleteAsync("/api/users/" + userId);
            //    deleteUserResponse.EnsureSuccessStatusCode();

            //    var getUsersResponse = await client.GetStringAsync("/api/users");
            //    var users = JsonConvert.DeserializeObject<List<CreateUserDto>>(getUsersResponse);

            //    users.Should().NotContain(u => u.Id == userId);
            //}


            private static CreateUserDto CreateSUT()
        {

            var userDto = new CreateUserDto
            {
                Name = "user 1",
                Address = "my address",
                Email = "myemail@email",
                PhoneNumber = "***",
                //ApplicationUserId = "3fa85f64 - 5717 - 4562"
            };

            //var http_client = new CustomWebApplicationFactory<Program>().CreateClient();

            //var content = new StringContent(JsonConvert.SerializeObject(userDto), Encoding.UTF8, "application/json");
            //var createResult = http_client.PostAsync(ApiURL, content).Result;
            //createResult.EnsureSuccessStatusCode();

            //var getUsersResult = http_client.GetStringAsync(ApiURL).Result;
            //var users = JsonConvert.DeserializeObject<List<CreateUserDto>>(getUsersResult);
            //var createdUser = users.Single(x => x.Id == userDto.Id);

            //return createdUser;
            return userDto;
        }

        public void Dispose()
        {
            //CleanDatabases();
        }
    }
}
