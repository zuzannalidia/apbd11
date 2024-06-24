using apbd10.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd10.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PrescriptionMedicament>()
                .HasKey(pm => new { pm.IdPrescription, pm.IdMedicament });

            modelBuilder.Entity<PrescriptionMedicament>()
                .HasOne(pm => pm.Prescription)
                .WithMany(p => p.PrescriptionMedicaments)
                .HasForeignKey(pm => pm.IdPrescription);

            modelBuilder.Entity<PrescriptionMedicament>()
                .HasOne(pm => pm.Medicament)
                .WithMany(m => m.PrescriptionMedicaments)
                .HasForeignKey(pm => pm.IdMedicament);

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Doctor)
                .WithMany(d => d.Prescriptions)
                .HasForeignKey(p => p.IdDoctor);

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Patient)
                .WithMany(pa => pa.Prescriptions)
                .HasForeignKey(p => p.IdPatient);

            // Seed data
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { IdDoctor = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
                new Doctor { IdDoctor = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com" }
            );

            modelBuilder.Entity<Medicament>().HasData(
                new Medicament { IdMedicament = 1, Name = "MedicamentA", Description = "DescriptionA", Type = "TypeA" },
                new Medicament { IdMedicament = 2, Name = "MedicamentB", Description = "DescriptionB", Type = "TypeB" }
            );

            modelBuilder.Entity<Patient>().HasData(
                new Patient { IdPatient = 1, FirstName = "Alice", LastName = "Johnson", Birthdate = new DateTime(1990, 1, 1) },
                new Patient { IdPatient = 2, FirstName = "Bob", LastName = "Brown", Birthdate = new DateTime(1985, 5, 23) }
            );

            modelBuilder.Entity<Prescription>().HasData(
                new Prescription { IdPrescription = 1, Date = new DateTime(2024, 1, 1), DueDate = new DateTime(2024, 1, 15), IdDoctor = 1, IdPatient = 1 },
                new Prescription { IdPrescription = 2, Date = new DateTime(2024, 2, 1), DueDate = new DateTime(2024, 2, 15), IdDoctor = 2, IdPatient = 2 }
            );

            modelBuilder.Entity<PrescriptionMedicament>().HasData(
                new PrescriptionMedicament { IdPrescription = 1, IdMedicament = 1, Dose = 10, Details = "Take once a day" },
                new PrescriptionMedicament { IdPrescription = 1, IdMedicament = 2, Dose = 5, Details = "Take twice a day" },
                new PrescriptionMedicament { IdPrescription = 2, IdMedicament = 1, Dose = 20, Details = "Take once a day" }
            );
        }
    }
}
