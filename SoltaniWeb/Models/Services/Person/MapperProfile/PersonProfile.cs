using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SoltaniWeb.Models.Domain;
using SoltaniWeb.Models.Extensions;

using SoltaniWeb.Models.structs.PersonVM;

namespace SoltaniWeb.Models.Services.Person.MapperProfile
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<PersonAddressViewModel, tbl_PersonAddress>();
            CreateMap<PersonInformationSettingViewModel, tbl_PersonInformationSetting>();
            CreateMap<tbl_person, PersonViewModel>()
                .ForMember(x => x.FullNamePerson, opt => opt.MapFrom(z => (z.Fname ??"") + " " + (z.Lname??"")))
                .ForMember(x => x.Address, opt => opt.MapFrom(z => z.address??((z.PersonAddresses==null || z.PersonAddresses.Count==0)?"": z.PersonAddresses.FirstOrDefault().Address)))
                .ForMember(x => x.Cell, opt => opt.MapFrom(z => z.cell?? ((z.PersonInformationSettings == null || z.PersonInformationSettings.Count == 0) ? "" : z.PersonInformationSettings.FirstOrDefault(per=> per.PropertyName==PersonInformationSetting.Mobile.ToString()).PropertyValue)))
                .ForMember(x => x.GroupId, opt => opt.MapFrom(z => (z.Groups==null || z.Groups.Count==0)?0: z.Groups.FirstOrDefault().GroupId))
                .ForMember(x => x.GroupName, opt => opt.MapFrom(z => (z.Groups == null || z.Groups.Count == 0) ? "نامشخص" : string.Join(",", z.Groups.Select(x => x.Group.Name).ToArray())))
                .ForMember(x => x.PersonGroups, opt => opt.MapFrom(z => (z.Groups == null || z.Groups.Count == 0) ?new List<string>(): z.Groups.Select(x=>x.Group.Name).ToList()))
                .ForMember(x => x.CompanyName, opt => opt.MapFrom(z => (z.tbl_CompanyPerson == null || z.tbl_CompanyPerson.Count == 0) ? "" : string.Join(",", z.tbl_CompanyPerson.Select(x => x.tbl_Company.Name).ToArray())))
                .ForMember(x => x.Companys, opt => opt.MapFrom(z => (z.tbl_CompanyPerson == null || z.tbl_CompanyPerson.Count == 0) ? new List<string>() : z.tbl_CompanyPerson.Select(x => x.tbl_Company.Name).ToList()))
                .ForMember(x => x.PersonAddresses, opt => opt.Ignore())
                .ForMember(x => x.PersonInformationSetting, opt => opt.Ignore())
                .ForMember(x => x.BrancheName, opt => opt.MapFrom(z => z.Branches.branch_name))
                ;
            CreateMap<tbl_person, PersonCreateViewModel>()
              .ForMember(x => x.PersonInformationSetting, opt => opt.MapFrom(z => z.PersonInformationSettings.Select(x => new PersonInformationSettingViewModel
              {
                 PersonId = x.PersonId,
                  Id= x.Id,
                  PropertyName= x.PropertyName,
                  PropertyValue= x.PropertyValue
              }).ToList()))
            .ForMember(x => x.PersonAddresses, opt => opt.MapFrom(z => z.PersonAddresses.Select(x=>new PersonAddressViewModel
            {
                Id=x.Id,
                Address=x.Address,
                Phone3=x.Phone3,
                PostalCode=x.PostalCode,
                Phone2=x.Phone2,
                Phone=x.Phone,
                PersonId=x.PersonId
                }).ToList()));

        }
    }
}
