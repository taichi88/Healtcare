using System;
using System.Collections.Generic;
using HealthcareApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApi.Infrastructure;

public partial class HealthcareApiContext : DbContext
{
    public HealthcareApiContext()
    {
    }

    public HealthcareApiContext(DbContextOptions<HealthcareApiContext> options)
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

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCC2EDD9B25C");

            entity.ToTable("Appointments", "Clinical");

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
                .HasPrincipalKey(p => p.PersonId)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appointments_Doctor");

            entity.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .HasPrincipalKey(p => p.PersonId)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appointments_Patient");
        });

        modelBuilder.Entity<DeskStaff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DeskStaf__3214EC07F495B35A");

            entity.ToTable("DeskStaff", "Core");

            entity.HasIndex(e => e.PersonId, "UQ__DeskStaf__AA2FFBE4A6271690").IsUnique();

            entity.HasOne(d => d.Person).WithOne(p => p.DeskStaff)
                .HasForeignKey<DeskStaff>(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DeskStaff__Perso__286302EC");
        });

        modelBuilder.Entity<Diagnosis>(entity =>
        {
            entity.HasKey(e => e.DiagnosisId).HasName("PK__Diagnose__0C54CC7336BBA273");

            entity.ToTable("Diagnoses", "Clinical");

            entity.HasIndex(e => e.DiagnosisDate, "IX_Diagnoses_Date");

            entity.HasIndex(e => e.DoctorId, "IX_Diagnoses_DoctorId");

            entity.HasIndex(e => e.PatientId, "IX_Diagnoses_PatientId");

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PrescribedTreatment).HasColumnType("text");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Diagnoses)
                .HasPrincipalKey(p => p.PersonId)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Diagnoses_Doctor");

            entity.HasOne(d => d.Patient).WithMany(p => p.Diagnoses)
                .HasPrincipalKey(p => p.PersonId)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Diagnoses_Patient");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Doctors__3214EC071979FC24");

            entity.ToTable("Doctors", "Core");

            entity.HasIndex(e => e.Specialty, "IX_Doctors_Specialty");

            entity.HasIndex(e => e.PersonId, "UQ__Doctors__AA2FFBE4F1DE2CAC").IsUnique();

            entity.Property(e => e.LicenseNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Specialty)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Person).WithOne(p => p.Doctor)
                .HasForeignKey<Doctor>(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Doctors_Person");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Patients__3214EC077A0D5241");

            entity.ToTable("Patients", "Core");

            entity.HasIndex(e => e.PersonId, "IX_Patients_PersonId");

            entity.HasIndex(e => e.PersonId, "UQ__Patients__AA2FFBE4BCE6301B").IsUnique();

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

            entity.HasOne(d => d.Person).WithOne(p => p.Patient)
                .HasForeignKey<Patient>(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patients_Person");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A3877BF4C30");

            entity.ToTable("Payments", "Billing");

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
            entity.HasKey(e => e.Id).HasName("PK__Persons__3214EC07EA1A0F95");

            entity.ToTable("Persons", "Core");

            entity.HasIndex(e => e.Surname, "IX_Persons_Surname");

            entity.HasIndex(e => e.PersonalNumber, "UQ__Persons__AC2CC42ED6656B1A").IsUnique();

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
