using SoltaniWeb.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoltaniWeb.Models.repository
{
    public class samplerepository
    {
        public IQueryable<tbl_sample> getsample(int ID = 0)
        {

            _4820_soltaniwebContext db = new _4820_soltaniwebContext();
            var q = from a in db.tbl_sample
                    where a.idcategory==ID

                    select a;
            //var w = q.FirstOrDefault().tbl_category.categoryname;

            return q;
        }

        public IQueryable<tbl_sample> getsamplerandom()
        {
            Random rnd = new Random();

            int b = rnd.Next(1, getsample().Count());

            _4820_soltaniwebContext db = new _4820_soltaniwebContext();



            var q = from a in db.tbl_sample
                    where a.idsample.Equals(b)
                    select a;







            return q;



        }


        public virtual tbl_files getsamplesrandom(int decortype = 0)
        {

            Random rnd = new Random();
            _4820_soltaniwebContext db = new _4820_soltaniwebContext();
            var qcabinet = db.tbl_files.Where(a => a.decortypeid == decortype);
            int countsample = qcabinet.Count();
            int row = rnd.Next(1, countsample);
            if (countsample > 1)
            {
                var q = qcabinet.OrderBy(a => a.id).Skip(row).Take(1).SingleOrDefault();

                return q;
            }
            else
            {
                var q = qcabinet.SingleOrDefault();
                return q;
            }

        }



    }
}