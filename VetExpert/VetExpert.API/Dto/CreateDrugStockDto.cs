﻿namespace VetExpert.API.Dto
{
    public class CreateDrugStockDto
    {
        public Guid DrugId { get; set; }

        public int Quantity { get; set; } = 0;

        public DateTime ExpirationDate { get; set; }
    }
}
