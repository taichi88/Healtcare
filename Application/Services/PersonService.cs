
using HealthcareApi.Domain.Models;
using HealthcareApi.application.Interfaces;
using HealthcareApi.Application.DTO;
using HealthcareApi.Application.IUnitOfWork;
using AutoMapper;
using Microsoft.Extensions.Logging;


namespace Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PersonService> _logger;


        public PersonService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<PersonService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<PersonDto> CreatePersonAsync(PersonDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();

            _logger.LogInformation("Starting CreatePersonAsync for Name: {Name}, Surname: {Surname}", dto.Name, dto.Surname);
            var person = _mapper.Map<Person>(dto);

            try
            {
                var createdPerson = await _unitOfWork.Persons.AddPersonAsync(person);
                await _unitOfWork.CommitAsync();
               
                _logger.LogInformation("Created person with Id: {PersonId}", createdPerson.Id);
                return _mapper.Map<PersonDto>(createdPerson);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating person: {Name} {Surname}", dto.Name, dto.Surname);
                throw;
            }
        }
        public async Task<IEnumerable<PersonDto>> GetAllPersonsAsync()
        {
            _logger.LogInformation("Retrieving all persons");


            var persons = await _unitOfWork.Persons.GetAllPersonsAsync();

            _logger.LogInformation("Retrieved {Count} persons", persons?.Count() ?? 0);

            return _mapper.Map<IEnumerable<PersonDto>>(persons);
        }


        public async Task<PersonDto?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Retrieving person by Id: {PersonId}", id);


            var person = await _unitOfWork.Persons.GetByIdAsync(id);
            if (person == null)
            {
                _logger.LogWarning("Person with Id: {PersonId} not found", id);
                return null;
            }

            // Use AutoMapper here:
            return _mapper.Map<PersonDto>(person);
        }

        public async Task<PersonDto?> UpdatePersonAsync(int id, PersonDto dto)
        {
            _logger.LogInformation("Updating person with Id: {PersonId}", id);

            var person = await _unitOfWork.Persons.GetByIdAsync(id);
            if (person == null)
            {
                _logger.LogWarning("Person with Id: {PersonId} not found for update", id);
                throw new KeyNotFoundException();
            }

            _mapper.Map(dto, person);
            await _unitOfWork.Persons.UpdateAsync(person);

            _logger.LogInformation("Person with Id: {PersonId} updated successfully", id);

            var updatedDto = _mapper.Map<PersonDto>(person);

            return updatedDto;
        }

       

        public async Task<bool> DeletePersonAsync(int id)
        {
            _logger.LogInformation("Deleting person with Id: {PersonId}", id);

            var result = await _unitOfWork.Persons.DeleteAsync(id);

            if (result)
                _logger.LogInformation("Person with Id: {PersonId} deleted successfully", id);
            else
                _logger.LogWarning("Failed to delete person with Id: {PersonId}", id);

            return result;
        }
    }
}
