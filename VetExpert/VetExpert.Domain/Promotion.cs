namespace VetExpert.Domain
{
    public class Promotion
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Guid ClinicId { get; set; } = Guid.Empty;

        public virtual Clinic Clinic { get; set; } = new Clinic();
    }    
}
