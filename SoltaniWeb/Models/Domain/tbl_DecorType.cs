using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_DecorType
    {
        public tbl_DecorType()
        {
            tbl_files = new HashSet<tbl_files>();
        }

        public int id { get; set; }
        public string name { get; set; }
        public string keywords { get; set; }

        public virtual ICollection<tbl_files> tbl_files { get; set; }
    }
}
