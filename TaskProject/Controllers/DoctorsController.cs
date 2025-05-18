using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskProject.Models;

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

        // GET: api/Doctors
        [HttpGet]
        public ActionResult<IEnumerable<Doctor>> GetDoctors()
        {
            return Ok(Doctors);  // Return mocked doctor data
        }
    }

}
