using HealthcareApi.Domain.Models;
using HealthcareApi.Application.DTO;
using HealthcareApi.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using HealthcareApi.Domain.IRepositories.IDapperRepositories;
using AutoMapper;



namespace HealthcareApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        // Mock data for appointments static list
        private static readonly List<Appointment> Appointments = new List<Appointment>
        {
           
        };
        private readonly IDapperAppointmentRepository _repository;
        private readonly IMapper _mapper;
        public AppointmentsController(IDapperAppointmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all appointments.
        /// </summary>
        /// <returns>A list of all appointments.</returns>
      
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAllAppointmentsAsync()
        {

            
                var appointments = await _repository.GetAllAppointmentsAsync();
                var dtoList = _mapper.Map<IEnumerable<AppointmentsDto>>(appointments);
                return Ok(dtoList);
            
        }
        [HttpPost]
        public async Task<ActionResult<int>> CreateAppointmentAsync([FromBody] AppointmentsDto dto)
        {
            var mappedAppointment = _mapper.Map<Appointment>(dto);
            var id = await _repository.CreateAppointmentAsync(mappedAppointment);
            return Ok(id); // returns HTTP 200 with created appointment ID
        }





        [HttpGet("{id}")]
public async Task<ActionResult<Appointment>> GetAppointmentByIdAsync(int id)
{
    var appointment = await _repository.GetAppointmentByIdAsync(id);
    if (appointment == null)
        return NotFound();

    return Ok(appointment);
}

        /// <summary>
        /// Updates an existing appointment by ID.
        /// </summary>
        /// <param name="id">The ID of the appointment to update.</param>
        /// <param name="updatedAppointment">The updated appointment details.</param>
        /// <returns>The updated appointment.</returns>
        /// <response code="200">Appointment successfully updated.</response>
        /// <response code="400">Invalid appointment data.</response>
        /// <response code="404">Appointment not found.</response>
        [HttpPut("{id}")]
        public ActionResult<Appointment> UpdateAppointment([FromRoute] int id, Appointment updatedAppointment)
        {
            return Ok();
        }
        /// <summary>
        /// Deletes an appointment by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the appointment to delete.</param>
        /// <returns>Status code indicating the result of the delete operation.</returns>
        /// <response code="204">Appointment successfully deleted.</response>
        /// <response code="404">Appointment not found.</response>
        [HttpDelete("{id}")]
        public ActionResult DeleteAppointment([FromRoute] int id)
        {
            var appointment = Appointments.Find(a => a.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }
            Appointments.Remove(appointment);
            return NoContent();
        }
    }
}
