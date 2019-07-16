using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_jobexperiences
    {
        public int id { get; set; }
        public int applicant_id { get; set; }
        public string CompanyName { get; set; }
        public string post { get; set; }
        public string jobname { get; set; }
        public string Periodhistory { get; set; }
        public DateTime? startdate { get; set; }
        public DateTime? enddate { get; set; }
        public string leavingreson { get; set; }

        public virtual tbl_applicants applicant_ { get; set; }
    }
}
