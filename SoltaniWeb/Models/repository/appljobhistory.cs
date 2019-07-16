using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoltaniWeb.Models.repository
{
    public class appljobhistory
    {
        public int id { get; set; }
        public int applicant_id { get; set; }
        public string CompanyName { get; set; }
        public string post { get; set; }
        public string jobname { get; set; }
        public string Periodhistory { get; set; }
        public Nullable<System.DateTime> startdate { get; set; }
        public Nullable<System.DateTime> enddate { get; set; }
        public string leavingreson { get; set; }
    }
}