
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

        [HttpPost]
        public async Task<ActionResult<DoctorDto>> CreateDoctor([FromBody] DoctorDto dto)
        {
            var createdoDoctor = await _doctorservice.CreateDoctorAsync(dto);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDto>> GetDoctorById(int id)
        {
            var doctor = await _doctorservice.GetDoctorByIdAsync(id);
            if (doctor == null)
                return NotFound();
            return Ok(doctor);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<DoctorDto>> UpdateDoctor(int id, DoctorDto dto)
        {
            var updated = await _doctorservice.UpdateDoctorAsync(id, dto);
            return Ok(updated);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var deleted = await _doctorservice.DeleteDoctorAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent(); // 204 on success
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetAllDoctors()
        {
            var doctors = await _doctorservice.GetAllDoctorsAsync();
            return Ok(doctors);
        }

    }
}

