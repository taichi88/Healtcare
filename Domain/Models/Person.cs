using System;
using System.Collections.Generic;

namespace HealthcareApi.Api.Models;

public partial class Person
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? Email { get; set; }

    public string PersonalNumber { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public virtual DeskStaff? DeskStaff { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual Patient? Patient { get; set; }
}
