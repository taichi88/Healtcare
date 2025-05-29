using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthcareApi.Domain.Models;

namespace HealthcareApi.Domain.IRepositories
{
    public interface IDoctorRepository
    {
        Task<Doctor> AddDoctorAsync(Doctor doctor);

        Task<Doctor> GetDoctorByIdAsync(int id);
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();

        Task UpdateDoctorAsync(Doctor doctor);


        Task<bool> DeleteDoctorAsync(int id);
    }
}
