using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_job
    {
        public  tbl_job()
        {
            tbl_applicants = new HashSet<tbl_applicants>();
        }

        public int jobid { get; set; }
        public string jobtitle { get; set; }
        public string jobdesc { get; set; }

        public virtual ICollection<tbl_applicants> tbl_applicants { get; set; }
    }
}
