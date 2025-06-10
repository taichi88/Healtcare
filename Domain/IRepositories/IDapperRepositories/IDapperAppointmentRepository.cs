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
        Task<Appointment?> CreateAppointmentAsync(Appointment appointment);
        Task<Appointment?> GetAppointmentByIdAsync(int id);
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        Task<bool> UpdateAppointmentAsync(int id, Appointment appointment);
        Task<bool> DeleteAppointmentAsync(int id);

    }
}
