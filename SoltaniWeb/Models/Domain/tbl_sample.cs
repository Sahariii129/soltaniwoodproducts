using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_sample
    {
        public int idsample { get; set; }
        public string namesample { get; set; }
        public string codename { get; set; }
        public string sampleimg { get; set; }
        public int idcategory { get; set; }
        public string sampledisc { get; set; }
        public int? imagesize { get; set; }
        public int? like { get; set; }
        public int? dislike { get; set; }

        public virtual tbl_category idcategoryNavigation { get; set; }
        public virtual tbl_sample idsampleNavigation { get; set; }
        public virtual tbl_sample InverseidsampleNavigation { get; set; }
    }
}
