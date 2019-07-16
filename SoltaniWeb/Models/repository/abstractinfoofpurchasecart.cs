using SoltaniWeb.Models.Domain;
using SoltaniWeb.Models.structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoltaniWeb.Models.repository
{
    public class abstractinfoofpurchasecart
    {
        _4820_soltaniwebContext db = new _4820_soltaniwebContext();
        public editnumberiteminpurchasecart getabstractcartinfo(int cart_id = 0)
        {
            editnumberiteminpurchasecart result = new editnumberiteminpurchasecart();

            var cartlist = db.tbl_purchasekartitemlist.Where(a => a.perchasekart_id == cart_id);
            decimal totalprice = 0, totalweight = 0, totalcosttransportation = 0, discount = 0, totalweightsaved = 0;
            string totalpricestr, totalcosttransportationstr, discountstr, discountcode;
            var transportationdetails = db.tbl_transportaiondetails.Where(a => a.cartid == cart_id);
            var transportaioncost = db.tbl_transportationcost.Where(a => a.cart_id == cart_id);
            foreach (var item in cartlist)
            {
                totalprice = totalprice + (decimal)(item.number * item.price);
                totalweight = totalweight + (decimal)(item.number * (item.product_.weight.HasValue == true ? item.product_.weight.Value : 0));
            }
            totalpricestr = string.Format("{0:#,##0.##}", totalprice) + " ریال";
            if (cartlist.Count() > 0)
            {

                if (cartlist.FirstOrDefault().perchasekart_.discount_id != null)
                {
                    discount = Math.Floor(cartlist.FirstOrDefault().perchasekart_.discount_.percentage.Value * totalprice / 100);
                    discountstr = string.Format("{0:#,##0.##}", discount) + " ریال";
                    discountcode = cartlist.FirstOrDefault().perchasekart_.discount_.discountcode;
                }
                else
                {
                    discount = 0;
                    discountstr = string.Format("{0:#,##0.##}", discount) + " ریال";
                    discountcode = "";
                }
                if (db.tbl_transportationcost.Where(a => a.cart_id == cart_id).Count() > 0)
                {

                    totalcosttransportation = db.tbl_transportationcost.Where(a => a.cart_id == cart_id).Sum(a => a.totaltcost.Value);
                    totalcosttransportationstr = string.Format("{0:#,##0.##}", totalcosttransportation) + " ریال";
                }
                else
                {
                    totalcosttransportation = 0;
                    totalcosttransportationstr = string.Format("{0:#,##0.##}", totalcosttransportation) + " ریال";
                }
                decimal payableprice = 0;
                string payablepricestr = "";

                payableprice = totalprice + totalcosttransportation - discount;
                payablepricestr = string.Format("{0:#,##0.##}", payableprice) + " ریال";
                result.totalprice = totalpricestr;
                result.totalcosttransportation = totalcosttransportationstr;
                result.discount = discountstr;
                result.payableprice = payablepricestr;
                result.totalweight = totalweight.ToString() + "kg";

                return result;
            }
            else
            {
                return new editnumberiteminpurchasecart { totalprice = "", totalcosttransportation = "", discount = "", payableprice = "" };

            }
        }
    }
}