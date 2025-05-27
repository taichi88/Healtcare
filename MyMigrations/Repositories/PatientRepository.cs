using HealthcareApi.Api.Models;
using HealthcareApi.Domain.IRepositories;
using HealthcareApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

public class PatientRepository : IPatientRepository
{
    private readonly HealthcareSystemDbContext _context;

    public PatientRepository(HealthcareSystemDbContext context)
    {
        _context = context;
    }

    // Add dummy method (e.g., GetAll) to satisfy the interface
    public Task<List<Patient>> GetAllAsync()
    {
        return _context.Patients.ToListAsync();
    }
}

