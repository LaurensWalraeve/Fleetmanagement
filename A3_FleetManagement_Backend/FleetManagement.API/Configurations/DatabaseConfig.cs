using FleetManagement.Data.Contexts.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using FleetManagement.Common.Models;
using FleetManagement.Common.Models.Update;
using FleetManagement.Data.Entities;
using FleetManagement.Data.Profiles;

namespace FleetManagement.API.Configurations
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<FleetDbContext>(
                options => options
                    .UseMySql(configuration.GetConnectionString("FleetManagementDb"), ServerVersion.Parse("8.0.29-mysql"))
                    .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                    .EnableSensitiveDataLogging(),
                ServiceLifetime.Transient);

            var mappingConfig = new MapperConfiguration(mc =>
            {
                //mc.CreateMap<Driver, DriverValue>();
                mc.CreateMap<DriverValue, Driver>()
                    .ReverseMap()
                    .ForMember(dest => dest.Licenses, opt => opt.Ignore())
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address)); // Voeg deze regel toe
                //mc.CreateMap<DriverUpdateModel, Driver>().ReverseMap();
                mc.CreateMap<DriverUpdateModel, DriverValue>().ReverseMap();
                mc.CreateMap<DriverLicenseValue, DriverLicense>().ReverseMap();
                mc.CreateMap<Address, AddressValue>();
                mc.CreateMap<AddressValue, Address>().ReverseMap();
                mc.CreateMap<AddressUpdateModel, Address>().ReverseMap();
                mc.CreateMap<AddressUpdateModel, AddressValue>().ReverseMap();
                mc.CreateMap<VehicleUpdateModel, Vehicle>().ReverseMap();
                mc.CreateMap<VehicleUpdateModel, VehicleValue>().ReverseMap();
                mc.CreateMap<VehicleValue, Vehicle>()
                    .ReverseMap();

                mc.CreateMap<LicenseTypeValue, LicenseType>().ReverseMap();
                mc.CreateMap<FuelTypeValue, FuelType>().ReverseMap();
                mc.CreateMap<DriverFuelCardValue, DriverFuelCard>().ReverseMap();
                mc.CreateMap<DriverVehicleValue, DriverVehicle>().ReverseMap();
                mc.CreateMap<FuelCardValue, FuelCard>().ReverseMap();
                mc.CreateMap<FuelCardUpdateModel, FuelCard>().ReverseMap();
                mc.CreateMap<FuelCardUpdateModel, FuelCardValue>()
                    .ForMember(dest => dest.AcceptedFuels, opt => opt.Ignore());
                mc.CreateMap<FuelCardValue, FuelCardUpdateModel>()
                    .ForMember(dest => dest.AcceptedFuels, opt => opt.Ignore());
                mc.CreateMap<VehicleTypeValue, VehicleType>().ReverseMap();
                mc.CreateMap<FuelCardAcceptedFuelsValue, FuelCardAcceptedFuels>().ReverseMap();
                mc.CreateMap<UserValue, User>().ReverseMap();
                mc.CreateMap<RoleValue, Role>().ReverseMap();
                mc.AddProfile(new DriverProfile()); // Add profiles for other models if needed

            });
            
            services.AddSingleton(mappingConfig.CreateMapper());
        }
    }
}
