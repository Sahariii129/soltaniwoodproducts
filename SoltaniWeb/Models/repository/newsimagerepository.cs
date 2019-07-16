using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoltaniWeb.Models.Domain;

namespace SoltaniWeb.Models.repository
{
    public class newsimagerepository
    {
        public IQueryable<tbl_newsimagefiles> getnewsimagefile()
        {
            _4820_soltaniwebContext db = new _4820_soltaniwebContext();
            var q = db.tbl_newsimagefiles.OrderByDescending(a => a.id);
            return q;



        }
    }
}