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



            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual(promotion.Id, Guid.Empty);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual(promotion.ClinicId, Guid.Empty);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(promotion.Name);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(promotion.Description);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(promotion.Clinic);

        }

    }
}
