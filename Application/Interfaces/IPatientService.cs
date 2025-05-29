using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthcareApi.Domain.Models;
using HealthcareApi.Application.DTO;



namespace HealthcareApi.application.Interfaces
{
    public interface IPatientService
    {
        Task<PatientDto> CreatePatientAsync(PatientDto dto);

        Task<PatientDto> UpdatePatientAsync(int id, PatientDto dto);
        Task<PatientDto?> GetPatientByIdAsync(int id);
        Task<bool> DeletePatientAsync(int id);
        Task<IEnumerable<PatientDto>> GetAllPatientAsync();
    }
}
