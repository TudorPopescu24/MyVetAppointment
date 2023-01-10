using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetExpert.Domain;

namespace VetExpert.Testing
{

    [TestClass]
    public class DrugStocks
    {

        [TestMethod]

        public void DrugStockTest()
        {
            DrugStock drugStock= new DrugStock();

            Assert.AreNotEqual(drugStock.Id, Guid.Empty);
            Assert.AreEqual(drugStock.DrugId, Guid.Empty);
            Assert.IsNotNull(drugStock.Drug);
            Assert.IsNotNull(drugStock.ExpirationDate);
            Assert.AreEqual(drugStock.Quantity, 0);


        }
    }
}
