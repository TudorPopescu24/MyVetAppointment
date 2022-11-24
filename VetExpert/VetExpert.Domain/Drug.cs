namespace VetExpert.Domain
{
    public class Drug
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Manufacturer { get; set; }

        public int Weight { get; set; }

        public string Prospect { get; set; }

        public int Price { get; set; }

        public virtual ICollection<DrugStock> DrugStocks { get; set; } = new List<DrugStock>();

    }
}
