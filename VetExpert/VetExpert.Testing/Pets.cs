using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetExpert.Domain;

namespace VetExpert.Testing
{
    public class Pets
    {

        public void PetValid()
        {
            Pet pet = new Pet();



            Assert.NotEqual(pet.Id, Guid.Empty);
            Xunit.Assert.NotNull(pet.Name);
            Xunit.Assert.NotNull(pet.TypeOfPet);


        }


        public void PetVaccineInfo()
        {
            Pet pet = new Pet();

            pet.IsVaccinated = true;

            Assert.NotNull(pet.DateOfVaccine);


        }

    }
}
