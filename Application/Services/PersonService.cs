using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.IRepositories;
using Domain.Models.Dto;
using TaskProject.Models;

namespace Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<PersonDto> CreatePersonAsync(PersonDto dto)
        {

            var person = new Person
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                Address = dto.Address,
                PersonalNumber = dto.PersonalNumber,
                DateOfBirth = dto.DateOfBirth,
                Phone = dto.Phone,
                Role = dto.Role.ToString(),

                // Map other fields...
            };

            var createdPerson = await _personRepository.AddPersonAsync(person);

            return new PersonDto
            {
                Name = createdPerson.Name,
                Surname = createdPerson.Surname,
                PersonalNumber = createdPerson.PersonalNumber,
                DateOfBirth = createdPerson.DateOfBirth,
                Phone = createdPerson.Phone,
                Role = Enum.TryParse<RoleType>(createdPerson.Role, out var role) ? role : RoleType.Patient,
                Email = createdPerson.Email,
                Address = createdPerson.Address,
                // Map other fields...
            };


        }
        public async Task<PersonDto> UpdatePersonAsync(int id, PersonDto dto)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null) throw new KeyNotFoundException();

            // map only the updatable fields
            person.Name = dto.Name;
            person.Email = dto.Email;
            person.Address = dto.Address;
            person.Role = dto.Role.ToString();
            // …etc…

            await _personRepository.UpdateAsync(person);

            // map back to DTO
            return new PersonDto
            {
                Name = person.Name,
                Email = person.Email,
                Role = Enum.TryParse<RoleType>(person.Role, out var role) ? role : RoleType.Patient,
                // …etc…
            };
        }
        public async Task<PersonDto?> GetByIdAsync(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null) return null;

            return new PersonDto
            {
                Name = person.Name,
                Surname = person.Surname,
                Email = person.Email,
                Address = person.Address,
                PersonalNumber = person.PersonalNumber,
                DateOfBirth = person.DateOfBirth,
                Phone = person.Phone,
                Role = Enum.TryParse<RoleType>(person.Role, out var r) ? r : RoleType.Patient
            };
        }
        public async Task<bool> DeletePersonAsync(int id)
        {
            return await _personRepository.DeleteAsync(id);
        }
    }
}
