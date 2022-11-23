namespace VetExpert.Domain
{
    public class Promotion
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Clinic Clinic { get; set; }
    }
}
