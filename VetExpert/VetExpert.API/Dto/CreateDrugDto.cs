namespace VetExpert.API.Dto
{
    public class CreateDrugDto
    {
        public string Name { get; set; } = string.Empty;

        public string Manufacturer { get; set; } = string.Empty;

        public int Weight { get; set; } = 0;

        public string Prospect { get; set; } = string.Empty;

        public int Price { get; set; } = 0;
    }
}
