using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SoltaniWeb.Models.Domain;
using SoltaniWeb.Models.Extensions;
using SoltaniWeb.Models.structs.CompanyVM;
using SoltaniWeb.Models.structs.PersonVM;
using SoltaniWeb.Models.Services.Person;

namespace SoltaniWeb.Models.Services.Company
{
    public class CompanyService:ICompanyService
    {
        _4820_soltaniwebContext _context = new _4820_soltaniwebContext();
        private readonly IMapper _mapper;
        public CompanyService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public CompanyViewModel FindCompany(int id)
        {
            var company = _context.tbl_Company.FirstOrDefault(x => x.Id == id);
            var result = _mapper.Map<CompanyViewModel>(company);
            return result;
        }

        public IEnumerable<CompanyViewModel> GetAll()
        {
            var companies = _context.tbl_Company.ProjectTo<CompanyViewModel>(_mapper.ConfigurationProvider);
              //  .Select(x=>new CompanyViewModel
            //{
            //    Address=x.Address,
            //    CompanyRegistrationNumber=x.CompanyRegistrationNumber,
            //    CompanyType=x.Type,
            //    CompanyTypes=new CompanyTypeClass() { Id=(int)x.Type.ToEnum<CompanyType>(),Name=x.Type.ToEnum<CompanyType>().GetDisplayName()},
            //});//.ProjectTo<CompanyViewModel>(_mapper.ConfigurationProvider);
            return companies;
        }

        public IEnumerable<PersonViewModel> GetCompanyPerson(int? companyId)
        {
            if (companyId != null)
            {
                var result = _context.tbl_CompanyPerson.Where(x => x.CompanyId== companyId).Select(x=>new PersonViewModel
                {
                    Address = x.tbl_person.address,
                    Cell = x.tbl_person.cell ?? ((x.tbl_person.PersonInformationSettings == null || x.tbl_person.PersonInformationSettings.Count == 0) ? "" : x.tbl_person.PersonInformationSettings.FirstOrDefault(per => per.PropertyName == PersonInformationSetting.Mobile.ToString()).PropertyValue),
                    CodeMeli = x.tbl_person.codemelli,
                    FullNamePerson = x.tbl_person.Fname + " " + x.tbl_person.Lname,
                    CompanyId = x.CompanyId,
                    Id = x.PersonId,
                    Tell = x.tbl_person.tell,
                    CompanyName = x.tbl_Company.Name,
                });
                return result;
            }
            else
            {
                var result = _context.tbl_CompanyPerson.Select(x => new PersonViewModel
                {
                    Address = x.tbl_person.address,
                    Cell = x.tbl_person.cell ?? ((x.tbl_person.PersonInformationSettings == null || x.tbl_person.PersonInformationSettings.Count == 0) ? "" : x.tbl_person.PersonInformationSettings.FirstOrDefault(per => per.PropertyName == PersonInformationSetting.Mobile.ToString()).PropertyValue),
                    CodeMeli = x.tbl_person.codemelli,
                    FullNamePerson = x.tbl_person.Fname + " " + x.tbl_person.Lname,
                    CompanyId = x.CompanyId,
                    Id = x.PersonId,
                    Tell = x.tbl_person.tell,
                    CompanyName = x.tbl_Company.Name,
                });
                return result;
            }
           

        }

        public IEnumerable<CompanySectionViewModel> GetCompanySections(int? companyId)
        {
            var result = _context.tbl_CompanySection.Select(x => new CompanySectionViewModel
            {
                Id=x.Id,
                CompanyId=x.CompanyId,
                CompanyName=x.tbl_Company.Name,
                SectionKeywords=x.tbl_section.keywords,
                SectionName=x.tbl_section.name,
                SectionShortName=x.tbl_section.shortname,
                SectionStatus=x.tbl_section.status,
                SectionNameEn=x.tbl_section.name_EN,
                CompanySectionDescription=x.Description
               
            });

            return result;

        }

