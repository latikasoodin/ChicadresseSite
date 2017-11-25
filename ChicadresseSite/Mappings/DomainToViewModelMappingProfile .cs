using AutoMapper;
using Chicadresse.Entities.Domain;
using Chicadresse.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChicadresseSite.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public DomainToViewModelMappingProfile()
        {
            CreateMap<User, UserViewModel>();

            CreateMap<Guest_Companinons, CompanionViewModel>();

            CreateMap<Guest_Details, GuestViewModel>()
                .ForMember(d => d.TableId, opt => opt.MapFrom(s => s.Guest_Table.FirstOrDefault().TableId))
                .ForMember(d => d.Companions, opt => opt.MapFrom(s => s.Guest_Companinons));
        }
    }
}