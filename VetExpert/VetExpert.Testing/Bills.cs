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
            Assert.IsNotNull(bill.Currency);
            Assert.IsNotNull(bill.Drugs);
            Assert.IsNotNull(bill.User);
            Assert.AreNotEqual(bill.UserId, Guid.Empty);
            Assert.IsNotNull(bill.Clinic);
            Assert.AreNotEqual(bill.ClinicId, Guid.Empty);




        }
    }
}
