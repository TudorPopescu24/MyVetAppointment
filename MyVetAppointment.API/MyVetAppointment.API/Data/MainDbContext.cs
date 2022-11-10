﻿using Microsoft.EntityFrameworkCore;
using MyVetAppointment.API.Entities;

namespace MyVetAppointment.API.Data
{
    public class MainDbContext : DbContext
    {
        public DbSet<Clinic> Clinics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = MyVetAppointment.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}