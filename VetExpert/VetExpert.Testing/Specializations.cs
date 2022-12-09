using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetExpert.Domain;

namespace VetExpert.Testing
{
    [TestClass]
    public class Specializations
    {
        [TestMethod]
        public void PromotionValid()
        {
            Specialization specialization = new Specialization();



            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual(specialization.Id, Guid.Empty);

        }
    }
}
