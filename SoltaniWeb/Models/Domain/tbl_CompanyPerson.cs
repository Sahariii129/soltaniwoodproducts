using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_CompanyPerson
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int CompanyId { get; set; }

        [StringLength(1000)]
        public string Title { get; set; }

        public virtual tbl_Company tbl_Company { get; set; }

        public virtual tbl_person tbl_person { get; set; }
    }
}
