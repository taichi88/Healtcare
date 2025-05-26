
using Microsoft.AspNetCore.Mvc;



using Application.Services;

using HealthcareApi.application.Interfaces;
using HealthcareApi.Application.DTO;

namespace HealthcareApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    

  
    public class PersonsController : ControllerBase
    {

        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }


        [HttpPost]
        public async Task<ActionResult<PersonDto>> CreatePerson([FromBody] PersonDto dto)
        {
           

            var createdPerson = await _personService.CreatePersonAsync(dto);

           

            return Ok();

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDto>> GetById(int id)
        {
            var person = await _personService.GetByIdAsync(id);
            if (person == null)
                return NotFound();
            return Ok(person);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<PersonDto>> UpdatePerson(int id,   PersonDto dto)
        {
            var updated = await _personService.UpdatePersonAsync(id, dto);
            return Ok(updated);
        }
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
