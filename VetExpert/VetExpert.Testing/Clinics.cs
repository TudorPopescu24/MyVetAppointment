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



            Assert.AreNotEqual(clinic.Id, Guid.Empty);
            Assert.IsNotNull(clinic.Name);
            Assert.IsNotNull(clinic.Email);
            Assert.IsNotNull(clinic.Doctors);


        }

    }
}
