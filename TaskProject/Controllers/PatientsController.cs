using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using TaskProject.Models;
using TaskProject.Models.Dto;
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
        /// <summary>
        /// Gets all Registered Patients.
        /// </summary>
        /// <returns> A list of all Patients.</returns>
        
        // GET: api/Patients
        [HttpGet]
        public ActionResult<IEnumerable<Patient>> GetPatients()
        {
            return Ok(Patients);  // Return mocked patient data
        }
        /// <summary>
        /// Register New Patient of our clinic.
        /// </summary>
        /// <returns> Added Patient.</returns>
        /// 
        // POST: api/Patients
        [HttpPost]
        public ActionResult<Patient> CreatePatient(CreatePatientDto dto)
        {
            var newPatient = new Patient
            {
                PatientId = Patients.Count + 1,
                Name = dto.Name,
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth
            };

            Patients.Add(newPatient);
            return CreatedAtAction(nameof(GetPatients), new { id = newPatient.PatientId }, newPatient);
        }

    }

}
