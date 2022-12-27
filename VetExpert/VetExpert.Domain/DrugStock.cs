namespace VetExpert.Domain
{
    public class DrugStock
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; } = 0;

        public DateTime ExpirationDate { get; set; }

        public Guid DrugId { get; set; } = Guid.Empty;

        public virtual Drug Drug { get; set; } = new Drug();

        public DrugStock()
        {
	        Id = Guid.NewGuid();
        }
    }
}
