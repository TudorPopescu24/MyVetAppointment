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
    public class Clinics
    {
        [TestMethod]

        public void ClinicValid()
        {
            Clinic clinic = new Clinic();



            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual(clinic.Id, Guid.Empty);
            Xunit.Assert.NotNull(clinic.Name);
            Xunit.Assert.NotNull(clinic.Email);


        }

    }
}
