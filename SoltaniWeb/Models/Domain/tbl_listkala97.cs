using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_listkala97
    {
        public int id { get; set; }
        public int productid { get; set; }
        public int kalanumber { get; set; }
        public decimal kalacost { get; set; }
        public string kaladescription { get; set; }
        public decimal totalcost { get; set; }
        public DateTime sodoordate { get; set; }
        public int usernameid { get; set; }
        public int htype { get; set; }
        public int kalanumberm { get; set; }
        public int anbarid { get; set; }
        public string time { get; set; }

        public virtual tbl_anbar anbar { get; set; }
        public virtual tbl_havaletype htypeNavigation { get; set; }
        public virtual tbl_products product { get; set; }
        public virtual tbl_user username { get; set; }
    }
}
