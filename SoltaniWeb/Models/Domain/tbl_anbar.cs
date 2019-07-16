using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_anbar
    {
        public tbl_anbar()
        {
            tbl_listkala = new HashSet<tbl_listkala>();
            tbl_listkala97 = new HashSet<tbl_listkala97>();
        }

        public int id { get; set; }
        public string anbarname { get; set; }
        public string anbardesc { get; set; }
        public string address { get; set; }

        public virtual ICollection<tbl_listkala> tbl_listkala { get; set; }
        public virtual ICollection<tbl_listkala97> tbl_listkala97 { get; set; }
    }
}
