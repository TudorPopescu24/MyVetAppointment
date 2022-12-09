using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetExpert.Domain;

namespace VetExpert.Testing
{
    public class Doctors
    {


        public void DoctorValid()
        {
            Doctor doctor= new Doctor();



            Xunit.Assert.NotNull(doctor.Id);
 
        }


    }
}
