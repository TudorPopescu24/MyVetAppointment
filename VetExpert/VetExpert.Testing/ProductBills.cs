using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetExpert.Domain;

namespace VetExpert.Testing
{
    [TestClass]
    public class ProductBills
    {
        [TestMethod]
        public void ProductBillValid()
        {
            ProductBill productBill = new ProductBill();



            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual(productBill.Id, Guid.Empty);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual(productBill.BillId, Guid.Empty);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual(productBill.DrugId, Guid.Empty);
            Xunit.Assert.NotNull(productBill.Quantity);
            Xunit.Assert.NotNull(productBill.Bill);
            Xunit.Assert.NotNull(productBill.Drug);
           ;



        }
    }
}
