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
        Task<IEnumerable<PatientDto>> GetAllPatientAsync();
        Task<PatientDto?> GetPatientByIdAsync(int id);
        Task<PatientDto?> UpdatePatientAsync(int id, PatientDto dto);
        Task<bool> DeletePatientAsync(int id);
      
    }
}
