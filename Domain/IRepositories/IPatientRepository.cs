using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthcareApi.Domain.Models;



namespace HealthcareApi.Domain.IRepositories
{
    public interface IPatientRepository
    {
        Task<Patient> AddPatientAsync(Patient patient);



        Task<Patient> GetPatientByIdAsync(int id);
        Task<IEnumerable<Patient>> GetAllPatientAsync();
        
        Task UpdatePatientAsync(Patient patient);
        Task<bool> DeleteAsync(int id);
    }

}
