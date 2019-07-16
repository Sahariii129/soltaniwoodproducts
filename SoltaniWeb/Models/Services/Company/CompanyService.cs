using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Caching.Memory;
using SoltaniWeb.Models.Domain;
using SoltaniWeb.Models.structs.CompanyVM;

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
            return companies;
        }

        public int Create(CompanyViewModel model)
        {
            var company = _mapper.Map<tbl_Company>(model);
            _context.tbl_Company.Add(company);
            _context.SaveChanges();
            return company.Id;
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
                per.Type = model.CompanyType;
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
