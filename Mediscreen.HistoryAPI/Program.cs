using Mediscreen.HistoryAPI.Repositories;
using Mediscreen.HistoryAPI.Services;
using Mediscreen.Shared.Services;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add environment variables.
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
builder.Services.ConfigureMongoDb(configuration);

// Dependency Injection
builder.Services.AddSingleton<IPatientsRepository, PatientsRepository>();
builder.Services.AddSingleton<IPatientsService, PatientsService>();
builder.Services.AddSingleton<IHistoryRepository, HistoryRepository>();
builder.Services.AddSingleton<IHistoryService, HistoryService>();


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

app.UseAuthorization();

app.MapControllers();

app.Run();
