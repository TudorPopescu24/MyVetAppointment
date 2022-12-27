namespace VetExpert.Domain
{
    public class Specialization
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public virtual ICollection<DoctorSpecialization> DoctorSpecializations { get; set; } = new List<DoctorSpecialization>();

        public Specialization()
        {
            Id = Guid.NewGuid();
        }
    }
}
