using System;
using System.Collections.Generic;

namespace HealthcareApi.Domain.Models;

public partial class DeskStaff
{
    public int PersonId { get; set; }

    public virtual Person Person { get; set; } = null!;
}
