using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthcareApi.Api.Models;
using HealthcareApi.Domain.IRepositories;




namespace HealthcareApi.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly HealthcareApiContext _context;
        public PersonRepository(HealthcareApiContext context)
        {
            _context = context;
        }
        public async Task<Person> AddPersonAsync(Person person)
        {

            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<Person> GetByIdAsync(int id) =>
            await _context.Persons.FindAsync(id);

        public async Task UpdateAsync(Person person)
        {
            _context.Persons.Update(person);
            await _context.SaveChangesAsync();   // EF Core opens+commits a transaction here
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null) return false;

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
