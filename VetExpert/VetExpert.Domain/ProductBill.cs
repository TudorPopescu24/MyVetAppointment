using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetExpert.Domain
{
    public class ProductBill
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public int Quantity { get; set; } = 0;

        public Guid BillId { get; set; } = Guid.Empty;

        public virtual Bill Bill { get; set; } = new Bill();

        public Guid DrugId { get; set; } = Guid.Empty;

        public virtual Drug Drug { get; set; } = new Drug();
    }
}
