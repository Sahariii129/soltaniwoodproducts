using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoltaniWeb.Models.repository
{
    public class appcontactlistview
    {
        public int id { get; set; }
        public int applicant_id { get; set; }
        public int contacttype_id { get; set; }
        public string contact_value { get; set; }
        public string actionaccesstype { get; set; }
        public string fullname { get; set; }
    }
}