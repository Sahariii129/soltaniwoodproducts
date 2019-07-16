using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_branches
    {
        public  tbl_branches()
        {
            tbl_orderfrom_branch = new HashSet<tbl_order>();
            tbl_orderto_branch = new HashSet<tbl_order>();
            tbl_user = new HashSet<tbl_user>();
            Persons = new HashSet<tbl_person>();
        }

        public int id { get; set; }
        public string branch_name { get; set; }
        public string cellphone { get; set; }
        public string tellphone { get; set; }
        public string Address { get; set; }
        public string branch_manager { get; set; }
        public virtual ICollection<tbl_person> Persons { get; set; }
        public virtual ICollection<tbl_order> tbl_orderfrom_branch { get; set; }
        public virtual ICollection<tbl_order> tbl_orderto_branch { get; set; }
        public virtual ICollection<tbl_user> tbl_user { get; set; }
    }
}
