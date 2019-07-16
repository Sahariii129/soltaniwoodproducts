using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_transaction
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string transid { get; set; }
        public decimal amount { get; set; }
        public DateTime varizdate { get; set; }
        public int? cartid { get; set; }
        public string sharh { get; set; }

        public virtual tbl_purchasekart cart { get; set; }
        public virtual tbl_user user_ { get; set; }
    }
}
