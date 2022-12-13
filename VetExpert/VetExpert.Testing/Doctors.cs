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
    public class Doctors
    {
[       [TestMethod]
        public void DoctorValid()
        {
            Doctor doctor= new Doctor();



            Assert.AreNotEqual(doctor.Id, Guid.Empty);
            Assert.AreNotEqual(doctor.ClinicId, Guid.Empty);
            Assert.NotNull(doctor.FirstName);
            Assert.NotNull(doctor.LastName);
            Assert.NotNull(doctor.Email);
            Assert.NotNull(doctor.Clinic);
            Assert.NotNull(doctor.DoctorSpecializations);




        }


    }
}
