using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using VetExpert.Domain;

namespace VetExpert.Infrastructure
{
    public class MainDbContext : DbContext
    {
        //Configure dbcontext appsettings.json
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }
        public DbSet<Clinic> Clinics { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Bill> Bills { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Specialization> Specializations { get; set; }

        public DbSet<DoctorSpecialization> DoctorSpecializations { get; set; }

        public DbSet<Drug> Drugs { get; set; }

        public DbSet<DrugStock> DrugStocks { get; set; }

        public DbSet<Pet> Pets { get; set; }

        public DbSet<Promotion> Promotions { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DoctorSpecialization>()
                .HasKey(x => new { x.DoctorId, x.SpecializationId });
            modelBuilder.Entity<DoctorSpecialization>()
                .HasOne(x => x.Doctor)
                .WithMany(x => x.DoctorSpecializations)
                .HasForeignKey(x => x.DoctorId);
            modelBuilder.Entity<DoctorSpecialization>()
                .HasOne(x => x.Specialization)
                .WithMany(x => x.DoctorSpecializations)
                .HasForeignKey(x => x.SpecializationId);
        }
    }
}
