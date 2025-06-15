
using Microsoft.AspNetCore.Mvc;
using HealthcareApi.application.Interfaces;
using HealthcareApi.Application.DTO;
using Application.Services;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


namespace HealthcareApi.Api.Controllers
{
    [Authorize(Roles = "Admin")]

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)] // If not logged in
    [ProducesResponseType(StatusCodes.Status403Forbidden)]

    [ApiController]
    [Route("api/[controller]")]

    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }
        /// <summary>
        /// Create a new person
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<PersonDto>> CreatePerson([FromForm] PersonDto dto)
        {
            var createdPerson = await _personService.CreatePersonAsync(dto);
            return Ok(createdPerson);
        }
        /// <summary>
        /// Get all persons
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetAllPersonAsync()
        {
            var persons = await _personService.GetAllPersonsAsync();

            return Ok(persons);
        }

        /// <summary>
        /// Get person by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDto>> GetById(int id)
        {
            var person = await _personService.GetByIdAsync(id);
            if (person == null)
                return NotFound();
            return Ok(person);
        }
       
        /// <summary>
        /// Update a person by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<PersonDto>> UpdatePerson(int id, [FromForm] PersonDto dto)
        {
            var updated = await _personService.UpdatePersonAsync(id, dto);
            return Ok(updated);
        }
        /// <summary>
        /// Delete a person
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var deleted = await _personService.DeletePersonAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent(); // 204 on success
        }

    }
}
