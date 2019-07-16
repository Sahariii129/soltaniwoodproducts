using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_samples
    {
        public int Id { get; set; }
        public int filesid { get; set; }
        public int productsid { get; set; }
        public int userid { get; set; }
        public DateTime date { get; set; }
        public string description { get; set; }

        public virtual tbl_files files { get; set; }
        public virtual tbl_products products { get; set; }
        public virtual tbl_user user { get; set; }
    }
}
