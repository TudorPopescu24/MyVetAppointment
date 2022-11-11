using Microsoft.EntityFrameworkCore;
using MyVetAppointment.API.Entities;
using System.Collections.Generic;

namespace MyVetAppointment.API.Data
{
    public class MainDbContext : DbContext
    {
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Admin> Admins { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Bill> Bills { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Specialization> Specializations { get; set; }

        public DbSet<Drug> Drugs { get; set; }

        public DbSet<DrugStock> DrugStocks { get; set; }

        public DbSet<Pet> Pets { get; set; }

        public DbSet<Promotion> Promotions { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = MyVetAppointment.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
