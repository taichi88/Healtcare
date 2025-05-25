using Domain.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using TaskProject.Models;
using TaskProject.Models.Dto;
using Domain.IRepositories;
using Application.Services;
using Application.Interfaces;

namespace TaskProject.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

  
    public class PersonsController : Controller
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
        public async Task<ActionResult<PersonDto>> UpdatePerson(int id, [FromBody] PersonDto dto)
        {
            var updated = await _personService.UpdatePersonAsync(id, dto);
            return Ok(updated);
        }


    }
}
