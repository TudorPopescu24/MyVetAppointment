namespace VetExpert.API.Dto
{
    public class CreatePetDto
    {
        public Guid UserId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string TypeOfPet { get; set; } = string.Empty;

        public int Age { get; set; } = 0;

        public int Weight { get; set; } = 0;

        public bool IsVaccinated { get; set; } = false;

        public DateTime? DateOfVaccine { get; set; }
    }
}
