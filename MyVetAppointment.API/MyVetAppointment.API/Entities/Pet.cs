namespace MyVetAppointment.API.Entities
{
    public class Pet
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string TypeOfPet { get; set; }

        public int Age { get; set; }
        
        public int Weight { get; set; }

        public bool IsVaccinated { get; set; }

        public DateTime DateOfVaccine { get; set; }

        public User User { get; set; }
    }
}
