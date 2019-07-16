using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_actionaccesstype
    {
        public  tbl_actionaccesstype()
        {
            tbl_accesstypes = new HashSet<tbl_accesstypes>();
        }

        public int id { get; set; }
        public string actionaccesstype { get; set; }
        public string description { get; set; }

        public virtual ICollection<tbl_accesstypes> tbl_accesstypes { get; set; }
    }
}
