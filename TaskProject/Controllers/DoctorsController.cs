
using HealthcareApi.Application.DTO;

using HealthcareApi.Application.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace HealthcareApi.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorservice;
        
        public DoctorsController(IDoctorService doctorservice)
        {
            _doctorservice = doctorservice;
        }
        /// <summary>
        /// Create a new doctor
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<DoctorDto>> CreateDoctor([FromBody] DoctorDto dto)
        {
            var createdDoctor = await _doctorservice.CreateDoctorAsync(dto);

            return Ok(createdDoctor);
        }
        /// <summary>
        /// Get all doctors
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetAllDoctors()
        {
            var doctors = await _doctorservice.GetAllDoctorsAsync();
            return Ok(doctors);
        }
        /// <summary>
        /// Get a doctor by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDto>> GetDoctorById(int id)
        {
            var doctor = await _doctorservice.GetDoctorByIdAsync(id);
            if (doctor == null)
                return NotFound();
            return Ok(doctor);
        }
        /// <summary>
        /// Update a doctor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<DoctorDto>> UpdateDoctor(int id, DoctorDto dto)
        {
            var updated = await _doctorservice.UpdateDoctorAsync(id, dto);
            return Ok(updated);
        }
        /// <summary>
        /// Delete a doctor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var deleted = await _doctorservice.DeleteDoctorAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent(); // 204 on success
        }        
    }
}

