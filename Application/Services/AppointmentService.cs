using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HealthcareApi.Application.DTO;
using HealthcareApi.Application.Interfaces;
using HealthcareApi.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Linq;
using HealthcareApi.Application.IUnitOfWork;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AppointmentService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        

        public async Task<AppointmentsDto> CreateAppointmentAsync(AppointmentsDto dto)
        {
            _logger.LogInformation("Creating new appointment");

            var appointment = _mapper.Map<Appointment>(dto);
            var createdAppointment = await _unitOfWork.Appointments.CreateAppointmentAsync(appointment);

            _logger.LogInformation("Appointment created successfully");

            return _mapper.Map<AppointmentsDto>(createdAppointment);
        }

        public async Task<IEnumerable<AppointmentsDto>> GetAllAppointmentsAsync()
        {
            _logger.LogInformation("Retrieving all appointments");

            var appointments = await _unitOfWork.Appointments.GetAllAppointmentsAsync();

            _logger.LogInformation("Retrieved {Count} appointments", appointments?.Count() ?? 0);

            return _mapper.Map<IEnumerable<AppointmentsDto>>(appointments);
        }

        public async Task<AppointmentsDto?> GetAppointmentByIdAsync(int id)
        {
            _logger.LogInformation("Retrieving appointment by Id: {AppointmentId}", id);

            var appointment = await _unitOfWork.Appointments.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                _logger.LogWarning("Appointment with Id: {AppointmentId} not found", id);
                return null;
            }

            return _mapper.Map<AppointmentsDto>(appointment);
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            _logger.LogInformation("Deleting appointment with Id: {AppointmentId}", id);

            var result = await _unitOfWork.Appointments.DeleteAppointmentAsync(id);

            if (result)
                _logger.LogInformation("Appointment with Id: {AppointmentId} deleted successfully", id);
            else
                _logger.LogWarning("Failed to delete appointment with Id: {AppointmentId}", id);
            return result;
        }

        public async Task<AppointmentsDto?> UpdateAppointmentAsync(int id, AppointmentsDto dto)
        {
            _logger.LogInformation("Updating appointment with Id: {AppointmentId}", id);

            var appointment = await _unitOfWork.Appointments.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                _logger.LogWarning("Appointment with Id: {AppointmentId} not found for update", id);
                throw new KeyNotFoundException();
            }

            _mapper.Map(dto, appointment);
            await _unitOfWork.Appointments.UpdateAppointmentAsync(id, appointment);

            _logger.LogInformation("Appointment with Id: {AppointmentId} updated successfully", id);

            return _mapper.Map<AppointmentsDto>(appointment);
        }
    }

}

