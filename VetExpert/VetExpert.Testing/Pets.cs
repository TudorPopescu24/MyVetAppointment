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



            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual(pet.Id, Guid.Empty);
            Xunit.Assert.NotNull(pet.Name);
            Xunit.Assert.NotNull(pet.TypeOfPet);


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
