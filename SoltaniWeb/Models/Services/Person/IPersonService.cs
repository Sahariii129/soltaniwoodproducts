using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using SoltaniWeb.Models.Domain;
using SoltaniWeb.Models.Extensions;
using SoltaniWeb.Models.structs;
using SoltaniWeb.Models.structs.PersonVM;

namespace SoltaniWeb.Models.Services.Person
{
    public interface IPersonService
    {
        IEnumerable<tbl_person> Read();
        PersonCreateViewModel FindPerson(int personId);
        ResultStatus FindPerson(string fName,string lName,string mobile, string codeMeli);
        IQueryable<structs.PersonVM.PersonViewModel> GetAll();
        void Create(PersonCreateViewModel person);
        void Update(PersonCreateViewModel person);
        List<PersonAddressViewModel> FindPersonAddresses(int personId);
        int AddPersonAddresses(PersonAddressViewModel personAddress);
        void RemovePersonAddresses(PersonAddressViewModel personAddress);
        int AddPersonInformationSetting(PersonInformationSettingViewModel personInformation);
        void RemovePersonInformationSetting(PersonInformationSettingViewModel personInformation);
        List<PersonInformationSettingViewModel> FindPersonInformation(int personId);
        void UpdateAddress(PersonAddressViewModel personAddress);
        void UpdateInformationSetting(PersonInformationSettingViewModel personInformation);
        void Destroy(structs.PersonVM.PersonViewModel person);
    }
}
