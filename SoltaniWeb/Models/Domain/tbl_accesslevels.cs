using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_accesslevels
    {
        public int id { get; set; }
        public int userid { get; set; }
        public int accessid { get; set; }
        public bool accessvalue { get; set; }

        public virtual tbl_accesstypes access { get; set; }
        public virtual tbl_user user { get; set; }
    }
}
