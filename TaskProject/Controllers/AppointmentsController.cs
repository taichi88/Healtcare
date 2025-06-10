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
using HealthcareApi.Application.Interfaces;
using Application.Services;



namespace HealthcareApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
       
        public AppointmentsController(IAppointmentService appointmentService)
        {
           _appointmentService = appointmentService;
        }
        /// <summary>
        /// Creates a new appointment.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<int>> CreateAppointmentAsync([FromBody] AppointmentsDto dto)
        {
            var createdAppointmentId = await _appointmentService.CreateAppointmentAsync(dto);
            return Ok(createdAppointmentId);
        }

        /// <summary>
        /// Gets all appointments.
        /// </summary>
        /// <returns>A list of all appointments.</returns>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAllAppointmentsAsync()
        {            
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return Ok(appointments);
        }

        /// <summary>
        /// Gets an appointment by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentsDto>> GetAppointmentByIdAsync([FromRoute] int id)
        {
 var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            return Ok(appointment); 
        }

       /// <summary>
       ///  Update Appointment 
       /// </summary>
       /// <param name="id"></param>
       /// <param name="updatedAppointment"></param>
       /// <returns></returns>
       /// 
        [HttpPut("{id}")]
        public async Task<ActionResult<AppointmentsDto>> UpdateAppointment([FromRoute] int id, AppointmentsDto dto)
        {

            var updated = await _appointmentService.UpdateAppointmentAsync(id, dto);
            return Ok(updated);
        }
       /// <summary>
       ///  Delete Appointment
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteAppointment([FromRoute] int id)
        {
            var deleted = _appointmentService.DeleteAppointmentAsync(id);
            return Ok(deleted);
        }
    }
}
