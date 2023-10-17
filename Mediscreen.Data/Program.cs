using Mediscreen.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Access to configuration file
ConfigurationManager configuration = builder.Configuration;

// Configure MongoDb
builder.Services.ConfigureMongoDb(configuration);

var app = builder.Build();
app.Run();
