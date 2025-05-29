using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthcareApi.Domain.Models;
using HealthcareApi.Application.IUnitOfWork;
using HealthcareApi.Domain.IRepositories;
using HealthcareApi.Infrastructure.Repositories;
using HealthcareApi.Infrastructure;

namespace HealthcareApi.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly HealthcareApiContext _context;
        private IDbTransaction _transaction;
        //ამის მაგივრად დეფენდენსი იჯექშენი გავაკეთ ჯობია. 
        public UnitOfWork(HealthcareApiContext context)
        {
            _context = context;
            Persons = new PersonRepository(_context);
            Doctors = new DoctorRepository(_context);
        }

        //ეს არის ერთდაიგივე და აქ რომ უნიტოფორკის ობიექტს ვქმნი სამივე ინიცილიზდება.
        public IPatientRepository Patients => new PatientRepository(_context);
        public IPersonRepository Persons { get; }
        public IDoctorRepository Doctors { get; }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public void BeginTransaction()
        {
            _transaction = (IDbTransaction)_context.Database.BeginTransaction();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
