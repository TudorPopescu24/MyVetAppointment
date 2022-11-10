namespace MyVetAppointment.API.Entities
{
    public class Doctor
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int ClinicId { get; set; }

        public Clinic Clinic { get; set; }

        public ICollection<Specialization> Specializations { get; set; }
    }
}
