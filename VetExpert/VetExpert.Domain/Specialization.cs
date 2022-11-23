namespace VetExpert.Domain
{
    public class Specialization
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Doctor> Doctors { get; set; }
    }
}
