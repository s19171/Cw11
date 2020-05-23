using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw11.Models
{
    public class CodeFirstContext : DbContext
    {
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<PrescriptionMedicament> PrescriptionMedicament { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }

        public CodeFirstContext()
        {

        }
        public CodeFirstContext(DbContextOptions<CodeFirstContext> opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.IdPatient).HasName("PATIENT_PK");
                //entity.Property(e => e.idPatient).ValueGeneratedNever();
                entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.BirthDate).IsRequired();
            });
            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.HasKey(e => e.IdPrescription).HasName("PRESCRIPTION_PK");
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.DueDate).IsRequired();
                entity.HasOne(d => d.Patient).WithMany(p => p.Prescriptions).HasForeignKey(d => d.IdPatient)
                .OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("Prescription_Patient");
            });
            modelBuilder.Entity<PrescriptionMedicament>(entity =>
            {
                entity.ToTable("Prescription_Medicament");
                entity.HasKey(e => e.IdPrescription).HasName("PRESCRIPTION_MEDICAMENT_PK1");
                entity.HasKey(e => e.IdMedicament).HasName("PRESCRIPTION_MEDICAMENT_PK2");
                entity.Property(e => e.IdPrescription).ValueGeneratedNever();
                entity.Property(e => e.IdMedicament).ValueGeneratedNever();
                entity.HasOne(d => d.Prescription).WithMany(p => p.Medicaments).HasForeignKey(d => d.IdPrescription);
                entity.HasOne(d => d.Medicament).WithMany(p => p.Prescriptions).HasForeignKey(d => d.IdMedicament);
                entity.Property(e => e.Details).IsRequired();
            });
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.IdDoctor).HasName("DOCTOR_PK");
                entity.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
            });
            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.IdMedicament).HasName("DOCTOR_PK");
                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Type).HasMaxLength(100).IsRequired();
            });


        }
    }
}
