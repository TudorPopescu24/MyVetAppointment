using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetExpert.Domain;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace VetExpert.Testing
{
    [TestClass]
    public class Bills
    {
        [TestMethod]
        public void BillValid()
        {
            Bill bill = new Bill();



            Assert.AreNotEqual(bill.Id, Guid.Empty);
            Assert.IsNotNull(bill.Value);
            Assert.IsNotNull(bill.DateTime);


        }
    }
}
