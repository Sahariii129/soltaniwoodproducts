using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_contactype
    {
        public  tbl_contactype()
        {
            tbl_appcontactsinfo = new HashSet<tbl_appcontactsinfo>();
        }

        public int id { get; set; }
        public string contacttype { get; set; }
        public string Regexp { get; set; }

        public virtual ICollection<tbl_appcontactsinfo> tbl_appcontactsinfo { get; set; }
    }
}
