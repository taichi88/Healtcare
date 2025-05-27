using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthcareApi.Api.Models;
using HealthcareApi.Application.IUnitOfWork;
using HealthcareApi.Domain.IRepositories;

namespace HealthcareApi.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HealthcareApiContext _context;

        public IPatientRepository Patients { get; }
        public IPersonRepository Persons { get; }
      

        public UnitOfWork(HealthcareApiContext context,
                          IPatientRepository patients,
                          IPersonRepository persons
                          )

        {
            _context = context;
            Patients = patients;
            Persons = persons;  
            
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Rollback()
        {
            // EF Core doesn’t support explicit rollback without transaction scope,
            // but you can implement this if you use explicit transactions.
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
