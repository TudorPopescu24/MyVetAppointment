namespace VetExpert.API.Dto
{
    public class CreatePromotionDto
    {
        public Guid ClinicId { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
