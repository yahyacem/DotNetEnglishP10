using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Add ocelot.json file and environment variables
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true).AddEnvironmentVariables();

// Configure JWT bearer tokens
builder.Services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, jwtOptions =>
{
    //jwtOptions.Audience = $"api://{configuration["AzureAd:ClientID"]}";
    jwtOptions.Authority = $"{configuration["AzureAd:Instance"]}{configuration["AzureAd:TenantID"]}";
    jwtOptions.RequireHttpsMetadata = false;
    jwtOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidAudiences = configuration.GetSection("AzureAd:Scopes").Get<string[]>(),
        ValidIssuer = $"{configuration["AzureAd:Instance"]}{configuration["AzureAd:TenantID"]}/v2.0"
    };
});

// Set up AzureAd authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer()
    .AddMicrosoftIdentityWebApp(configuration, "AzureAd")
    .EnableTokenAcquisitionToCallDownstreamApi(configuration.GetSection("AzureAd:Scopes").Get<string[]>())
    .AddInMemoryTokenCaches();

// Add Ocelot
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.UseOcelot();

app.Run();