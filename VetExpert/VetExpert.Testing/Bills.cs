using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetExpert.Domain;

namespace VetExpert.Testing
{
    public class Bills
    {
        public void BillValid()
        {
            Bill bill = new Bill();



            Xunit.Assert.NotNull(bill.Id);

        }
    }
}
