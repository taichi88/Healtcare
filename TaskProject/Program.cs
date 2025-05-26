using HealthcareApi.application.Interfaces;
using Application.Services;
using HealthcareApi.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using HealthcareApi.Infrastructure.Repositories;
using HealthcareApi.Api.Models;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonService, PersonService>();


builder.Services.AddDbContext<HealthcareSystemDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Get the XML documentation file path
    var xmlFile = Path.Combine(AppContext.BaseDirectory, "TaskProject.xml");
    options.IncludeXmlComments(xmlFile); // This tells Swagger to include the XML fileas comments
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var dbContext = serviceProvider.GetRequiredService<HealthcareSystemDbContext>();
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
