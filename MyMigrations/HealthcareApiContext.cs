using System;
using System.Collections.Generic;
using HealthcareApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApi.Infrastructure;
//sda
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
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCC210FC17F5");

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
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appointments_Doctor");

            entity.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appointments_Patient");
        });

        modelBuilder.Entity<DeskStaff>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__DeskStaf__AA2FFBE5BA79A8F7");

            entity.ToTable("DeskStaff", "Core");

            entity.Property(e => e.PersonId).ValueGeneratedNever();

            entity.HasOne(d => d.Person).WithOne(p => p.DeskStaff)
                .HasForeignKey<DeskStaff>(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DeskStaff__Perso__276EDEB3");
        });

        modelBuilder.Entity<Diagnosis>(entity =>
        {
            entity.HasKey(e => e.DiagnosisId).HasName("PK__Diagnose__0C54CC733CCFE268");

            entity.ToTable("Diagnoses", "Clinical");

            entity.HasIndex(e => e.DiagnosisDate, "IX_Diagnoses_Date");

            entity.HasIndex(e => e.DoctorId, "IX_Diagnoses_DoctorId");

            entity.HasIndex(e => e.PatientId, "IX_Diagnoses_PatientId");

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PrescribedTreatment).HasColumnType("text");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Diagnoses)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Diagnoses_Doctor");

            entity.HasOne(d => d.Patient).WithMany(p => p.Diagnoses)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Diagnoses_Patient");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__Doctors__AA2FFBE580CF42CE");

            entity.ToTable("Doctors", "Core");

            entity.HasIndex(e => e.Specialty, "IX_Doctors_Specialty");

            entity.Property(e => e.PersonId).ValueGeneratedNever();
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
            entity.HasKey(e => e.PersonId).HasName("PK__Patients__AA2FFBE5B7E5C374");

            entity.ToTable("Patients", "Core");

            entity.HasIndex(e => e.PersonId, "IX_Patients_PersonId");

            entity.Property(e => e.PersonId).ValueGeneratedNever();
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
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A387F7B3F5E");

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
            entity.HasKey(e => e.Id).HasName("PK__Persons__3214EC074CA7FA65");

            entity.ToTable("Persons", "Core");

            entity.HasIndex(e => e.Surname, "IX_Persons_Surname");

            entity.HasIndex(e => e.PersonalNumber, "UQ__Persons__AC2CC42EC6F9BC79").IsUnique();

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
