using SoltaniWeb.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoltaniWeb.Models.repository
{
    public class newstyperepository
    {

        public IQueryable<tbl_newstype> getnewstype()
        {
            _4820_soltaniwebContext db = new _4820_soltaniwebContext();
            var q = db.tbl_newstype;
            return q;

        }

    }
}