using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_category
    {
        public  tbl_category()
        {
            tbl_products = new HashSet<tbl_products>();
            tbl_sample = new HashSet<tbl_sample>();
        }

        public int id { get; set; }
        public string categoryname { get; set; }
        public string description { get; set; }
        public string imagecategory { get; set; }
        public string shortname { get; set; }
        public string keywords { get; set; }
        public int? section_id { get; set; }
        public string actionname { get; set; }
        public bool? status { get; set; }
        public bool? showprice { get; set; }
        public string fulldescription { get; set; }
        public bool? purchaseonline { get; set; }
        public int? weight { get; set; }
        public byte[] image { get; set; }

        public virtual tbl_section section_ { get; set; }
        public virtual ICollection<tbl_products> tbl_products { get; set; }
        public virtual ICollection<tbl_sample> tbl_sample { get; set; }
    }
}
