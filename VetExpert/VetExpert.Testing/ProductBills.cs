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
            Assert.IsNotNull(productBill.Quantity);
            Assert.IsNotNull(productBill.Bill);
            Assert.IsNotNull(productBill.Drug);



        }
    }
}
