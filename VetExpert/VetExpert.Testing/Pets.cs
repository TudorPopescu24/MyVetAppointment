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



            Xunit.Assert.NotNull(pet.Id);

        }

    }
}
