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
    public class Appointments
    {
        [TestMethod]
        public void AppointemtValid()
        {
            Appointment appointment = new Appointment();



            Assert.AreNotEqual(appointment.Id, Guid.Empty);
            Assert.IsNotNull(appointment.DateTime);
            Assert.AreNotEqual(appointment.PetId, Guid.Empty);
            Assert.IsNotNull(appointment.Pet);
            Assert.AreNotEqual(appointment.DoctorId, Guid.Empty);
            Assert.IsNotNull(appointment.Doctor);


        }
    }
}
