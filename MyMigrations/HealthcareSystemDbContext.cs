using System;
using System.Collections.Generic;
using HealthcareApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApi.Api.Models;

public partial class HealthcareSystemDbContext : DbContext
{
    public HealthcareSystemDbContext()
    {
    }

    public HealthcareSystemDbContext(DbContextOptions<HealthcareSystemDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<DeskStaff> DeskStaffs { get; set; }

    public virtual DbSet<Diagnosis> Diagnoses { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Person> Persons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=HealthcareSystemDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCC248783DD3");

            entity.HasIndex(e => e.AppointmentDateTime, "IX_Appointments_Date");

            entity.HasIndex(e => e.DoctorId, "IX_Appointments_DoctorId");

            entity.HasIndex(e => e.PatientId, "IX_Appointments_PatientId");

            entity.Property(e => e.AppointmentDateTime).HasColumnType("datetime");
            entity.Property(e => e.Notes).HasColumnType("text");
            entity.Property(e => e.ReasonForVisit)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Doctor).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK_Appointments_Doctor");

            entity.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_Appointments_Patient");
        });

        modelBuilder.Entity<DeskStaff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DeskStaf__3214EC0728C78A38");

            entity.ToTable("DeskStaff");

            entity.HasIndex(e => e.PersonId, "UQ__DeskStaf__AA2FFBE4652A1E3D").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__DeskStaf__C9F28456312A4536").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Person).WithOne(p => p.DeskStaff)
                .HasForeignKey<DeskStaff>(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DeskStaff__Perso__29572725");
        });

        modelBuilder.Entity<Diagnosis>(entity =>
        {
            entity.HasKey(e => e.DiagnosisId).HasName("PK__Diagnose__0C54CC738B4E49BD");

            entity.HasIndex(e => e.DiagnosisDate, "IX_Diagnoses_Date");

            entity.HasIndex(e => e.DoctorId, "IX_Diagnoses_DoctorId");

            entity.HasIndex(e => e.PatientId, "IX_Diagnoses_PatientId");

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PrescribedTreatment).HasColumnType("text");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Diagnoses)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK_Diagnoses_Doctor");

            entity.HasOne(d => d.Patient).WithMany(p => p.Diagnoses)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_Diagnoses_Patient");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Doctors__3214EC07D5C4E53A");

            entity.HasIndex(e => e.Specialty, "IX_Doctors_Specialty");

            entity.HasIndex(e => e.PersonId, "UQ_Doctor_Person").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.LicenseNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Specialty)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Doctor)
                .HasForeignKey<Doctor>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Doctors_Person");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Patients__3214EC074E4227E9");

            entity.HasIndex(e => e.PersonId, "IX_Patients_PersonId");

            entity.HasIndex(e => e.PersonId, "UQ_Patient_Person").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Allergies).HasColumnType("text");
            entity.Property(e => e.BloodType)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.EmergencyContactName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmergencyContactPhone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.InsuranceNumber)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Patient)
                .HasForeignKey<Patient>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patients_Person");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A38015C1216");

            entity.HasIndex(e => e.AppointmentId, "IX_Payments_AppointmentId");

            entity.HasIndex(e => e.PaymentDate, "IX_Payments_PaymentDate");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Notes).HasColumnType("text");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Appointment).WithMany(p => p.Payments)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_Appointment");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Persons__3214EC07E48E01A2");

            entity.HasIndex(e => e.Surname, "IX_Persons_Surname");

            entity.HasIndex(e => e.PersonalNumber, "UQ__Persons__AC2CC42E28FCD4E9").IsUnique();

            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PersonalNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
