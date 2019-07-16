using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_actions
    {
        public  tbl_actions()
        {
            tbl_actionaccessibility = new HashSet<tbl_actionaccessibility>();
        }

        public int id { get; set; }
        public string action_name { get; set; }
        public int? controller_id { get; set; }
        public string action_description { get; set; }
        public int? menu_id { get; set; }

        public virtual tbl_controllers controller_ { get; set; }
        public virtual tbl_accesstypes menu_ { get; set; }
        public virtual ICollection<tbl_actionaccessibility> tbl_actionaccessibility { get; set; }
    }
}
