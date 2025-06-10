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
using Microsoft.EntityFrameworkCore.Storage;
using HealthcareApi.Domain.IRepositories.IDapperRepositories;
using HealthcareApi.Infrastructure.Repositories.DapperRepository;

namespace HealthcareApi.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HealthcareApiContext _context;
        
        private IDbContextTransaction _transaction;

        public UnitOfWork(HealthcareApiContext context )
        {
            _context = context;
            Persons = new PersonRepository(_context);
            Doctors = new DoctorRepository(_context);
            Patients = new PatientRepository(_context);
            Appointments = new DapperAppointmentRepository(_context);
        }
        public IDbTransaction DapperTransaction => _transaction?.GetDbTransaction(); // for dapper
        public IPatientRepository Patients { get; }
        public IPersonRepository Persons { get; }
        public IDoctorRepository Doctors { get; }
        public IDapperAppointmentRepository Appointments { get; }


        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
            return _transaction;
        }
        public async Task<int> CommitAsync()
        {
            if (_transaction != null)
            {
                await _context.SaveChangesAsync();       // Save changes
                await _transaction.CommitAsync();        // Commit transaction
            }
            else
            {
                return await _context.SaveChangesAsync(); // Just save if no transaction
            }

            return 1; // or return number of saved rows if needed
        }
        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
        }
        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
