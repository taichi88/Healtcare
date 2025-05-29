using HealthcareApi.Api.Models;
using HealthcareApi.Domain.IRepositories;
using HealthcareApi.Infrastructure;
using Microsoft.EntityFrameworkCore;

public class PatientRepository : IPatientRepository
{
    private readonly HealthcareApiContext _context;

    public PatientRepository(HealthcareApiContext context)
    {
        _context = context;
    }

    // Add dummy method (e.g., GetAll) to satisfy the interface
    public Task<List<Patient>> GetAllAsync()
    {
        return _context.Patients.ToListAsync();
    }
}

