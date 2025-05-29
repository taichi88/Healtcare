
using Application.Services;
using HealthcareApi.Domain.Models;
using HealthcareApi.application.Interfaces;
using HealthcareApi.Application.DTO;
using Microsoft.AspNetCore.Mvc;


namespace HealthcareApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost]
        public async Task<ActionResult<PatientDto>> CreatePatient([FromBody] PatientDto dto)
        {
            var createdPatient = await _patientService.CreatePatientAsync(dto);

            return Ok();    
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDto>> GetPatientById(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null)
                return NotFound();
            return Ok(patient);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<PatientDto>> UpdatePatient(int id, PatientDto dto)
        {
            var updated = await _patientService.UpdatePatientAsync(id, dto);
            return Ok(updated);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var deleted = await _patientService.DeletePatientAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent(); // 204 on success
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetAllPatients()
        {
            var patients = await _patientService.GetAllPatientAsync();
            return Ok(patients);
        }

    }

}
