using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_newstype
    {
        public  tbl_newstype()
        {
            tbl_news = new HashSet<tbl_news>();
        }

        public int id { get; set; }
        public string newstype { get; set; }
        public string description { get; set; }

        public virtual ICollection<tbl_news> tbl_news { get; set; }
    }
}
