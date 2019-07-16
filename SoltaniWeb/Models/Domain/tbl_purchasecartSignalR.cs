using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_purchasecartSignalR
    {
        public int id { get; set; }
        public int userid { get; set; }
        public string connectionid_cart { get; set; }
        public string purchasecartDesc { get; set; }

        public virtual tbl_user idNavigation { get; set; }
    }
}
