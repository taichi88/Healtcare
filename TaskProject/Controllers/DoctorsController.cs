using HealthcareApi.Application.DTO;
using HealthcareApi.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HealthcareApi.Api.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        // Mock data for doctors
        private static readonly List<Doctor> Doctors = new List<Doctor>
        {
        
        };
        /// <summary>
        /// Gets all Doctors.
        /// </summary>
        /// <returns> A all Doctors who work.</returns>
        // GET: api/Doctors
        [HttpGet]
        public ActionResult<IEnumerable<Doctor>> GetDoctors()
        {
            return Ok(Doctors);  // Return mocked doctor data
        }
        /// <summary>
        /// Hire new Doctor.
        /// </summary>
        /// <returns> Hired Doctor.</returns>
        /// 

        [HttpPost]
        public ActionResult<Doctor> CreateDoctor([FromBody] DoctorDto dto)
        {
            return Ok();
        }

    }
}

