using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthcareApi.Application.DTO;
using HealthcareApi.Domain.Models;



namespace HealthcareApi.application.Interfaces
{
    public interface IPersonService
    {
        Task<PersonDto> CreatePersonAsync(PersonDto dto);
        Task<IEnumerable<PersonDto>> GetAllPersonsAsync();
        Task<PersonDto?> GetByIdAsync(int id);
        Task<PersonDto?> UpdatePersonAsync(int id, PersonDto dto);
        Task<bool> DeletePersonAsync(int id);
       
    }
}
