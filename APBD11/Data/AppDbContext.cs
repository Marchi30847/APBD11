using APBD11.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD11.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Patient> Patients { get; set; } = null!;
    public DbSet<Doctor> Doctors { get; set; } = null!;
    public DbSet<Medicament> Medicaments { get; set; } = null!;
    public DbSet<Prescription> Prescriptions { get; set; } = null!;
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = null!;
    public virtual DbSet<User> Users { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.ToTable("Patient");
            entity.HasKey(p => p.IdPatient);

            entity.Property(p => p.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(p => p.LastName)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(p => p.BirthDate)
                .HasColumnType("date")
                .IsRequired();

            entity.HasMany(p => p.Prescriptions)
                .WithOne(r => r.Patient)
                .HasForeignKey(r => r.IdPatient);
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.ToTable("Doctor");
            entity.HasKey(d => d.IdDoctor);

            entity.Property(d => d.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(d => d.LastName)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(d => d.Email)
                .HasMaxLength(100)
                .IsRequired();

            entity.HasMany(d => d.Prescriptions)
                .WithOne(r => r.Doctor)
                .HasForeignKey(r => r.IdDoctor);
        });
        
        modelBuilder.Entity<Medicament>(entity =>
        {
            entity.ToTable("Medicament");
            entity.HasKey(m => m.IdMedicament);

            entity.Property(m => m.Name)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(m => m.Description)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(m => m.Type)
                .HasMaxLength(100)
                .IsRequired();

            entity.HasMany(m => m.PrescriptionMedicaments)
                .WithOne(pm => pm.Medicament)
                .HasForeignKey(pm => pm.IdMedicament);
        });
        
        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.ToTable("Prescription");
            entity.HasKey(r => r.IdPrescription);

            entity.Property(r => r.Date)
                .HasColumnType("date")
                .IsRequired();

            entity.Property(r => r.DueDate)
                .HasColumnType("date")
                .IsRequired();
            
            entity.HasMany(r => r.PrescriptionMedicaments)
                .WithOne(pm => pm.Prescription)
                .HasForeignKey(pm => pm.IdPrescription);
        });
        
        modelBuilder.Entity<PrescriptionMedicament>(entity =>
        {
            entity.ToTable("Prescription_Medicament");

            entity.HasKey(pm => new { pm.IdPrescription, pm.IdMedicament });

            entity.Property(pm => pm.Dose)
                .IsRequired(false);

            entity.Property(pm => pm.Details)
                .HasMaxLength(100)
                .IsRequired();
        });
        
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasKey(u => u.Id);

            entity.Property(u => u.Username)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(u => u.PasswordHash)
                .IsRequired();

            entity.Property(u => u.PasswordSalt)
                .IsRequired();

            entity.Property(u => u.RefreshToken)
                .HasMaxLength(500);

            entity.Property(u => u.RefreshTokenExpiryTime)
                .IsRequired();
        });
    }
}