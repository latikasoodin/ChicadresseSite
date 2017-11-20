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
            CreateMap<User,UserViewModel>();
            CreateMap<User, TaskViewModel>();
        }
    }
}