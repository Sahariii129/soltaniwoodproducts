using SoltaniWeb.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoltaniWeb.Models.repository
{
    public class newscommentreposit
    {
        public IQueryable<tbl_newscomments> getnewscomments(int id = 0)
        {
            _4820_soltaniwebContext db = new _4820_soltaniwebContext();
            var q = db.tbl_newscomments.Where(a => (a.newsid == id && a.parentid ==0 &&  a.comfirmation==true)).OrderByDescending(a => a.id);
            return q;

        }

        public IQueryable<tbl_newscomments> getreplynewscomments(int id = 0 , int id01 =0)
        {
            _4820_soltaniwebContext db = new _4820_soltaniwebContext();
            var q = db.tbl_newscomments.Where(a => (a.newsid == id && a.parentid==id01 && a.comfirmation == true)).OrderByDescending(a => a.id);
            return q;

        }


    }
}