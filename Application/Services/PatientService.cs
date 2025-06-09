
using HealthcareApi.Domain.Models;
using HealthcareApi.Application.DTO;
using HealthcareApi.Application.IUnitOfWork;
using HealthcareApi.application.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;


namespace Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PatientService> _logger;

        public PatientService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<PatientService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;

        }

        public async Task<PatientDto> CreatePatientAsync(PatientDto dto)
        {
            _logger.LogInformation("Creating a new patient with PatientId: {PatientId}", dto.PersonId);

            var patient = _mapper.Map<Patient>(dto);
            var createdPatient = await _unitOfWork.Patients.AddPatientAsync(patient);

            _logger.LogInformation("Patient created successfully with PatientId: {PatientId}", createdPatient.PersonId);


            return _mapper.Map<PatientDto>(createdPatient);
        }


        public async Task<IEnumerable<PatientDto>> GetAllPatientAsync()
        {
            _logger.LogInformation("Retrieving all patients");


            var patients = await _unitOfWork.Patients.GetAllPatientAsync();

            _logger.LogInformation("Retrieved {Count} patients", patients?.Count() ?? 0);

            return _mapper.Map<IEnumerable<PatientDto>>(patients);
        }

        public async Task<PatientDto?> GetPatientByIdAsync(int id)
        {
            _logger.LogInformation("Retrieving patient by Id: {PatientId}", id);

            var patient = await _unitOfWork.Patients.GetPatientByIdAsync(id);
            if (patient == null)
            {
                _logger.LogWarning("Patient with Id: {PatientId} not found", id);
                throw new KeyNotFoundException();
            }
            return _mapper.Map<PatientDto>(patient);
        }

        public async Task<PatientDto?> UpdatePatientAsync(int id, PatientDto dto)
        {
            _logger.LogInformation("Updating patient with Id: {PatientId}", id);


            var patient = await _unitOfWork.Patients.GetPatientByIdAsync(id);
            if (patient == null)
            {
                _logger.LogWarning("Patient with Id: {PatientId} not found", id);
                throw new KeyNotFoundException();
            }

            _mapper.Map(dto, patient);

            await _unitOfWork.Patients.UpdatePatientAsync(patient);
            await _unitOfWork.CommitAsync();

            _logger.LogInformation("Patient with Id: {PatientId} updated successfully", id);

            return _mapper.Map<PatientDto>(patient);
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            _logger.LogInformation("Deleting patient with Id: {PatientId}", id);

            var result = await _unitOfWork.Patients.DeleteAsync(id);

            if (result)
                _logger.LogInformation("Patient with Id: {PatientId} deleted successfully", id);
            else
                _logger.LogWarning("Failed to delete patient with Id: {PatientId}", id);

            return result;
        }        
    }
}