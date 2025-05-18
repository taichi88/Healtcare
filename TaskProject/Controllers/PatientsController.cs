using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using TaskProject.Models;
namespace TaskProject.Controllers
{
   

    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        // Mock data for patients
        private static readonly List<Patient> Patients = new List<Patient>
    {
        new Patient { PatientId = 1, Name = "Alice Smith", Email = "alice@example.com", DateOfBirth = new DateTime(1990, 1, 1) },
        new Patient { PatientId = 2, Name = "Bob Johnson", Email = "bob@example.com", DateOfBirth = new DateTime(1985, 5, 15) }
    };

        // GET: api/Patients
        [HttpGet]
        public ActionResult<IEnumerable<Patient>> GetPatients()
        {
            return Ok(Patients);  // Return mocked patient data
        }

        // POST: api/Patients
        [HttpPost]
        public ActionResult<Patient> CreatePatient(Patient patient)
        {
            patient.PatientId = Patients.Count + 1;  // Assign a new ID
            Patients.Add(patient);
            return CreatedAtAction(nameof(GetPatients), new { id = patient.PatientId }, patient);  // Return the created patient
        }
    }

}
