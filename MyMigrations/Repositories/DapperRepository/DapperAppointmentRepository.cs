using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dapper;
using HealthcareApi.Domain.Models;
using Microsoft.Data.SqlClient;
using HealthcareApi.Domain.IRepositories.IDapperRepositories;
using Dapper;
using System.Data;

namespace HealthcareApi.Infrastructure.Repositories.DapperRepository
{
    public class DapperAppointmentRepository : IDapperAppointmentRepository
    {
        private readonly DapperDbContext _context;
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction = null!;

        public DapperAppointmentRepository(DapperDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAppointmentAsync(Appointment appointment)
        {
            const string sql = @"
            INSERT INTO Appointments (PatientId, DoctorId, AppointmentDate, Notes)
            VALUES (@PatientId, @DoctorId, @AppointmentDate, @Notes);
            SELECT CAST(SCOPE_IDENTITY() as int);";

            using var connection = _context.CreateConnection();
            var id = await connection.QuerySingleAsync<int>(sql, appointment);
            return id;
        }
    }

}
