using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthcareApi.Application.DTO;



namespace HealthcareApi.application.Interfaces
{
    internal interface IPatientService
    {
        Task<PatientDto> CreatePersonAsync(PatientDto dto);
        Task<PatientDto> UpdatePersonAsync(int id, PersonDto dto);
        Task<PatientDto?> GetByIdAsync(int id);
        Task<bool> DeletePersonAsync(int id);
    }
}
