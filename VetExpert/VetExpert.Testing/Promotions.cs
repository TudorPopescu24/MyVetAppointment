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
    public class Promotions
    {
        [TestMethod]
        public void PromotionValid()
        {
            Promotion promotion = new Promotion();



            Assert.AreNotEqual(promotion.Id, Guid.Empty);
            Assert.AreNotEqual(promotion.ClinicId, Guid.Empty);
            Assert.IsNotNull(promotion.Name);
            Assert.IsNotNull(promotion.Description);
            Assert.IsNotNull(promotion.Clinic);

        }

    }
}
