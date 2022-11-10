using Microsoft.EntityFrameworkCore;
using MyVetAppointment.API.Entities;

namespace MyVetAppointment.API.Data
{
    public class MainDbContext : DbContext
    {
        public DbSet<Clinic> Clinics { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

<<<<<<< HEAD
        public DbSet<Bill> Bills { get; set; }

        public DbSet<Appointment> Appointments { get; set; }


=======
        public DbSet<Specialization> Specializations { get; set; }
>>>>>>> c30d37b8e54853c214f4733a1627b12c46286694

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = MyVetAppointment.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
