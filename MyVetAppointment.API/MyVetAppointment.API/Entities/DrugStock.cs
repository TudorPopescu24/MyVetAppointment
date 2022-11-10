namespace MyVetAppointment.API.Entities
{
    public class DrugStock
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public ICollection<Drug> Drugs { get; set; }
    }
}
