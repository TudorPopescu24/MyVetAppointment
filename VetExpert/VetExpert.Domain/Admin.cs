namespace VetExpert.Domain
{
    public class Admin
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public Admin()
        {
            Id= Guid.NewGuid();
        }
    }



}
