
using HealthcareApi.Domain.Models;
using HealthcareApi.Application.DTO;
using HealthcareApi.Application.IUnitOfWork;
using HealthcareApi.application.Interfaces;


namespace Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PatientDto> CreatePatientAsync(PatientDto dto)
        {
            var patient = new Patient
            {
                PersonId = dto.PersonId,
                InsuranceNumber = dto.InsuranceNumber,
            };
               
           var createdPatient = await _unitOfWork.Patients.AddPatientAsync(patient);

            return new PatientDto
            {
                PersonId = createdPatient.PersonId,
               InsuranceNumber = createdPatient.InsuranceNumber,
            };
        }
        public async Task<IEnumerable<PatientDto>> GetAllPatientAsync()
        {
            var patients = await _unitOfWork.Patients.GetAllPatientAsync();
            return patients.Select(p => MapToPatientDto(p));
        }


        public async Task<PatientDto> GetPatientByIdAsync(int id)
        {
            var patient = await _unitOfWork.Patients.GetPatientByIdAsync(id);
            if (patient == null) throw new KeyNotFoundException();
            return MapToPatientDto(patient);
        }


        public async Task<PatientDto> UpdatePatientAsync(int id, PatientDto dto)
        {
            var patient = await _unitOfWork.Patients.GetPatientByIdAsync(id);
            if (patient == null) throw new KeyNotFoundException();

            patient.PersonId = dto.PersonId;
            patient.InsuranceNumber = dto.InsuranceNumber;
            
            await _unitOfWork.Patients.UpdatePatientAsync(patient);
            await _unitOfWork.CommitAsync();
            return MapToPatientDto(patient);
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            return await _unitOfWork.Patients.DeleteAsync(id);
        }

        private PatientDto MapToPatientDto(Patient patient)
        {
            return new PatientDto
            {
                PersonId = patient.PersonId,
                InsuranceNumber = patient.InsuranceNumber,
            };
        }
    }
}