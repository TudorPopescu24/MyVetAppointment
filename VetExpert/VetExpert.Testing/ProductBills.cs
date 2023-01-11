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



            Assert.AreNotEqual(productBill.Id, Guid.Empty);
            Assert.AreEqual(productBill.BillId, Guid.Empty);
            Assert.AreEqual(productBill.DrugId, Guid.Empty);
            Assert.AreEqual(productBill.Quantity, 0);
            Assert.IsNotNull(productBill.Bill);
            Assert.IsNotNull(productBill.Drug);



        }
    }
}
