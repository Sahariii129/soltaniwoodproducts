using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoltaniWeb.Models.Extensions;
using SoltaniWeb.Models.structs.CompanyVM;
using SoltaniWeb.Models.structs.PersonVM;

namespace SoltaniWeb.Models.Services.Company
{
    public interface ICompanyService
    {
        CompanyViewModel FindCompany(int id);
        IEnumerable<CompanyViewModel> GetAll();
        IEnumerable<PersonViewModel> GetCompanyPerson(int? companyId=null);
       // IEnumerable<CompanySectionViewModel> GetCompanySections(int? companyId);
        int Create(CompanyViewModel model);
        ResultStatus AddPersonsToCompany(int companyId, int[] personId);
        ResultStatus DeletPersonsFromCompany(int companyId, int personId);
         ResultStatus AddSectionsToCompany(int companyId, int[] sectionId);
        ResultStatus DeletSectionsFromCompany(int companyId, int sectionId);
        void Update(CompanyViewModel model);
        void RemoveCompany(CompanyViewModel model);
    }
}
