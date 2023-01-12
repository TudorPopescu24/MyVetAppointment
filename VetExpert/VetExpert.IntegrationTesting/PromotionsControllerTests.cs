using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;
using VetExpert.API.Dto;
using Xunit;

namespace VetExpert.IntegrationTesting
{
    [Collection("Database tests")]
    public class PromotionsControllerTests :  IDisposable
    {
        private const string ApiURL = "/api/Promotions";


        [Fact]
        public async void When_CreatedPromotion_Then_ShouldReturnPromotionInTheGetRequest()
        {
            //CleanDatabases();
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            
            var getPromotionResult = await http_client.GetStringAsync(ApiURL);
            // Assert
            
            var promotions = JsonConvert.DeserializeObject<List<CreatePromotionDto>>(getPromotionResult);

            promotions.Count.Should().Be(1);
            promotions.Should().HaveCount(1);
            promotions.Should().NotBeNull();
            Dispose();
        }

        [Fact]
        public async void When_CreatingPromotion_Then_ShouldReturnCreatedPromotionInTheGetRequest()
        {
            var http_client = new CustomWebApplicationFactory<Program>().CreateClient();
            CreatePromotionDto promotionDto = CreateSUT();
            var postResult = await http_client.PostAsJsonAsync(ApiURL, promotionDto);
            postResult.EnsureSuccessStatusCode();
            var getResult = await http_client.GetStringAsync(ApiURL);
            var promotions = JsonConvert.DeserializeObject<List<CreatePromotionDto>>(getResult);

            //promotions.Should().Contain(promotionDto);
            promotions.Should().HaveCount(2);
            promotions.Should().NotBeNull();
        }

        private static CreatePromotionDto CreateSUT()
        {
            // Arrange
            return new CreatePromotionDto
            {
                ClinicId= Guid.NewGuid(),
                Name = "Test-Promotion",
                Description = "Test-Description"

            };
        }

        public void Dispose()
        {
            //CleanDatabases();
        }
    }
}
