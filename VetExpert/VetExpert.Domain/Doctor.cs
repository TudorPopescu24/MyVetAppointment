namespace VetExpert.Domain
{
    public class Doctor
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public Guid ClinicId { get; set; }

        public virtual Clinic Clinic { get; set; }

        public virtual ICollection<DoctorSpecialization> DoctorSpecializations { get; set; } = new List<DoctorSpecialization>();
    }
}
