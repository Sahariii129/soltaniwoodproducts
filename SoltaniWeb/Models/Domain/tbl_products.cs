using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_products
    {
        public tbl_products()
        {
            tbl_listkala = new HashSet<tbl_listkala>();
            tbl_listkala97 = new HashSet<tbl_listkala97>();
            tbl_orderdetails = new HashSet<tbl_orderdetails>();
            tbl_purchasekartitemlist = new HashSet<tbl_purchasekartitemlist>();
            tbl_samples = new HashSet<tbl_samples>();
        }

        public int idproduct { get; set; }
        public int categoryid { get; set; }
        public string name { get; set; }
        public string dimension { get; set; }
        public string codename { get; set; }
        public string imageproducts { get; set; }
        public string description { get; set; }
        public string grade { get; set; }
        public string keywords { get; set; }
        public int? inventory { get; set; }
        public decimal? lastbuycost { get; set; }
        public decimal? lastcellcost { get; set; }
        public DateTime? date { get; set; }
        public int? users { get; set; }
        public int? ftpid { get; set; }
        public bool? status { get; set; }
        public DateTime updatesellcost { get; set; }
        public DateTime updatebuycost { get; set; }
        public decimal? weight { get; set; }
        public byte[] image { get; set; }
        public byte[] thumbnail_image { get; set; }

        public virtual tbl_category category { get; set; }
        public virtual tbl_ftp ftp { get; set; }
       public virtual ICollection<tbl_listkala> tbl_listkala { get; set; }
       public virtual ICollection<tbl_listkala97> tbl_listkala97 { get; set; }
       public virtual ICollection<tbl_orderdetails> tbl_orderdetails { get; set; }
       public virtual ICollection<tbl_purchasekartitemlist> tbl_purchasekartitemlist { get; set; }
       public virtual ICollection<tbl_samples> tbl_samples { get; set; }
    }
}
