using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TaskProject.Models;
using TaskProject.Models.Dto;


namespace TaskProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        // Mock data for appointments static list
        private static readonly List<Appointment> Appointments = new List<Appointment>
        {
            new Appointment { AppointmentId = 1, PatientId = 1, DoctorId = 1, AppointmentDateTime = new DateTime(2025, 5, 20, 10, 0, 0), ReasonForVisit = "General Checkup" },
            new Appointment { AppointmentId = 2, PatientId = 2, DoctorId = 2, AppointmentDateTime = new DateTime(2025, 5, 20, 11, 30, 0), ReasonForVisit = "Dental Cleaning" }
        };

        /// <summary>
        /// Gets all appointments.
        /// </summary>
        /// <returns>A list of all appointments.</returns>
        [HttpGet]

        public ActionResult<IEnumerable<Appointment>> GetAllAppointments()
        {
            return Ok(Appointments);
        }

        /// <summary>
        /// Creates a new appointment.
        /// </summary>
        /// <param name="appointmentDto">The data required to create the appointment.</param>
        /// <returns>The created appointment object.</returns>
        /// <response code="201">Appointment successfully created.</response>
        /// <response code="400">Invalid appointment data.</response>
        [HttpPost]
        
        public ActionResult<Appointment> CreateAppointment([FromBody] AppointmentsDto appointmentDto)
        {
            var appointment = new Appointment
            {
                AppointmentId = Appointments.Count + 1, // this code is untiol i use database Autoincrement constraints.
                PatientId = appointmentDto.PatientId,
                DoctorId = appointmentDto.DoctorId,
                AppointmentDateTime = appointmentDto.AppointmentDateTime,
                ReasonForVisit = appointmentDto.ReasonForVisit
            };

            Appointments.Add(appointment);
            return CreatedAtAction(nameof(GetAppointmentById), new { id = appointment.AppointmentId }, appointment);
        }

        /// <summary>
        /// Gets an appointment by ID.
        /// </summary>
        /// <param name="id">Appointment ID</param>
        /// <returns>The appointment if found.</returns>
        /// <response code="200">Returns the appointment</response>
        /// <response code="404">Appointment not found</response>
        [HttpGet("{id}")]
        public ActionResult<Appointment> GetAppointmentById([FromRoute] int id)
        {
            var appointment = Appointments.Find(a => a.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }
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
            var appointment = Appointments.Find(a => a.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }
            appointment.PatientId = updatedAppointment.PatientId;
            appointment.DoctorId = updatedAppointment.DoctorId;
            appointment.AppointmentDateTime = updatedAppointment.AppointmentDateTime;
            appointment.ReasonForVisit = updatedAppointment.ReasonForVisit;
            return Ok(appointment);
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
