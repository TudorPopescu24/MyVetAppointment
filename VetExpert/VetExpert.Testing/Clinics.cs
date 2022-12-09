using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetExpert.Domain;

namespace VetExpert.Testing
{
    public class Clinics
    {

        public void ClinicValid()
        {
            Clinic clinic = new Clinic();



            Xunit.Assert.NotNull(clinic.Id);

        }

    }
}
