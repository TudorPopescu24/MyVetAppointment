namespace VetExpert.Domain
{
    public class DrugStock
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; }

        public DateTime ExpirationDate { get; set; }

        public Guid DrugId { get; set; }

        public virtual Drug Drug { get; set; }
    }
}
