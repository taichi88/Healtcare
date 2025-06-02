using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthcareApi.Domain.Models;
using Microsoft.Data.SqlClient;
using HealthcareApi.Domain.IRepositories.IDapperRepositories;
using Dapper;
using System.Data;
using HealthcareApi.Infrastructure.Data.Dapper.DapperDbContext;
using HealthcareApi.Application.DTO;

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
    INSERT INTO Clinical.Appointments (PatientId, DoctorId, AppointmentDateTime, ReasonForVisit)
    VALUES (@PatientId, @DoctorId, @AppointmentDateTime, @ReasonForVisit);
    SELECT CAST(SCOPE_IDENTITY() as int);";

            using var connection = _context.CreateConnection();
            var id = await connection.QuerySingleAsync<int>(sql, appointment);
            return id;
        }


        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            const string sql = "SELECT * FROM Clinical.Appointments";
            using var connection = _context.CreateConnection();
            var appointments = await connection.QueryAsync<Appointment>(sql);
            return appointments;
        }
        public async Task<Appointment?> GetAppointmentByIdAsync(int id)
        {
            const string sql = "SELECT * FROM Clinical.Appointments WHERE AppointmentId = @Id";
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Appointment>(sql, new { Id = id });
        }

        public async Task<bool> UpdateAppointmentAsync(Appointment appointment)
        {
            const string sql = @"
        UPDATE Clinical.Appointments 
        SET PatientId = @PatientId, 
            DoctorId = @DoctorId, 
            AppointmentDateTime = @AppointmentDateTime, 
            ReasonForVisit = @ReasonForVisit
        WHERE Id = @Id";

            using var connection = _context.CreateConnection();
            var rowsAffected = await connection.ExecuteAsync(sql, appointment);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            const string sql = "DELETE FROM Clinical.Appointments WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }

    }

}
