using HealthcareApi.application.Interfaces;
using Application.Services;
using HealthcareApi.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using HealthcareApi.Infrastructure.Repositories;
using HealthcareApi.Application.IUnitOfWork;
using HealthcareApi.Infrastructure;
using HealthcareApi.Application.Interfaces;
using HealthcareApi.Infrastructure.Data.Dapper.DapperDbContext;
using HealthcareApi.Domain.IRepositories.IDapperRepositories;
using HealthcareApi.Infrastructure.Repositories.DapperRepository;
using Microsoft.Identity.Client;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;





var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<HealthcareApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));





// Add services to the container.
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IDapperPaymentRepository, DapperPaymentRepository>();
builder.Services.AddScoped<IDapperAppointmentRepository, DapperAppointmentRepository>();
builder.Services.AddScoped<IDapperAccountRepository, DapperAccountRepository>();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<DapperDbContext>();
builder.Services.AddAutoMapper(typeof(HealthcareApi.Application.AutoMapperClass));
builder.Services.AddLogging();
builder.Services.AddScoped<IDbConnection>(_ => new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IAuthService, AuthService>();



var key = builder.Configuration["JwtSettings:Key"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key!))
        };
    });


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = Path.Combine(AppContext.BaseDirectory, "TaskProject.xml");
    options.IncludeXmlComments(xmlFile);

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var dbContext = serviceProvider.GetRequiredService<HealthcareApiContext>();
    if (!await dbContext.Database.CanConnectAsync())
    {
        await dbContext.Database.MigrateAsync();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
