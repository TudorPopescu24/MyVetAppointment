using System;

namespace VetExpert.Domain
{
    public class Bill
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public int Value { get; set; } = 0;

        public string Currency { get; set; } = string.Empty;

        public virtual ICollection<Drug> Drugs { get; set; } = new List<Drug>();

        public DateTime DateTime { get; set; } = DateTime.Now;

        public Guid UserId { get; set; } = Guid.Empty;

        public virtual User User { get; set; } = new User();

        public Guid ClinicId { get; set; } = Guid.Empty;

        public virtual Clinic Clinic { get; set; } = new Clinic();

    }
}
