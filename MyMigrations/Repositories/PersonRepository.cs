using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthcareApi.Domain.Models;
using HealthcareApi.Domain.IRepositories;
using HealthcareApi.Infrastructure;
using System.Numerics;
using Microsoft.EntityFrameworkCore;




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
           
            return person;
        }
        public async Task<IEnumerable<Person>> GetAllPersonsAsync()

        {
            return await _context.Persons.ToListAsync();
        }

        public async Task<Person?> GetByIdAsync(int id)
        {
            return await _context.Persons.FindAsync(id);

        }

        public async Task<bool>  UpdateAsync(Person person)
        {
            _context.Persons.Update(person);
            var result = await _context.SaveChangesAsync();
            return result > 0;
               // EF Core opens+commits a transaction here
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
