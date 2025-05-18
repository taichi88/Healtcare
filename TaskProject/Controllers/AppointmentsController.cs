using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskProject.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using TaskProject.Models;

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

        // GET: api/Appointments
        [HttpGet]
        public ActionResult<IEnumerable<Appointment>> GetAppointments()
        {
            return Ok(Appointments);  // Return mocked appointment data
        }

        // POST: api/Appointments
        [HttpPost]
        public ActionResult<Appointment> CreateAppointment(Appointment appointment)
        {
            appointment.AppointmentId = Appointments.Count + 1;  // Assign a new ID
            Appointments.Add(appointment);
            return CreatedAtAction(nameof(GetAppointments), new { id = appointment.AppointmentId }, appointment);  // Return the created appointment
        }
    }

}
