namespace VetExpert.Domain
{
    public class Admin
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string UserName { get; set; } = string.Empty;
    }
}
