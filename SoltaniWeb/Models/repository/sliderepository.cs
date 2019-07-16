using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoltaniWeb.Models.Domain;


namespace SoltaniWeb.Models.repository
{
    public class sliderepository

    {
        private readonly _4820_soltaniwebContext db = new _4820_soltaniwebContext();
        

        public sliderepository( _4820_soltaniwebContext db)
        {
         
            this.db = db;
        }
        public IQueryable<tbl_slides> getslides()

        {
           
            var q = db.tbl_slides.Where(a => a.show == true).OrderByDescending(a=>a.rank);

            return q;


        }

        public virtual tbl_slides firstslide()
        {
           
            var q = db.tbl_slides.Where(a => a.first == true).FirstOrDefault();

            return q;
        }




        public IQueryable<tbl_slides> getslidesn()
        {
           
            var q = db.tbl_slides;

            return q;


        }

    }
}