namespace Medicines.Data
{
    using Medicines.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection.Metadata.Ecma335;

    public class MedicinesContext : DbContext
    {
        public MedicinesContext()
        {
        }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<PatientMedicine> PatientsMedicines { get; set; }
        public MedicinesContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PatientMedicine>()
                  .HasKey(p => new { p.PatientId, p.MedicineId });
        }
    }
}
