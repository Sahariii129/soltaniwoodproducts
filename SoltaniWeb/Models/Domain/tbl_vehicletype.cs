using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_vehicletype
    {
        public  tbl_vehicletype()
        {
            tbl_transportationcost = new HashSet<tbl_transportationcost>();
        }

        public int id { get; set; }
        public string vehiclename { get; set; }
        public decimal? capacity { get; set; }
        public decimal? factor { get; set; }
        public decimal? mincost { get; set; }

        public virtual ICollection<tbl_transportationcost> tbl_transportationcost { get; set; }
    }
}
