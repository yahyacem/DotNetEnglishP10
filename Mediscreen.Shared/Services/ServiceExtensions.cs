using Mediscreen.Shared.Settings;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Configuration;

namespace Mediscreen.Shared.Services
{
    public static class ServiceExtensions
    {
        public static void ConfigureMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(configuration.GetSection("MediscreenDatabase"));
        }
        public static void ConfigureMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
        public static void ConfigureHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient();
        }
        public static void ConfigureRedis(this IServiceCollection services)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "redis:6379"; // redis is the container name of the redis service. 6379 is the default port
                options.InstanceName = "SampleInstance";
            });
        }
        public static void ConfigureAzureADApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(configuration, "AzureAd");
        }
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Audience = configuration["AzureAd:ClientId"];
                options.Authority = $"{configuration["AzureAd:Instance"]}{configuration["AzureAd:TenantId"]}";
            });
        }
    }
}