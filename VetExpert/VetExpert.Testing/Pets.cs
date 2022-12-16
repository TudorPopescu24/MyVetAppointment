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
    public class Pets
    {
        [TestMethod]
        public void PetValid()
        {
            Pet pet = new Pet();



            Assert.AreNotEqual(pet.Id, Guid.Empty);
            Assert.AreNotEqual(pet.UserId, Guid.Empty);
            Assert.IsNotNull(pet.Name);
            Assert.IsNotNull(pet.IsVaccinated);
            Assert.IsNotNull(pet.Age);
            Assert.IsNotNull(pet.DateOfVaccine);
            Assert.IsNotNull(pet.TypeOfPet);
            Assert.IsNotNull(pet.User);



        }

        [TestMethod]

        public void PetVaccineInfo()
        {
            Pet pet = new Pet();

            pet.IsVaccinated = true;

            Assert.IsNotNull(pet.DateOfVaccine);


        }

    }
}
