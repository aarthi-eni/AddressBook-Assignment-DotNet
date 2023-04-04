using Entities.Dtos;
using Entities.Model;
using AutoMapper;
using System;

namespace Entities. MappingProfile
{
    public class Mapper : Profile
    {
        public Mapper()
        {
         CreateMap<Address, AddressDto>().ReverseMap();
         CreateMap<EmailAddress, EmailDto>().ReverseMap();
         CreateMap<PhoneNumber,PhoneDto>().ReverseMap();
         CreateMap<Asset, AssetDto>().ReverseMap();
        }
    }

}