using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetExpert.Domain;

namespace VetExpert.Testing
{
    public class Admins
    {

        public void AdminValid()
        {
            Admin admin = new Admin();



            Xunit.Assert.NotNull(admin.Id);

        }


    }
}
