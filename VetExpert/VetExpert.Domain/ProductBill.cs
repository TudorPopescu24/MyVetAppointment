using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetExpert.Domain
{
    public class ProductBill
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; }

        public Guid BillId { get; set; }

        public virtual Bill Bill { get; set; }

        public Guid DrugId { get; set; }

        public virtual Drug Drug { get; set; }
    }
}
