namespace VetExpert.Domain
{
    public class Clinic
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string? WebsiteUrl { get; set; }

        public string? Address { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

        public Clinic()
        {
            Id = new Guid();
        }
    }
}
