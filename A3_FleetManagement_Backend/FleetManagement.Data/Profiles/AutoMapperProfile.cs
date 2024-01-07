using AutoMapper;
using FleetManagement.Common.Models;
using FleetManagement.Data.Entities;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<DriverValue, Driver>()
    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
    .ForMember(dest => dest.FuelCards, opt => opt.MapFrom(src => src.FuelCards.Select(dfc => dfc.FuelCard)))
    .ForMember(dest => dest.Vehicles, opt => opt.MapFrom(src => src.Vehicles.Select(dv => dv.Vehicle)));

        CreateMap<DriverFuelCardValue, FuelCard>()
            .ForMember(dest => dest.FuelCardId, opt => opt.MapFrom(src => src.FuelCardID));

        // Mogelijk moet je nog andere mappings toevoegen voor specifieke eigenschappen van FuelCard, afhankelijk van je modellogica.

        CreateMap<DriverVehicleValue, Vehicle>()
            .ForMember(dest => dest.VehicleId, opt => opt.MapFrom(src => src.VehicleID));
    // Andere toewijzingen voor eigenschappen van Vehicle

        // Voeg eventueel nog andere mappings toe voor specifieke eigenschappen van Vehicle, afhankelijk van je modellogica.


        CreateMap<Vehicle, VehicleValue>().ReverseMap();
        CreateMap<FuelCard, FuelCardValue>().ReverseMap();
        CreateMap<Address, AddressValue>().ReverseMap();
        CreateMap<DriverLicenseValue, DriverLicense>();
        CreateMap<AddressValue, Address>();
        CreateMap<VehicleValue, Vehicle>();
        CreateMap<LicenseTypeValue, LicenseType>();
        CreateMap<FuelTypeValue, FuelType>();
        CreateMap<DriverFuelCardValue, DriverFuelCard>();
        CreateMap<DriverVehicleValue, DriverVehicle>();
        CreateMap<FuelCardValue, FuelCard>();
        CreateMap<VehicleTypeValue, VehicleType>();
        CreateMap<FuelCardAcceptedFuelsValue, FuelCardAcceptedFuels>();
        CreateMap<UserValue, User>().ReverseMap();
        CreateMap<RoleValue, Role>();
    }
}
