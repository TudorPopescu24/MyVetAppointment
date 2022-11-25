namespace VetExpert.Domain
{
    public class Bill
    {
        public Guid Id { get; set; }

        public int Value { get; set; }

        public string Currency { get; set; }

        public virtual ICollection<Drug> Drugs { get; set; } = new List<Drug>();

        public DateTime DateTime { get; set; }
        
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public Guid ClinicId { get; set; }

        public virtual Clinic Clinic { get; set; }
    }
}