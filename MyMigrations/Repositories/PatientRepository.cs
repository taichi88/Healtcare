using System;
using HealthcareApi.Domain.Models;
using HealthcareApi.Domain.IRepositories;
using HealthcareApi.Infrastructure ;
using Microsoft.EntityFrameworkCore;

namespace HealthcareApi.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly HealthcareApiContext _context;

        public PatientRepository(HealthcareApiContext context)
        {
            _context = context;
        }

        public async Task<Patient> AddPatientAsync(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            return await _context.Patients.FindAsync(id);
        }

        public async Task<IEnumerable<Patient>> GetAllPatientAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) return false;
            
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
                return true;
            
        }
    }

}