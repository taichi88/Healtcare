using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthcareApi.Domain.IRepositories;

namespace HealthcareApi.Application.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IPersonRepository Persons { get; }
        IPatientRepository Patients { get; }
        
        // add other repositories here

        Task<int> CommitAsync();
        void Rollback();  // optional if you want explicit rollback
    }
}
