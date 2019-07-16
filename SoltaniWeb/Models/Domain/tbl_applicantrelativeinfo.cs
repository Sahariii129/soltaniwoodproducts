using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_applicantrelativeinfo
    {
        public int id { get; set; }
        public int? applicant_id { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string Relative { get; set; }
        public string job { get; set; }
        public string address { get; set; }
        public string tell { get; set; }

        public virtual tbl_applicants applicant_ { get; set; }
    }
}
