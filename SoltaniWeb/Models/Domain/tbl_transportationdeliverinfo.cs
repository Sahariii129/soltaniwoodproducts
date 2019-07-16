using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_transportationdeliverinfo
    {
        public int id { get; set; }
        public int cartid { get; set; }
        public DateTime? deliver_date { get; set; }
        public string driver_name { get; set; }
        public string driver_cellphone { get; set; }
        public byte[] receipt_img { get; set; }
        public string description { get; set; }

        public virtual tbl_purchasekart cart { get; set; }
    }
}
