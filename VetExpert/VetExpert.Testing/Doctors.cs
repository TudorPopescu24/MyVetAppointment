using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetExpert.Domain;
using Assert = NUnit.Framework.Assert;

namespace VetExpert.Testing
{
    public class Doctors
    {


        public void DoctorValid()
        {
            Doctor doctor= new Doctor();



            Assert.AreNotEqual(doctor.Id, Guid.Empty);
            Assert.AreNotEqual(doctor.ClinicId, Guid.Empty);
            Xunit.Assert.NotNull(doctor.LastName);
            Xunit.Assert.NotNull(doctor.Clinic);



        }


    }
}
