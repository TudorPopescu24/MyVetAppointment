using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetExpert.Domain;

namespace VetExpert.Testing
{
    [TestClass]
    public class DoctorSpecializations
    {
        [TestMethod]

        public void DoctorSpecializationValid()
        {
            DoctorSpecialization doctspec = new DoctorSpecialization();

            Assert.AreEqual(doctspec.DoctorId, Guid.Empty);
            Assert.AreEqual(doctspec.SpecializationId, Guid.Empty);
            Assert.IsNotNull(doctspec.SpecializationId);
            Assert.IsNotNull(doctspec.Specialization);
            Assert.IsNotNull(doctspec.Doctor);



        }
    }
}
