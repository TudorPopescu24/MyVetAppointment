namespace VetExpert.Domain
{
    public class Doctor
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public Guid ClinicId { get; set; } = Guid.Empty;

        public virtual Clinic Clinic { get; set; } = new Clinic();

        public virtual ICollection<DoctorSpecialization> DoctorSpecializations { get; set; } = new List<DoctorSpecialization>();
    }
}
