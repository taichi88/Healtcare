using System;
using System.Collections.Generic;

namespace HealthcareApi.Api.Models;

public partial class Diagnosis
{
    public int DiagnosisId { get; set; }

    public int? PatientId { get; set; }

    public DateOnly DiagnosisDate { get; set; }

    public string? Description { get; set; }

    public string? PrescribedTreatment { get; set; }

    public int? DoctorId { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual Patient? Patient { get; set; }
}
