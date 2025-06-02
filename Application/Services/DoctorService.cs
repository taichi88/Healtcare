using System;
using System.Collections.Generic;

using HealthcareApi.Domain.Models;
using HealthcareApi.Application.DTO;
using HealthcareApi.Application.Interfaces;
using HealthcareApi.Application.IUnitOfWork;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class DoctorService : IDoctorService

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PatientService> _logger;
        public DoctorService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<PatientService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;

        }
        public async Task<DoctorDto> CreateDoctorAsync(DoctorDto dto)
        {
            _logger.LogInformation("Creating new doctor with PersonId: {PersonId}", dto.PersonId);


            var doctor = _mapper.Map<Doctor>(dto);
            var createdDoctor = await _unitOfWork.Doctors.AddDoctorAsync(doctor);

            _logger.LogInformation("Doctor created successfully with PersonId: {PersonId}", createdDoctor.PersonId);

            return _mapper.Map<DoctorDto>(createdDoctor);

        }


        public async Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync()
        {
            _logger.LogInformation("Retrieving all doctors");


            var doctors = await _unitOfWork.Doctors.GetAllDoctorsAsync();
            _logger.LogInformation("Retrieved {Count} doctors", doctors?.Count() ?? 0);

            return _mapper.Map<IEnumerable<DoctorDto>>(doctors);
        }


        public async Task<DoctorDto> GetDoctorByIdAsync(int id)
        {
            _logger.LogInformation("Retrieving doctor by Id: {DoctorId}", id);

            var doctor = await _unitOfWork.Doctors.GetDoctorByIdAsync(id);
            if (doctor == null)
            {
                _logger.LogWarning("Doctor with Id: {DoctorId} not found", id);
                throw new KeyNotFoundException();
            }
            return _mapper.Map<DoctorDto>(doctor);
        }

        public async Task<DoctorDto> UpdateDoctorAsync(int id, DoctorDto dto)
        {
            _logger.LogInformation("Updating doctor with Id: {DoctorId}", id);

            var doctor = await _unitOfWork.Doctors.GetDoctorByIdAsync(id);
            if (doctor == null)
            {
                _logger.LogWarning("Doctor with Id: {DoctorId} not found for update", id);
                throw new KeyNotFoundException();
            }

            _mapper.Map(dto, doctor); 

            await _unitOfWork.Doctors.UpdateDoctorAsync(doctor);
            await _unitOfWork.CommitAsync();

            _logger.LogInformation("Doctor with Id: {DoctorId} updated successfully", id);


            return _mapper.Map<DoctorDto>(doctor);
        }
        public async Task<bool> DeleteDoctorAsync(int id)
        {
            _logger.LogInformation("Deleting doctor with Id: {DoctorId}", id);

            var result = await _unitOfWork.Doctors.DeleteDoctorAsync(id);
            if (result)
                _logger.LogInformation("Doctor with Id: {DoctorId} deleted successfully", id);
            else
                _logger.LogWarning("Failed to delete doctor with Id: {DoctorId}", id);

            return result;
        }
    }
}

