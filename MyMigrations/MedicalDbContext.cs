using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using TaskProject.Models;

public class MedicalDbContext : DbContext

{
    public MedicalDbContext(DbContextOptions<MedicalDbContext> options) : base(options) { }

    public DbSet<Person> Persons { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Diagnosis> Diagnoses { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<DailyReport> DailyReports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DailyReport>().HasNoKey();
        // Person-Patient 1:1
        modelBuilder.Entity<Person>()
            .HasOne(p => p.Patient)
            .WithOne(pat => pat.Person)
            .HasForeignKey<Patient>(pat => pat.Id);

        // Person-Doctor 1:1
        modelBuilder.Entity<Person>()
            .HasOne(p => p.Doctor)
            .WithOne(doc => doc.Person)
            .HasForeignKey<Doctor>(doc => doc.Id);

        // Patient-Appointments 1:M
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Patient)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        // Doctor-Appointments 1:M
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Doctor)
            .WithMany(d => d.Appointments)
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Patient-Diagnoses 1:M
        modelBuilder.Entity<Diagnosis>()
            .HasOne(d => d.Patient)
            .WithMany(p => p.Diagnoses)
            .HasForeignKey(d => d.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        // Doctor-Diagnoses 1:M
        modelBuilder.Entity<Diagnosis>()
            .HasOne(d => d.Doctor)
            .WithMany(doc => doc.Diagnoses)
            .HasForeignKey(d => d.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Appointment-Payment 1:1
        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Appointment)
            .WithOne(a => a.Payment)
            .HasForeignKey<Payment>(p => p.AppointmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    //public class MedicalDbContextFactory : IDesignTimeDbContextFactory<MedicalDbContext>
    //{
    //    public MedicalDbContext CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<MedicalDbContext>();
    //        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Hospital;Trusted_Connection=True;TrustServerCertificate=True;"); // replace with your actual connection string or load from config

    //        return new MedicalDbContext(optionsBuilder.Options);
    //    }
    //}
    

}
//public static class DataExtensions1
//{
//    public static WebApplication ApplyMigrations(this WebApplication app)
//    {
//        using var scope = app.Services.CreateScope();
//        var db = scope.ServiceProvider.GetRequiredService<MedicalDbContext>();
//        db.Database.Migrate();
//        return app;
//    }
//}
