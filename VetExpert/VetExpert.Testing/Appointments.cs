using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetExpert.Domain;

namespace VetExpert.Testing
{
    public class Appointments
    {
        public void AppointemtValid()
        {
            Appointment appointment = new Appointment();



            Assert.NotEqual(appointment.Id, Guid.Empty);

        }
    }
}
