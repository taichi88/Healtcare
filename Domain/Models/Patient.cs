using System;
using System.Collections.Generic;

namespace HealthcareApi.Domain.Models;

public partial class Patient
{
    public int Id { get; set; }

    public int PersonId { get; set; }

    public string? InsuranceNumber { get; set; }

    public string? EmergencyContactName { get; set; }

    public string? EmergencyContactPhone { get; set; }

    public string? BloodType { get; set; }

    public string? Allergies { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();

    public virtual Person IdNavigation { get; set; } = null!;
}
