using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_transportationcost
    {
        public int id { get; set; }
        public int cart_id { get; set; }
        public int vehicle_id { get; set; }
        public int number { get; set; }
        public decimal? tcost { get; set; }
        public decimal? totaltcost { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string address { get; set; }
        public decimal? distance { get; set; }
        public decimal? totalweight { get; set; }

        public virtual tbl_purchasekart cart_ { get; set; }
        public virtual tbl_vehicletype vehicle_ { get; set; }
    }
}
