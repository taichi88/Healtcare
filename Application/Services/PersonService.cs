
using HealthcareApi.Domain.Models;
using HealthcareApi.application.Interfaces;
using HealthcareApi.Application.DTO;
using HealthcareApi.Application.IUnitOfWork;


namespace Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PersonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                Phone = dto.Phone,
                
            };
            var createdPerson = await _unitOfWork.Persons.AddPersonAsync(person);

            return new PersonDto
            {
                Name = createdPerson.Name,
                Surname = createdPerson.Surname,
                PersonalNumber = createdPerson.PersonalNumber,
                Phone = createdPerson.Phone,
                Email = createdPerson.Email,
                Address = createdPerson.Address,
                
            };
        }
        public async Task<PersonDto> UpdatePersonAsync(int id, PersonDto dto)
        {
            var person = await _unitOfWork.Persons.GetByIdAsync(id);
            if (person == null) throw new KeyNotFoundException();

            // map only the updatable fields
            person.Name = dto.Name;
            person.Email = dto.Email;
            person.Address = dto.Address;
            // …etc…
            await _unitOfWork.Persons.UpdateAsync(person);
            // map back to DTO
            return new PersonDto
            {
                Name = person.Name,
                Email = person.Email,              
                // …etc…
            };
        }
        public async Task<PersonDto?> GetByIdAsync(int id)
        {
            var person = await _unitOfWork.Persons.GetByIdAsync(id);
            if (person == null) return null;

            return new PersonDto
            {
                Name = person.Name,
                Surname = person.Surname,
                Email = person.Email,
                Address = person.Address,
                PersonalNumber = person.PersonalNumber,
                Phone = person.Phone,
                
            };
        }
        public async Task<bool> DeletePersonAsync(int id)
        {
            return await _unitOfWork.Persons.DeleteAsync(id);
        }
    }
}