        public int Create(CompanyViewModel model)
        {
            var company = _mapper.Map<tbl_Company>(model);
            _context.tbl_Company.Add(company);
            _context.SaveChanges();
            return company.Id;
        }

        public ResultStatus AddPersonsToCompany(int companyId, int[] personId)
        {
            var op = new ResultStatus();
            try
            {
                personId = personId.Where(x => !_context.tbl_CompanyPerson.Any(y => y.CompanyId == companyId && y.PersonId == x))
                    .ToArray();
                foreach (var per in personId)
                {

                    _context.tbl_CompanyPerson.Add(new tbl_CompanyPerson()
                    {
                        CompanyId = companyId,
                        PersonId = per,
                    });
                }
                _context.SaveChanges();
                op.IsSuccessed = true;
                op.Message = "اطلاعات با موفقیت ثبت گردید";
            }
            catch (Exception e)
            {
                op.IsSuccessed = false;
                op.Message = "خطا در بروز عملیات";
            }

            return op;
        }
        public ResultStatus DeletPersonsFromCompany(int id)
        {
            var op = new ResultStatus();
            try
            {
                var per = _context.tbl_CompanyPerson.FirstOrDefault(x => x.Id==id);
                if (per == null)
                {
                    op.IsSuccessed = false;
                    op.Message = "  مشتری با این مشخصات پیدا نشد";
                    return op;
                }

                _context.tbl_CompanyPerson.Remove(per);
                _context.SaveChanges();
                op.IsSuccessed = true;
                op.Message = "اطلاعات با موفقیت حذف گردید";
            }
            catch (Exception e)
            {
                op.IsSuccessed = false;
                op.Message = "خطا در بروز عملیات";
            }

            return op;
        }

        public ResultStatus AddSectionsToCompany(int companyId, int[] sectionId)
        {
            var op = new ResultStatus();
            try
            {
                sectionId = sectionId.Where(x => !_context.tbl_CompanySection.Any(y => y.CompanyId == companyId && y.SectionId == x))
                    .ToArray();
                foreach (var per in sectionId)
                {

                    _context.tbl_CompanySection.Add(new tbl_CompanySection()
                    {
                        CompanyId = companyId,
                        SectionId = per,
                    });
                }
                _context.SaveChanges();
                op.IsSuccessed = true;
                op.Message = "اطلاعات با موفقیت ثبت گردید";
            }
            catch (Exception e)
            {
                op.IsSuccessed = false;
                op.Message = "خطا در بروز عملیات";
            }

            return op;
        }
        public ResultStatus DeletSectionsFromCompany(int id)
        {
            var op = new ResultStatus();
            try
            {
                var per = _context.tbl_CompanySection.FirstOrDefault(x => x.Id==id);
                if (per == null)
                {
                    op.IsSuccessed = false;
                    op.Message = "  مشتری با این مشخصات پیدا نشد";
                    return op;
                }

                _context.tbl_CompanySection.Remove(per);
                _context.SaveChanges();
                op.IsSuccessed = true;
                op.Message = "اطلاعات با موفقیت حذف گردید";
            }
            catch (Exception e)
            {
                op.IsSuccessed = false;
                op.Message = "خطا در بروز عملیات";
            }

            return op;
        }

        public void Update(CompanyViewModel model)
        {
            var per = _context.tbl_Company.FirstOrDefault(x => x.Id == model.Id);
            if (per != null)
            {
                per.Address = model.Address;
                per.Name = model.Name;
                per.Phone = model.Phone;
                per.Phone2 = model.Phone2;
                per.Phone3 = model.Phone3;
                per.CompanyRegistrationNumber = model.CompanyRegistrationNumber;
                per.Type = ((CompanyType)model.CompanyTypes.Id).ToString();
                _context.SaveChanges();
            }
        }

        public void RemoveCompany(CompanyViewModel model)
        {
            var company = _context.tbl_Company.FirstOrDefault(x => x.Id == model.Id);
            if (company != null)
            {
                _context.tbl_Company.Remove(company);
                _context.SaveChanges();
            }
           ;
        }
    }
}
