using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_accesstypes
    {
        public  tbl_accesstypes()
        {
            tbl_accesslevels = new HashSet<tbl_accesslevels>();
            tbl_actions = new HashSet<tbl_actions>();
        }

        public int id { get; set; }
        public string accesstype { get; set; }
        public string accesscaption { get; set; }
        public string actionname { get; set; }
        public int? actiontype { get; set; }
        public int? ordernumber { get; set; }
        public bool? status { get; set; }

        public virtual tbl_actionaccesstype actiontypeNavigation { get; set; }
        public virtual ICollection<tbl_accesslevels> tbl_accesslevels { get; set; }
        public virtual ICollection<tbl_actions> tbl_actions { get; set; }
    }
}
