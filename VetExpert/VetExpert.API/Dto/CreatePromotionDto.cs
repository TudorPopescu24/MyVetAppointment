namespace VetExpert.API.Dto
{
    public class CreatePromotionDto
    {
        public Guid ClinicId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
