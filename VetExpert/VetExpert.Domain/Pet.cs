namespace VetExpert.Domain
{
    public class Pet
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string TypeOfPet { get; set; }

        public int Age { get; set; }

        public int Weight { get; set; }

        public bool IsVaccinated { get; set; }

        public DateTime DateOfVaccine { get; set; }

        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public Pet()
        {
            Id = Guid.NewGuid();
        }
    }
}
