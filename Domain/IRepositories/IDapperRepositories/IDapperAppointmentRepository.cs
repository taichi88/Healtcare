using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthcareApi.Domain.Models;

namespace HealthcareApi.Domain.IRepositories.IDapperRepositories
{
    public interface IDapperAppointmentRepository
    {
        Task<int> CreateAppointmentAsync(Appointment appointment);
    }
}
