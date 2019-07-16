using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoltaniWeb.Models.Domain;


namespace SoltaniWeb.Models.repository
{
    public class newsrepository
    {
        public IQueryable<tbl_news> getslides(int i=0 , int j=0)
        {
            _4820_soltaniwebContext db = new _4820_soltaniwebContext();
            var q = db.tbl_news.Where(a=> a.confirmation==true).OrderByDescending(a => a.id).Skip(j).Take(i);
            
            return q;


        }

        public IQueryable<tbl_news> getnewsbasedtype(int i = 0, int j = 0, int k=0)
        {
            _4820_soltaniwebContext db = new _4820_soltaniwebContext();
            var q = db.tbl_news.Where(a => (a.confirmation == true && a.newtypeid == k )).OrderByDescending(a => a.id).Skip(j).Take(i);

            return q;


        }


    }
}