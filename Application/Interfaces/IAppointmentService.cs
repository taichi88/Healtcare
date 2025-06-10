using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthcareApi.Application.DTO;
using HealthcareApi.Domain.Models;


namespace HealthcareApi.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<AppointmentsDto> CreateAppointmentAsync(AppointmentsDto dto);
        Task<IEnumerable<AppointmentsDto>> GetAllAppointmentsAsync();
        Task<AppointmentsDto?> GetAppointmentByIdAsync(int id);
        Task<AppointmentsDto?> UpdateAppointmentAsync(int id, AppointmentsDto dto);
        Task<bool> DeleteAppointmentAsync(int id);
        
    }
}
