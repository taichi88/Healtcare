using System;
using System.Collections.Generic;

using HealthcareApi.Domain.Models;
using HealthcareApi.Application.DTO;
using HealthcareApi.Application.Interfaces;
using HealthcareApi.Application.IUnitOfWork;

namespace Application.Services
{
    public class DoctorService : IDoctorService

    {
        private readonly IUnitOfWork _unitOfWork;
        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<DoctorDto> CreateDoctorAsync(DoctorDto dto)
        {
            var doctor = new Doctor
            {
                PersonId = dto.PersonId,
                Specialty = dto.Specialty,
            };

            var createdDoctor = await _unitOfWork.Doctors.AddDoctorAsync(doctor);

            return new DoctorDto
            {
                PersonId = createdDoctor.PersonId,
                Specialty = createdDoctor.Specialty,
            };

        }
        public async Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync()
        {
            var doctors = await _unitOfWork.Doctors.GetAllDoctorsAsync();
            return doctors.Select(d => new DoctorDto
            {
                PersonId = d.PersonId,
                Specialty = d.Specialty,
            });
        }
        public async Task<DoctorDto> GetDoctorByIdAsync(int id)
        {
            var doctor = await _unitOfWork.Doctors.GetDoctorByIdAsync(id);
            if (doctor == null) throw new KeyNotFoundException();
            return MapToDoctorDto(doctor);
            
        }

        public async Task<DoctorDto> UpdateDoctorAsync(int id, DoctorDto dto)
        {
            var doctor = await _unitOfWork.Doctors.GetDoctorByIdAsync(id);
            if (doctor == null) throw new KeyNotFoundException();

            // map only the updatable fields
            doctor.PersonId = dto.PersonId;
            doctor.Specialty = dto.Specialty;
            await _unitOfWork.Doctors.UpdateDoctorAsync(doctor);
            await _unitOfWork.CommitAsync();
            return MapToDoctorDto(doctor);
            


        }
        public async Task<bool> DeleteDoctorAsync(int id)
        {
            return await _unitOfWork.Doctors.DeleteDoctorAsync(id);
        }

        private DoctorDto MapToDoctorDto(Doctor doctor)
        {
            return new DoctorDto
            {
                PersonId = doctor.PersonId,
                Specialty = doctor.Specialty,
            };
        }

    }
}

