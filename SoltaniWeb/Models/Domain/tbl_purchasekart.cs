using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_purchasekart
    {
        public  tbl_purchasekart()
        {
            tbl_purchasekartitemlist = new HashSet<tbl_purchasekartitemlist>();
            tbl_transaction = new HashSet<tbl_transaction>();
            tbl_transportaiondetails = new HashSet<tbl_transportaiondetails>();
            tbl_transportationcost = new HashSet<tbl_transportationcost>();
            tbl_transportationdeliverinfo = new HashSet<tbl_transportationdeliverinfo>();
        }

        public int id { get; set; }
        public int userid { get; set; }
        public DateTime purchasedatestart { get; set; }
        public bool ispaid { get; set; }
        public DateTime? purchasedateend { get; set; }
        public string addresstodeliver { get; set; }
        public string tell { get; set; }
        public int? province_id { get; set; }
        public int? city_id { get; set; }
        public bool? isdeleverd { get; set; }
        public int? discount_id { get; set; }
        public string transId { get; set; }
        public int? status { get; set; }
        public string pcartDesc { get; set; }
        public DateTime? GetPricedate { get; set; }
        public int? personelid { get; set; }
        public int? discountamount { get; set; }
        public int? transportationcost { get; set; }


        public virtual tbl_discount discount_ { get; set; }
        public virtual tbl_user user { get; set; }
        public virtual tbl_user personel { get; set; }
        public virtual ICollection<tbl_purchasekartitemlist> tbl_purchasekartitemlist { get; set; }
        public virtual ICollection<tbl_transaction> tbl_transaction { get; set; }
        public virtual ICollection<tbl_transportaiondetails> tbl_transportaiondetails { get; set; }
        public virtual ICollection<tbl_transportationcost> tbl_transportationcost { get; set; }
        public virtual ICollection<tbl_transportationdeliverinfo> tbl_transportationdeliverinfo { get; set; }
    }
}
