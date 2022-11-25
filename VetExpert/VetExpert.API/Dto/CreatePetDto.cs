namespace VetExpert.API.Dto
{
    public class CreatePetDto
    {
        public string Name { get; set; }

        public string TypeOfPet { get; set; }

        public int Age { get; set; }

        public int Weight { get; set; }

        public bool IsVaccinated { get; set; }
    }
}
