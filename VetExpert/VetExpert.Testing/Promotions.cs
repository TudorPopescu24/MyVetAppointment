using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetExpert.Domain;

namespace VetExpert.Testing
{
    public class Promotions
    {

        public void PromotionValid()
        {
            Promotion promotion = new Promotion();



            Xunit.Assert.NotNull(promotion.Id);

        }

    }
}
