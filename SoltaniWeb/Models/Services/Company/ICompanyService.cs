using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoltaniWeb.Models.structs.CompanyVM;
using SoltaniWeb.Models.structs.PersonVM;

namespace SoltaniWeb.Models.Services.Company
{
    public interface ICompanyService
    {
        CompanyViewModel FindCompany(int id);
        IEnumerable<CompanyViewModel> GetAll();
        int Create(CompanyViewModel model);
        void Update(CompanyViewModel model);
        void RemoveCompany(CompanyViewModel model);
    }
}
