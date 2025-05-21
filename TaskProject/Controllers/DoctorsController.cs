using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskProject.Models;
using TaskProject.Models.Dto;

namespace TaskProject.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        // Mock data for doctors
        private static readonly List<Doctor> Doctors = new List<Doctor>
    {
        new Doctor { DoctorId = 1, Name = "Dr. Emily Davis", Specialty = "Cardiology" },
        new Doctor { DoctorId = 2, Name = "Dr. Michael Lee", Specialty = "Dentistry" }
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
            var newDoctor = new Doctor
            {
                DoctorId = Doctors.Count + 1,
                Name = dto.Name,
                Specialty = dto.Specialty
            };
            Doctors.Add(newDoctor);
            return CreatedAtAction(nameof(GetDoctors), new { id = newDoctor.DoctorId }, newDoctor);
        }

    }
}

