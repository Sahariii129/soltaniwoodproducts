using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_searchproducts
    {
        public int id { get; set; }
        public string searchtext { get; set; }
        public int section_id { get; set; }
        public int category_id { get; set; }
        public string ip { get; set; }
        public int? user_id { get; set; }
        public DateTime searchdate { get; set; }

        public virtual tbl_user user_ { get; set; }
    }
}
