using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthcareApi.Application.DTO;



namespace HealthcareApi.application.Interfaces
{
    public interface IPersonService
    {
        Task<PersonDto> CreatePersonAsync(PersonDto dto);
        Task<PersonDto> UpdatePersonAsync(int id, PersonDto dto);
        Task<PersonDto?> GetByIdAsync(int id);
        Task<bool> DeletePersonAsync(int id);


    }
}
