namespace VetExpert.Domain
{
    public class Drug
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Manufacturer { get; set; } = string.Empty;

        public int Weight { get; set; } = 0;

        public string Prospect { get; set; } = string.Empty;

        public int Price { get; set; } = 0;

        public Guid ClinicId { get; set; } = Guid.Empty;
		public virtual Clinic Clinic { get; set; } = new Clinic();

		public virtual ICollection<DrugStock> DrugStocks { get; set; } = new List<DrugStock>();

        public Drug()
        {
            Id = Guid.NewGuid();
        }
    }
}
