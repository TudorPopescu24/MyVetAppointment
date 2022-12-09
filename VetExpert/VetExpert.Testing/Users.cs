using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetExpert.Domain;

namespace VetExpert.Testing
{
    public class Users
    {
        public void UserValid()
        {
            User user = new User();



            Xunit.Assert.NotNull(user.Id);

        }
    }
}
