using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoltaniWeb.Models.Domain;
using SoltaniWeb.Models.Services.Interfaces;
using SoltaniWeb.Models.structs;

namespace SoltaniWeb.Models.Services
{
    public class PurchaseCart : IPurchaseCart
    {
        _4820_soltaniwebContext db = new _4820_soltaniwebContext();

        public tbl_purchasekart findcartbyid(int cartid)
        {
            return db.tbl_purchasekart.Find(cartid);
        }

        public cartabstractinfo GetCartAbstractinfo(int userid = 0)
        {
            var cart = db.tbl_purchasekart.Find(opencartid(userid));

            cartabstractinfo q = new cartabstractinfo();
            if (cart == null)
            {
                q.totalnumber = 0;
                q.totalprice = 0;
                q.discount = 0;
                q.transportationcost = 0;
                q.payableprice =0;
            }
            else
            {
                q.totalnumber = cart.tbl_purchasekartitemlist.Sum(a => a.number);
                q.totalprice = (int)cart.tbl_purchasekartitemlist.Sum(a => a.totalprice);
                q.discount = cart.discountamount.HasValue ? cart.discountamount.Value : 0;
                q.transportationcost = cart.transportationcost.HasValue ? cart.transportationcost.Value : 0;
                q.payableprice = q.totalprice + q.transportationcost - q.discount;
            }


            return q;
        }

        public bool ISPriceValid(int cartid = 0)
        {
            var cart = db.tbl_purchasekart.Find(cartid);
            DateTime pricedatetime = cart.GetPricedate.HasValue ? cart.GetPricedate.Value : DateTime.Now;
            DateTime dateofnow = DateTime.Now;
            int days = (dateofnow - pricedatetime).Days;
            if (days < 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int itemnumberincart(int userid = 0)
        {
            var pcartopen = db.tbl_purchasekart.Where(a => a.userid == userid && a.ispaid == false).SingleOrDefault();
            int num = db.tbl_purchasekartitemlist.Where(a => a.perchasekart_id == pcartopen.id).Count();
            return num;




        }

        public int opencartid(int userid)
        {
            int cartid = 0;
            if (db.tbl_purchasekart.Where(a => a.userid == userid).Where(a => a.ispaid == false).Count() != 0)
            {
                cartid = db.tbl_purchasekart.Where(a => a.userid == userid).Where(a => a.ispaid == false).FirstOrDefault().id;
            }


            return cartid;

        }

        public void settransportaiondetails(int cartid, tbl_transportaiondetails tcartdetails)
        {
            var cart = findcartbyid(cartid);
            if (cart.tbl_transportaiondetails.Count()==0)
            {
                //add
                db.tbl_transportaiondetails.Add(tcartdetails);


            }
            else
            {
                //Edit
                var tdetails = cart.tbl_transportaiondetails.FirstOrDefault();
                tdetails.location_address = tcartdetails.location_address;
                tdetails.location_address = tcartdetails.location_name;
                tdetails.person_peygiri = tcartdetails.person_peygiri;
                tdetails.tell = tcartdetails.tell;
               
            }
            db.SaveChanges();
        }
    }
}
