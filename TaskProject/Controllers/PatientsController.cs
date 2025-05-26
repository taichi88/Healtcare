using Application;
using HealthcareApi.Application.DTO;
using HealthcareApi.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;

namespace HealthcareApi.Api.Controllers
{
   

    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        // Mock data for patients
        private static readonly List<Patient> Patients = new List<Patient>
    {
        
    };



        [HttpPost]
        public ActionResult<PatientDto> CreatePatient([FromBody] PatientDto dto)
        {

            return Ok();

        }





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
        /// Register New Patient in our hospital.
        /// </summary>
        /// <returns> Added Patient.</returns>
        /// 
        // POST: api/Patients
        

    }

}
