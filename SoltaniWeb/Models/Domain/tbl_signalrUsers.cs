using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_signalrUsers
    {
        public int id { get; set; }
        public string username { get; set; }
        public string fullname { get; set; }
        public string connectionId { get; set; }
        public int? userid { get; set; }

        public virtual tbl_user user { get; set; }
    }
}
