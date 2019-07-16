using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_CompanySection
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public int SectionId { get; set; }

        public string Description { get; set; }

        public virtual tbl_section tbl_section { get; set; }

        public virtual tbl_Company tbl_Company { get; set; }
    }
}
