using FleetManagement.Common.Services;
using FleetManagement.Common.Services.Interfaces;
using FleetManagement.Common.Models;
using FleetManagement.Data.Entities;
using FleetManagement.Data.Repositories;
using FleetManagement.Common.Repositories.Interfaces;

namespace FleetManagement.API.Configurations;

public static class DependencyInjectionConfig
{
    public static void AddDependencyInjectionConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        // General
        services.AddHttpClient();
        services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalhost3000",
                builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        //Services
        services.AddScoped<IDriverService, DriverService>();
        services.AddScoped<IVehicleService, VehicleService>();
        services.AddScoped<IFuelCardService, FuelCardService>();
        services.AddScoped<IUserService, UserService>();

        // Register the repositories
        services.AddScoped<IDriverRepository, DriverRepository>();
        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<IFuelCardRepository, FuelCardRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        // Add AutoMapper
        services.AddAutoMapper(typeof(Program));


        services.AddSingleton(configuration);
    }
}