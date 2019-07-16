using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SoltaniWeb.Models.Domain;
using SoltaniWeb.Models.structs.CompanyVM;

namespace SoltaniWeb.Models.Services.Company.MapperProfile
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<tbl_Company, CompanyViewModel>()
                .ForMember(src => src.CompanyType, des => des.MapFrom(x => x.Type));
            CreateMap<CompanyViewModel, tbl_Company>()
                .ForMember(src=>src.Type,des=>des.MapFrom(x=>x.CompanyType));
        }
    }
}
