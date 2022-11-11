namespace MyVetAppointment.API.Entities
{
    public class DrugStock
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public DateTime ExpirationDate { get; set; }

        public Drug Drug { get; set; }
    }
}
