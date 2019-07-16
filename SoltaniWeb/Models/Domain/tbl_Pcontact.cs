using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_Pcontact
    {
        public int id { get; set; }
        public int idname { get; set; }
        public string tel { get; set; }
        public string cell { get; set; }
        public string address { get; set; }

        public virtual tbl_person idnameNavigation { get; set; }
    }
}
