using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using VetExpert.API.Dto;
using VetExpert.Domain;
using VetExpert.Infrastructure;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace VetExpert.IntegrationTesting
{
    [Collection("Database tests")]
    public class UserControllerTests : IDisposable
    {

        private const string ApiURL = "/api/User";

        [Fact]
        public async void When_GetUser_Then_ShouldReturnUserInTheGetRequest()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            

            var getUserResult = await http_client.GetStringAsync(ApiURL);

            var users = JsonConvert.DeserializeObject<List<CreateUserDto>>(getUserResult);

            users.Count.Should().BeGreaterOrEqualTo(0);
            users.Should().HaveCount(3);
            users.Should().NotBeNull();
        }

        [Fact]
        public async void When_GetUserByAppId_Then_ShouldReturnUserWithCorrectId()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            

            var getUserResult = await http_client.GetStringAsync(ApiURL + "/applicationUser" + "/" + DbSeeding.users[0].ApplicationUserId);

            var getUser = JsonConvert.DeserializeObject<CreateUserDto>(getUserResult);

            getUser.Should().NotBeNull();
            //getUser.Id.Should().Be(userDto.Id);
        }

        [Fact]
        public async void When_DeleteUser_Then_ShouldReturn_CorrectNumber_Of_Users()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            var deleteUser=await http_client.DeleteAsync(ApiURL + "/" + DbSeeding.users[0].Id);
            deleteUser.StatusCode.Should().Be(HttpStatusCode.OK);
            var getUserResult = await http_client.GetStringAsync(ApiURL);
            var users = JsonConvert.DeserializeObject<List<CreateUserDto>>(getUserResult);
            users.Count.Should().Be(2);
        }

        [Fact]
        public async void When_UpdateUser_Then_ShouldReturn_CorrectUser()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            var user_modified = CreateUserDTO(DbSeeding.users[0]);
            var json = JsonConvert.SerializeObject(user_modified, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
            );
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var updateUser = await http_client.PutAsync(ApiURL + "/" + DbSeeding.users[0].Id,stringContent);
            var getUserResult = await http_client.GetStringAsync(ApiURL);
            var users = JsonConvert.DeserializeObject<List<CreateUserDto>>(getUserResult);
            int counter = 0;
            foreach (var user in users)
            {
                if (user.Name.Equals("USER_TEST"))
                    counter++;
                if (user.Name.Equals("name"))
                    counter = 100;
            }

            updateUser.StatusCode.Should().Be(HttpStatusCode.OK);

            counter.Should().Be(1);

        }

        private static CreateUserDto CreateUserDTO(User u)
        {
            var userDto = new CreateUserDto
            {
                Id = u.Id,
                Address =  u.Address,
                Name = "USER_TEST",
                ApplicationUserId =  u.ApplicationUserId,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber
            };
            return userDto;
        }


        [Fact]
        //public async void When_AddUser_Then_ShouldReturn_CorrectUsers()
        //{
        //    var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
        //    var userToAdd = CreateSUT();
        //    var json = JsonConvert.SerializeObject(userToAdd, Formatting.Indented,
        //        new JsonSerializerSettings()
        //        {
        //            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        //        }
        //    );
        //    var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
        //    var updateUser = await http_client.PostAsync(ApiURL, stringContent);
        //    var getUserResult = await http_client.GetStringAsync(ApiURL);
        //    var users = JsonConvert.DeserializeObject<List<CreateUserDto>>(getUserResult);


        //    updateUser.StatusCode.Should().Be(HttpStatusCode.OK);

            

        //}


        private static CreateUserDto CreateSUT()
        {

            var userDto = new CreateUserDto
            {
                

                Id= Guid.Parse("1A22AC17-F810-4DF3-B621-893DE38C2BD7"),
                Name = "user1",
                Address = "my address",
                Email = "myemail@email.com",
                PhoneNumber = "***",
                //ApplicationUserId = "3fa85f64 - 5717 - 4562"
            };
            return userDto;
        }

        public void Dispose()
        {
            //CleanDatabases();
        }
    }
}
