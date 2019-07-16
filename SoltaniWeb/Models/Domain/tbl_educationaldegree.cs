using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_educationaldegree
    {
        public  tbl_educationaldegree()
        {
            tbl_applicants = new HashSet<tbl_applicants>();
            tbl_educationalrecords = new HashSet<tbl_educationalrecords>();
        }

        public int educationid { get; set; }
        public string degree { get; set; }

        public virtual ICollection<tbl_applicants> tbl_applicants { get; set; }
        public virtual ICollection<tbl_educationalrecords> tbl_educationalrecords { get; set; }
    }
}
