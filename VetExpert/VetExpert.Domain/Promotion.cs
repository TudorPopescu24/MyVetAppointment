namespace VetExpert.Domain
{
    public class Promotion
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid ClinicId { get; set; }

        public virtual Clinic Clinic { get; set; }

        public Promotion()
        {
            Id = Guid.NewGuid();
        }
    }    
}
