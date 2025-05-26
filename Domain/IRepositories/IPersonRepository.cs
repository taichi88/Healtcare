using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthcareApi.Domain.Models;


namespace HealthcareApi.Domain.IRepositories
{
    public interface IPersonRepository
    {
        Task<Person> AddPersonAsync(Person person);
        Task<Person> GetByIdAsync(int id);
        Task UpdateAsync(Person person);
        Task<bool> DeleteAsync(int id);

    }

}
