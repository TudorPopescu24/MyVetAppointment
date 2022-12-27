namespace VetExpert.Domain
{
    public class Promotion
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Value { get; set; } = 0;

        public Guid ClinicId { get; set; } = Guid.Empty;

        public virtual Clinic Clinic { get; set; } = new Clinic();

        public Promotion()
        {
            Id = Guid.NewGuid();
        }
    }    
}
