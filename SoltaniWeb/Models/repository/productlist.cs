using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoltaniWeb.Models.repository
{
    public class productlist
    {

        public int idproduct { get; set; }
        public int categoryid { get; set; }
        public string name { get; set; }
        public string dimension { get; set; }
        public string codename { get; set; }
        public string imageproducts { get; set; }
        public string description { get; set; }
        public string grade { get; set; }
        public string keywords { get; set; }
        public Nullable<int> inventory { get; set; }
        public Nullable<decimal> lastbuycost { get; set; }
        public Nullable<decimal> lastcellcost { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public Nullable<int> users { get; set; }
        public Nullable<int> ftpid { get; set; }
        public Nullable<bool> status { get; set; }
        public System.DateTime updatesellcost { get; set; }
        public System.DateTime updatebuycost { get; set; }
    }
}