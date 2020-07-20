using AutoMapper;
using JobApplication.Areas.Identity.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Areas.Identity.Data.Mapper
{
    public class Mappings :Profile
    {
        public Mappings()
        {
            CreateMap<AppUser, AppUserDto>().ReverseMap();
        }
    }
}
