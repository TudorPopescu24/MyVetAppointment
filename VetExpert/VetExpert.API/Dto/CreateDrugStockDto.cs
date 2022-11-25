namespace VetExpert.API.Dto
{
    public class CreateDrugStockDto
    {
        public int Quantity { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
