using Mediscreen.PatientAPI.Repositories;
using Mediscreen.PatientAPI.Services;
using Mediscreen.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.ConfigureMongoDb(configuration);

// Add environment variables.
builder.Configuration.AddEnvironmentVariables();

// Dependency Injection
builder.Services.AddSingleton<IPatientsRepository, PatientsRepository>();
builder.Services.AddSingleton<IPatientsService, PatientsService>();


// Adds Microsoft Identity platform (AAD v2.0) support to protect this Api
builder.Services.AddMicrosoftIdentityWebApiAuthentication(configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
