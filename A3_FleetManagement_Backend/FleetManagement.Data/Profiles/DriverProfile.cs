using AutoMapper;
using FleetManagement.Common.Models;
using FleetManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagement.Data.Profiles
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<DriverValue, Driver>()
    .ForMember(dest => dest.FuelCards, opt => opt.MapFrom(src => src.FuelCards.Select(fc => fc.FuelCard).ToList()))
    .ForMember(dest => dest.Vehicles, opt => opt.MapFrom(src => src.Vehicles.Select(v => v.Vehicle).ToList()));

        }
    }

}
