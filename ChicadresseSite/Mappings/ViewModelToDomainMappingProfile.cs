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
        }
    }
}