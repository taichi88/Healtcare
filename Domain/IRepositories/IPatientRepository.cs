using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareApi.Domain.IRepositories
{
    using HealthcareApi.Domain.Models;

    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllAsync();
    }

}
