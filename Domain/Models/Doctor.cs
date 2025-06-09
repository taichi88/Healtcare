using System;
using System.Collections.Generic;

namespace HealthcareApi.Domain.Models;

public partial class Doctor
{
    public int Id { get; set; }

    public int PersonId { get; set; }

    public string? Specialty { get; set; }

    public string? LicenseNumber { get; set; }

    public int? YearsOfExperience { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();

    public virtual Person Person { get; set; } = null!;
}
