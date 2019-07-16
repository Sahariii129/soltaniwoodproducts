using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_section
    {
        public  tbl_section()
        {
            tbl_category = new HashSet<tbl_category>();
            tbl_CompanySection = new HashSet<tbl_CompanySection>();
        }

        public int id { get; set; }
        public string name { get; set; }
        public string keywords { get; set; }
        public string image { get; set; }
        public string shortname { get; set; }
        public int? ftpid { get; set; }
        public bool? status { get; set; }
        public int? ordering { get; set; }
        public string name_EN { get; set; }

        public virtual tbl_ftp ftp { get; set; }
        public virtual ICollection<tbl_category> tbl_category { get; set; }
        public virtual ICollection<tbl_CompanySection> tbl_CompanySection { get; set; }
    }
}
