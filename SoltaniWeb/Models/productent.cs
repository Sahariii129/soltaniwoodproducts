using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoltaniWeb.Models
{
    public class productent
    {
        public int id { set; get; }

        public string category { set; get; }

          public string anbar { set; get; }
          public string name { set; get; }
          public string codename { set; get; }

         public Nullable<decimal> sellcost { set; get; }
         public int Totalentity { set; get; }
          public Nullable<int> inventory { set; get; }
         public decimal lastbuyprice { set; get; }

         public decimal kalavalues { get; set; }
         public string lastdate { set; get; }
         public decimal lastsellprice { set; get; }
         public string lastselldate { set; get; }

    }
    
}


