
using System;
using HealthcareApi.Domain.Models;
using HealthcareApi.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using HealthcareApi.Infrastructure;

namespace HealthcareApi.Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly HealthcareApiContext _context;

        public DoctorRepository(HealthcareApiContext context)
        {
            _context = context;
        }
        public async Task<Doctor> AddDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return doctor;
        }
        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _context.Doctors.ToListAsync();
        }
        public async Task<Doctor?> GetDoctorByIdAsync(int id)
        {
            return await _context.Doctors.FindAsync(id);
        }       
        public async Task<bool> UpdateDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        public async Task<bool> DeleteDoctorAsync(int id)
        {
            var doctor = await  _context.Doctors.FindAsync(id);
            if (doctor == null) return false;
            
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
                return true;          
        }
    }
}
