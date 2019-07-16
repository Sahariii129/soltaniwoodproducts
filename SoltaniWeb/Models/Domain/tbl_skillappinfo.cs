using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_skillappinfo
    {
        public int id { get; set; }
        public int appid { get; set; }
        public int skillid { get; set; }
        public string skillvalue { get; set; }

        public virtual tbl_applicants app { get; set; }
        public virtual tbl_skills skill { get; set; }
    }
}
