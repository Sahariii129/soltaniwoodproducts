using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_order
    {
        public  tbl_order()
        {
            tbl_orderdetails = new HashSet<tbl_orderdetails>();
        }

        public int id { get; set; }
        public DateTime sodoor_date { get; set; }
        public int from_branchid { get; set; }
        public int to_branchid { get; set; }
        public int userid { get; set; }
        public string sharh { get; set; }
        public int orderforid { get; set; }
        public bool done { get; set; }
        public DateTime? done_datetime { get; set; }
        public string done_description { get; set; }
        public int? done_userid { get; set; }
        public bool? valid { get; set; }

        public virtual tbl_user done_user { get; set; }
        public virtual tbl_branches from_branch { get; set; }
        public virtual tbl_person orderfor { get; set; }
        public virtual tbl_branches to_branch { get; set; }
        public virtual tbl_user user { get; set; }
        public virtual ICollection<tbl_orderdetails> tbl_orderdetails { get; set; }
    }
}
