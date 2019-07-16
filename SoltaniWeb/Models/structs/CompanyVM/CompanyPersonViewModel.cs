using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoltaniWeb.Models.structs.PersonVM;

namespace SoltaniWeb.Models.structs.CompanyVM
{
    public class CompanyPersonViewModel
    {
        public CompanyViewModel Company { get; set; }
        public List<PersonViewModel> Person{ get; set; }
    }
}
