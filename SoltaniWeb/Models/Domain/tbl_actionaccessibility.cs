using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_actionaccessibility
    {
        public int id { get; set; }
        public int? acction_id { get; set; }
        public int? userid { get; set; }
        public bool permission { get; set; }

        public virtual tbl_actions acction_ { get; set; }
        public virtual tbl_user user { get; set; }
    }
}
