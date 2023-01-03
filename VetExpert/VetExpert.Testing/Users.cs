using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetExpert.Domain;

namespace VetExpert.Testing
{
    [TestClass]
    public class Users
    {
        [TestMethod]
        public void UserValid()
        {
            User user = new User();


            Assert.AreNotEqual(user.Id, Guid.Empty);
          //  Assert.AreNotEqual(user.ApplicationUserId, Guid.Empty);
            Assert.IsNotNull(user.ApplicationUser);
            Assert.IsNotNull(user.Name);
            Assert.IsNotNull(user.Email);
            Assert.IsNotNull(user.Pets);


        }
    }
}
