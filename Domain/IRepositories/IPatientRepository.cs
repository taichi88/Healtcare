using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthcareApi.Api.Models;

namespace HealthcareApi.Domain.IRepositories
{
   
   

    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllAsync();
    }

}
