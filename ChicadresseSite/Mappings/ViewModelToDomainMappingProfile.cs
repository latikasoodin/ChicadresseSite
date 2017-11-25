using AutoMapper;
using Chicadresse.Entities.Domain;
using Chicadresse.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChicadresseSite.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<UserViewModel, User>();

            CreateMap<CompanionViewModel, Guest_Companinons>()
                .ForMember(d => d.CreatedDate, opt => opt.MapFrom(s => DateTime.Now))
                .ForMember(d => d.ModifiedDate, opt => opt.MapFrom(s => DateTime.Now));

            CreateMap<GuestViewModel, Guest_Details>()
                 .ForMember(d => d.CreatedDate, opt => opt.MapFrom(s => DateTime.Now))
                .ForMember(d => d.ModifiedDate, opt => opt.MapFrom(s => DateTime.Now))
                .ForMember(d => d.Guest_Companinons, opt => opt.MapFrom(s => s.Companions));
        }
    }
}