using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthcareApi.Application.DTO;

namespace HealthcareApi.Application.Interfaces
{
    public interface IDoctorService
    {
        Task<DoctorDto> CreateDoctorAsync(DoctorDto dto);

        Task<DoctorDto> UpdateDoctorAsync(int id, DoctorDto dto);
        Task<DoctorDto?> GetDoctorByIdAsync(int id);
        Task<bool> DeleteDoctorAsync(int id);
        Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync();
    }
}
