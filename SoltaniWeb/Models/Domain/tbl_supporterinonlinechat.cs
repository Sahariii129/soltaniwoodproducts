using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_supporterinonlinechat
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string supporte_name { get; set; }

        public virtual tbl_user user_ { get; set; }
    }
}
