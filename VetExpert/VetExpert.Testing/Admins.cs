﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetExpert.Domain;

namespace VetExpert.Testing
{
    [TestClass]
    public class Admins
    {
        [TestMethod]
        public void AdminValid()
        {
            Admin admin = new Admin();



			Assert.AreNotEqual(admin.Id, Guid.Empty);
            Assert.IsNotNull(admin.UserName);

        }


    }
}
