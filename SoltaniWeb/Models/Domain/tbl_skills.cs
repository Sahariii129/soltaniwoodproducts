using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_skills
    {
        public  tbl_skills()
        {
            tbl_skillappinfo = new HashSet<tbl_skillappinfo>();
        }

        public int id { get; set; }
        public string skillname { get; set; }
        public string description { get; set; }

        public virtual ICollection<tbl_skillappinfo> tbl_skillappinfo { get; set; }
    }
}
