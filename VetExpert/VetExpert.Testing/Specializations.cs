using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetExpert.Domain;

namespace VetExpert.Testing
{
    public class Specializations
    {
        public void PromotionValid()
        {
            Specialization specialization = new Specialization();



            Xunit.Assert.NotNull(specialization.Id);

        }
    }
}
