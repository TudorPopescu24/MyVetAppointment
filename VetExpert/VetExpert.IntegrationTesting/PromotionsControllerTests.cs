using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;
using VetExpert.API.Dto;
using VetExpert.Testing;
using Xunit;

namespace VetExpert.IntegrationTesting
{
    [Collection("Database tests")]
    public class PromotionsControllerTests : BaseIntegrationTests, IDisposable
    {
        private const string ApiURL = "/api/promotions";


        [Fact]
        public async void When_CreatedPromotion_Then_ShouldReturnPromotionInTheGetRequest()
        {
            CleanDatabases();
            CreatePromotionDto promotionDto = CreateSUT();
            // Act
            var createPromotionResponse = await HttpClient.PostAsJsonAsync(ApiURL, promotionDto);
            var getPromotionResult = await HttpClient.GetStringAsync(ApiURL);
            // Assert
            createPromotionResponse.EnsureSuccessStatusCode();
            createPromotionResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);



            var promotions = JsonConvert.DeserializeObject<List<CreatePromotionDto>>(getPromotionResult);

            promotions.Count.Should().Be(1);
            promotions.Should().HaveCount(1);
            promotions.Should().NotBeNull();
            Dispose();
        }
        private static CreatePromotionDto CreateSUT()
        {
            // Arrange
            return new CreatePromotionDto
            {
                Name = "Test-Promotion",
                Description = "Test-Description"

            };
        }

        public void Dispose()
        {
            CleanDatabases();
        }
    }
}
