using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoltaniWeb.Models.Domain;


namespace SoltaniWeb.Models.repository
{
    public class filmreposit
    {
        public IQueryable<tbl_products> getproducts (string s=""){

            _4820_soltaniwebContext db = new _4820_soltaniwebContext(); 
            var q = from a in db.tbl_products 
                    where (a.name.Contains(s) || a.codename .Contains (s))
                    select a;

            return q;

        }
    }
}