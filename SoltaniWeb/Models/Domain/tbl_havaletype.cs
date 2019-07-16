using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_havaletype
    {
        public tbl_havaletype()
        {
            tbl_listkala = new HashSet<tbl_listkala>();
            tbl_listkala97 = new HashSet<tbl_listkala97>();
        }

        public int htype { get; set; }
        public string name { get; set; }

        public virtual tbl_havaletype htypeNavigation { get; set; }
        public virtual tbl_havaletype InversehtypeNavigation { get; set; }
       public virtual ICollection<tbl_listkala> tbl_listkala { get; set; }
       public virtual ICollection<tbl_listkala97> tbl_listkala97 { get; set; }
    }
}
