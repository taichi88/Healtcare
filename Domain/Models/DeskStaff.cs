using System;
using System.Collections.Generic;

namespace HealthcareApi.Api.Models;

public partial class DeskStaff
{
    public int PersonId { get; set; }

    public virtual Person Person { get; set; } = null!;
}
