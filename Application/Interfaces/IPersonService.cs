using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Dto;
using TaskProject.Models;

namespace Application.Interfaces
{
    public interface IPersonService
    {
        Task<PersonDto> CreatePersonAsync(PersonDto dto);
        Task<PersonDto> UpdatePersonAsync(int id, PersonDto dto);
        Task<PersonDto?> GetByIdAsync(int id);


    }
}
