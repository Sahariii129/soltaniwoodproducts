using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_orderdetails
    {
        public int id { get; set; }
        public int order_id { get; set; }
        public int product_id { get; set; }
        public int number { get; set; }
        public string description { get; set; }

        public virtual tbl_order order_ { get; set; }
        public virtual tbl_products product_ { get; set; }
    }
}
