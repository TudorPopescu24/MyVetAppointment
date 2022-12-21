namespace VetExpert.Domain
{
    public class Drug
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public string Manufacturer { get; set; } = string.Empty;

        public int Weight { get; set; } = 0;

        public string Prospect { get; set; } = string.Empty;

        public int Price { get; set; } = 0;

        public virtual ICollection<DrugStock> DrugStocks { get; set; } = new List<DrugStock>();

    }
}
