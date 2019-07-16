using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_controllers
    {
        public  tbl_controllers()
        {
            tbl_actions = new HashSet<tbl_actions>();
        }

        public int id { get; set; }
        public string controller_name { get; set; }

        public virtual ICollection<tbl_actions> tbl_actions { get; set; }
    }
}
