using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoltaniWeb.Models.Domain;
using System.Globalization;
using SoltaniWeb.Models.Extensions;


namespace SoltaniWeb.Models.repository
{
    public class entityproducutsreposit
    {

        public IQueryable getentity()
        {
            _4820_soltaniwebContext db = new _4820_soltaniwebContext();
            //var q = db.tbl_news.Where(a => a.confirmation == true).OrderByDescending(a => a.id).Skip(j).Take(i);

            var q = (from b in db.tbl_listkala97
                     group b by b.productid into g
                     select new
                                {
                                    productid = g.Key,

                                    Totalentity = g.Sum(item => item.kalanumberm),

                                });




            return q;


        }

        public int getentityitem(int id = 0)
        {
            _4820_soltaniwebContext db = new _4820_soltaniwebContext();
            var q = from b in db.tbl_listkala97

                    group b by b.productid into g
                    select new
                    {
                        productid = g.Key,

                        Totalentity = g.Sum(item => item.kalanumberm),

                    };

            var a = q.Where(k => k.productid == id).SingleOrDefault();
            if (a != null)
            {
                int h = (int)a.Totalentity;
                return h;
            }
            else
            {
                int h = 0;
                return h;
            }



        }


        public string getexistproducts(int id = 0)
        {
            _4820_soltaniwebContext db = new _4820_soltaniwebContext();
            var q = from b in db.tbl_listkala97

                    group b by b.productid into g
                    select new
                    {
                        productid = g.Key,

                        Totalentity = g.Sum(item => item.kalanumberm),

                    };
            var kalaq = db.tbl_products.Where(l => l.idproduct == id).SingleOrDefault();

            var a = q.Where(k => k.productid == id).SingleOrDefault();
            if (a != null)
            {
                int h = (int)a.Totalentity;
                if (h >= kalaq.inventory)
                {
                    return "موجود";
                }
                else if (h<kalaq.inventory && h>0)
                {
                    return "محدود";
                }
                else if(h==0)
                {
                    return "به زودی ";
                }
                else if(h<0)
                {
                    return "موجودی منفی";
                }
                else
                {
                    return "نا مشخص";
                }

            }
            else
            {

                return "موجود نیست";
            }



        }


        public decimal getlastbuyprice(int id = 0)
        {
            _4820_soltaniwebContext db = new _4820_soltaniwebContext();
            decimal lastbuyprice = db.tbl_listkala97.Where(a => a.productid == id && a.htype == 1).Count() == 0 ? 0 : db.tbl_listkala.Where(a => a.productid == id && a.htype == 1).OrderByDescending(a => a.sodoordate).FirstOrDefault().kalacost;


            return lastbuyprice;
        }


        public decimal getlastsellprice(int id = 0)
        {
            _4820_soltaniwebContext db = new _4820_soltaniwebContext();
            decimal lastsellprice = db.tbl_listkala97.Where(a => a.productid == id && a.htype == -1).Count() == 0 ? 0 : db.tbl_listkala.Where(a => a.productid == id && a.htype == -1).OrderByDescending(a => a.sodoordate).FirstOrDefault().kalacost;


            return lastsellprice;
        }


        public string getlastselldate(int id = 0)
        {
            _4820_soltaniwebContext db = new _4820_soltaniwebContext();
            var lastselldate = db.tbl_listkala97.Where(a => a.productid == id && a.htype == -1).Count() == 0 ? "" : db.tbl_listkala.Where(a => a.productid == id && a.htype == -1).OrderByDescending(a => a.sodoordate).FirstOrDefault().sodoordate.ToPersianDate();


            return lastselldate;
        }
        public string getlastselldesc(int id = 0)
        {
            _4820_soltaniwebContext db = new _4820_soltaniwebContext();
            var lastselldesc = db.tbl_listkala97.Where(a => a.productid == id && a.htype == -1).Count() == 0 ? "" : db.tbl_listkala.Where(a => a.productid == id && a.htype == -1).OrderByDescending(a => a.sodoordate).FirstOrDefault().kaladescription;


            return lastselldesc;
        }
        public string getlastbuydate(int id = 0)
        {
            _4820_soltaniwebContext db = new _4820_soltaniwebContext();
            var lastbuydate = db.tbl_listkala97.Where(a => a.productid == id && a.htype == 1).Count() == 0 ? "" : db.tbl_listkala.Where(a => a.productid == id && a.htype == 1).OrderByDescending(a => a.sodoordate).FirstOrDefault().sodoordate.ToPersianDate();


            return lastbuydate;
        }

        public int numbersell(int id = 0)
        {
            _4820_soltaniwebContext db = new _4820_soltaniwebContext();
            var number = db.tbl_listkala97.Where(a => a.productid == id && a.htype == -1).Count();


            return number;

        }

        public int numberbuy(int id = 0)
        {
            _4820_soltaniwebContext db = new _4820_soltaniwebContext();
            var number = db.tbl_listkala97.Where(a => a.productid == id && a.htype == 1).Count();


            return number;

        }

        public virtual tbl_listkala getlastbuyrecord(int id = 0)
        {
            _4820_soltaniwebContext db = new _4820_soltaniwebContext();
            var lastrecord = db.tbl_listkala97.Where(a => a.productid == id && a.htype == 1).Count() == 0 ? null : db.tbl_listkala.Where(a => a.productid == id && a.htype == 1).OrderByDescending(a => a.sodoordate).FirstOrDefault();


            return lastrecord;
        }


        public IQueryable<tbl_listkala97> getlastsellrecord(int id = 0)
        {
            _4820_soltaniwebContext db = new _4820_soltaniwebContext();
            var lastrecord =  db.tbl_listkala97.Where(a => a.productid == id && a.htype == -1).OrderByDescending(a => a.sodoordate);


            return lastrecord;
        }

    }
}

