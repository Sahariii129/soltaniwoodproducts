using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_purchasekartitemlist
    {
        public int id { get; set; }
        public int perchasekart_id { get; set; }
        public int product_id { get; set; }
        public int number { get; set; }
        public decimal price { get; set; }
        public decimal totalprice { get; set; }
        public DateTime purchase_datetime { get; set; }

        public virtual tbl_purchasekart perchasekart_ { get; set; }
        public virtual tbl_products product_ { get; set; }
    }
}
