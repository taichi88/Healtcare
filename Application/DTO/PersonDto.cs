using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HealthcareApi.Application.DTO
{
    public class PersonDto
    {

        public string Name { get; set; }
        public string Surname { get; set; }

        public string? Email { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string? Address { get; set; }


    }
}
