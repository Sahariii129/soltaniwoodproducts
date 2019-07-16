using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_transportaiondetails
    {
        public int id { get; set; }
        public int cartid { get; set; }
        public string location_name { get; set; }
        public string location_address { get; set; }
        public string tell { get; set; }
        public string person_peygiri { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string distance { get; set; }

        public virtual tbl_purchasekart cart { get; set; }
    }
}
