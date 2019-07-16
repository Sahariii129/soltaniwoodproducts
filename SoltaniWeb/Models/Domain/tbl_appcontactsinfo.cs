using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_appcontactsinfo
    {
        public int id { get; set; }
        public int applicant_id { get; set; }
        public int contacttype_id { get; set; }
        public string contact_value { get; set; }

        public virtual tbl_applicants applicant_ { get; set; }
        public virtual tbl_contactype contacttype_ { get; set; }
    }
}
