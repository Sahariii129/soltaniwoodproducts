using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_discount
    {
        public  tbl_discount()
        {
            tbl_purchasekart = new HashSet<tbl_purchasekart>();
        }

        public int id { get; set; }
        public string discountcode { get; set; }
        public decimal? percentage { get; set; }
        public string discountname { get; set; }
        public DateTime? startdate { get; set; }
        public DateTime? enddate { get; set; }
        public bool? status { get; set; }

        public virtual ICollection<tbl_purchasekart> tbl_purchasekart { get; set; }
    }
}
