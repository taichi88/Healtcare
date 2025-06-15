using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthcareApi.Application.DTO;

namespace HealthcareApi.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(LoginDto loginDto);
    }
}