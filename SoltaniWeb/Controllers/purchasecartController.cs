using SoltaniWeb.Models.Domain;
using SoltaniWeb.Models.structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SoltaniWeb.Models.repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SoltaniWeb.Models.Services.Interfaces;

namespace SoltaniWeb.Controllers
{
    //[getstatisticController]
    public class purchasecartController : Controller
    {
        // GET: purchasecart
        _4820_soltaniwebContext db = new _4820_soltaniwebContext();
        private IUserServices _userServices;
        public purchasecartController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult addnewitem(int product_id = 0)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new cartabstract { number = 0, userid = 0, numberitemexist = 0 });
            }
            else
            {
                int user = _userServices.GetUseridByUsername(User.Identity.Name);
                var product = db.tbl_products.Where(a => a.idproduct == product_id).SingleOrDefault();
                int numberexist;
                var listkala = db.tbl_listkala.Where(a => a.productid == product_id);
                if (listkala.Count() != 0)
                {
                    numberexist = listkala.Sum(a => a.kalanumberm);

                }
                else
                {
                    numberexist = 0;
                }

                if (numberexist == 0)
                {
                    return Json(new cartabstract { number = 0, userid = user, numberitemexist = 0 });

                }

                int number = 0;
                if (db.tbl_purchasekart.Where(a => a.userid == user && a.ispaid == false).Count() == 0)
                {
                    tbl_purchasekart pcart = new tbl_purchasekart();
                    pcart.userid = user;
                    pcart.purchasedatestart = DateTime.Now;
                    pcart.ispaid = false;
                    db.tbl_purchasekart.Add(pcart);
                    db.SaveChanges();
                    tbl_purchasekartitemlist item = new tbl_purchasekartitemlist();
                   
                    item.product_id = product_id;
                    item.number = 1;
                    item.price = product.lastcellcost.HasValue ? product.lastcellcost.Value : 0;
                    item.perchasekart_id = pcart.id;
                    item.purchase_datetime = DateTime.Now;
                    item.totalprice = item.number * item.price;
                    db.tbl_purchasekartitemlist.Add(item);
                    db.SaveChanges();

                    number = db.tbl_purchasekartitemlist.Where(a => a.perchasekart_id == pcart.id).Count();


                }
                else
                {
                    var pcartopen = db.tbl_purchasekart.Where(a => a.userid == user && a.ispaid == false).SingleOrDefault();
                    int kartidopen = pcartopen.id;
                    if (pcartopen.tbl_purchasekartitemlist.Where(a=>a.product_id== product_id).Count()!=0)
                    {
                        return Json(new { preselected = "1" });
                    }

                    tbl_purchasekartitemlist item = new tbl_purchasekartitemlist();
                    item.product_id = product_id;
                    item.number = 1;
                    item.price = product.lastcellcost.HasValue ? product.lastcellcost.Value : 0;
                    item.perchasekart_id = kartidopen;
                    item.purchase_datetime = DateTime.Now;
                    item.totalprice = item.number * item.price;
                    db.tbl_purchasekartitemlist.Add(item);
                    db.SaveChanges();

                    number = db.tbl_purchasekartitemlist.Where(a => a.perchasekart_id == pcartopen.id).Count();
                }
                cartabstract cartabs = new cartabstract();

                cartabs.number = number;
                cartabs.userid = user;
                cartabs.numberitemexist = numberexist;

                return Json(cartabs);
            }


        }


        public ActionResult showcart(int userid = 0)
        {
            //if (!User.Identity.IsAuthenticated)
            //{

            //    return RedirectToAction("login", "home");
            //}

                string discountcode;
            int user = int.Parse(HttpContext.User.Claims.FirstOrDefault().Value);

                if (user != userid)
                {
                    return RedirectToAction("login", "home");

                }
                else
                {

                    var cartlist = db.tbl_purchasekartitemlist.Where(a => a.perchasekart_.ispaid == false && a.perchasekart_.userid == userid).ToList();
                    //
                    decimal totalprice = 0, totalweight = 0, totalcosttransportation = 0, discount = 0;
                    string totalpricestr, totalweightstr, totalcosttransportationstr, discountstr;

                    foreach (var item in cartlist)
                    {
                        totalprice = totalprice + (decimal)(item.number * (item.product_.lastcellcost.HasValue == true ? item.product_.lastcellcost.Value : 0));
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
                        int carid = cartlist.FirstOrDefault().perchasekart_id;
                        if (db.tbl_transportationcost.Where(a => a.cart_id == carid).Count() > 0)
                        {

                            totalcosttransportation = db.tbl_transportationcost.Where(a => a.cart_id == carid).Sum(a => a.totaltcost.Value);
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


                        //
                        ViewBag.totalcosttransportation = totalcosttransportation;
                        ViewBag.totalprice = totalprice;
                        ViewBag.totalnumber = cartlist.Sum(a => a.number);
                        ViewBag.discount = discount;
                        ViewBag.totalweigth = totalweight;
                        ViewBag.payableprice = payableprice;
                        ViewBag.discountcode = discountcode;
                        return View(cartlist);
                    }
                    else
                    {
                        ViewBag.totalcosttransportation = 0;
                        ViewBag.totalprice = 0;
                        ViewBag.totalnumber = 0;
                        ViewBag.discount = 0;
                        ViewBag.totalweigth = 0;
                        ViewBag.payableprice = 0;
                        ViewBag.discountcode = "";
                        TempData["message"] = "سبد کالای شما خالی است";
                        return RedirectToAction("index", "home");
                    }

                    
                }

            
        }

        public editnumberiteminpurchasecart updatepurchasecart(int itemnumber = 0, int purchasecartitemid = 0, int purchasecartid = 0)
        {
            var itemselected = db.tbl_purchasekartitemlist.Find(purchasecartitemid);
            decimal newprice = 0, totalcosttransportation = 0;
            decimal totalpriceitem = 0;
            if (itemnumber != 0)
            {
                newprice = itemselected.product_.lastcellcost.HasValue == true ? itemselected.product_.lastcellcost.Value : 0;
                totalpriceitem = newprice * itemnumber;
            }


            var cartlist = db.tbl_purchasekartitemlist.Where(a => a.perchasekart_id == purchasecartid).ToList();
            decimal totalprice = 0;
            decimal totalweight = 0;
            decimal discount = 0;
            decimal payableprice = 0;
            string discountstr = "", totalcosttransportationstr = "";
            int totalnumber = cartlist.Sum(a => a.number);

            foreach (var item in cartlist)
            {
                totalprice = totalprice + (decimal)(item.number * (item.product_.lastcellcost.HasValue == true ? item.product_.lastcellcost.Value : 0));
                totalweight = totalweight + (decimal)(item.number * (item.product_.weight.HasValue == true ? item.product_.weight.Value : 0));

            }

            if (cartlist.Count()>0)
            {
                
            if (cartlist.FirstOrDefault().perchasekart_.discount_id != null)
            {
                discount = Math.Floor(cartlist.FirstOrDefault().perchasekart_.discount_.percentage.Value * totalprice / 100);

                discountstr = string.Format("{0:#,##0.##}", discount) + " ریال";
            }
            else
            {
                discount = 0;
                discountstr = string.Format("{0:#,##0.##}", discount) + " ریال";
            }

            //int carid = cartlist.FirstOrDefault().perchasekart_id;
            if (db.tbl_transportationcost.Where(a => a.cart_id == purchasecartid).Count() > 0)
            {

                totalcosttransportation = db.tbl_transportationcost.Where(a => a.cart_id == purchasecartid).Sum(a => a.totaltcost.Value);
                totalcosttransportationstr = string.Format("{0:#,##0.##}", totalcosttransportation) + " ریال";
            }
            else
            {
                totalcosttransportation = 0;
                totalcosttransportationstr = string.Format("{0:#,##0.##}", totalcosttransportation) + " ریال";
            }
            payableprice = totalprice + totalcosttransportation - discount;
            editnumberiteminpurchasecart editcart = new editnumberiteminpurchasecart();

            editcart.newprice = string.Format("{0:#,##0.##}", newprice) + " ریال";
            editcart.totalpriceitem = string.Format("{0:#,##0.##}", totalpriceitem) + " ریال";
            editcart.totalprice = string.Format("{0:#,##0.##}", totalprice) + " ریال";
            editcart.totalweight = string.Format("{0:#,##0.##}", totalweight) + " kg";
            editcart.totalnumber = totalnumber.ToString();
            editcart.totalcosttransportation = string.Format("{0:#,##0.##}", totalcosttransportation) + " ریال";
            editcart.discount = string.Format("{0:#,##0.##}", discount) + " ریال";
            editcart.payableprice = string.Format("{0:#,##0.##}", payableprice) + " ریال";
            return editcart;
            }
            else
            {
                editnumberiteminpurchasecart editcart = new editnumberiteminpurchasecart();

                editcart.newprice = string.Format("{0:#,##0.##}", 0) + " ریال";
                editcart.totalpriceitem = string.Format("{0:#,##0.##}", 0) + " ریال";
                editcart.totalprice = string.Format("{0:#,##0.##}", 0) + " ریال";
                editcart.totalweight = string.Format("{0:#,##0.##}", 0) + " kg";
                editcart.totalnumber = "0";
                editcart.totalcosttransportation = string.Format("{0:#,##0.##}", 0) + " ریال";
                editcart.discount = string.Format("{0:#,##0.##}", 0) + " ریال";
                editcart.payableprice = string.Format("{0:#,##0.##}", 0) + " ریال";
                return editcart;
            }

        }
        public ActionResult editnumber(int itemnumber = 0, int id = 0)
        {
            var itemselected = db.tbl_purchasekartitemlist.Find(id);
            int purchasecartid = itemselected.perchasekart_id;
            entityproducutsreposit entityrep = new entityproducutsreposit();
            int max = entityrep.getentityitem(itemselected.product_id);
            if (itemnumber>max)
            {
                string product = itemselected.product_.category.categoryname + " | " + itemselected.product_.name + "  کد " + itemselected.product_.codename;
                return Json(new {status="false" , productname = product, maxcount = max });
            }
            else
            {

            itemselected.number = itemnumber;


            db.SaveChanges();
            editnumberiteminpurchasecart updateoutput = new editnumberiteminpurchasecart();
            updateoutput = updatepurchasecart(itemnumber, id, purchasecartid);



            return Json(updateoutput);
            }

        }


        public ActionResult deleteitem(int id = 0)
        {
            var itemselected = db.tbl_purchasekartitemlist.Find(id);
            int purchasecartid = itemselected.perchasekart_id;

            db.tbl_purchasekartitemlist.Remove(itemselected);
            db.SaveChanges();

            editnumberiteminpurchasecart updateoutput = new editnumberiteminpurchasecart();
            updateoutput = updatepurchasecart(0, id, purchasecartid);
            return Json(updateoutput);


        }
        public List<transportation> getbesttcost(decimal distance, decimal totalweight = 0)
        {
            decimal cost = 0, mintotalcost = 0, number = 0, totalcost = 0;
            int number1 = 0;
            tbl_vehicletype veh1 = new tbl_vehicletype();
            tbl_vehicletype veh2 = new tbl_vehicletype();
            List<tbl_vehicletype> lstveh = new List<tbl_vehicletype>();
            List<tbl_vehicletype> lstveh2 = new List<tbl_vehicletype>();

            decimal selectedcapacity, selectedcapacity2, capacitydif = 0, capacitydif2 = 0, remainingweight = 0;
            int selectedveh1 = 0;
            List<transportation> tlst = new List<transportation>();
            foreach (var item in db.tbl_vehicletype)
            {
                capacitydif = totalweight - item.capacity.Value;
                if (capacitydif > 0)
                {

                    lstveh.Add(item);
                }

            }

            if (lstveh.Count() > 0)
            {
                selectedcapacity = lstveh.Max(a => a.capacity.Value);
                var vehsel1 = db.tbl_vehicletype.Where(a => a.capacity > selectedcapacity);
                if (vehsel1.Count() > 0)
                {
                    decimal selectedcapacityasl = vehsel1.Min(a => a.capacity.Value);
                    veh1 = vehsel1.Where(a => a.capacity == selectedcapacityasl).SingleOrDefault();
                }
                else
                {
                    veh1 = db.tbl_vehicletype.OrderByDescending(a => a.capacity.Value).FirstOrDefault();
                }
            }
            else
            {
                veh1 = db.tbl_vehicletype.OrderBy(a => a.capacity.Value).FirstOrDefault();
            }



            transportation t1 = new transportation();
            t1.number = Math.Floor(totalweight / veh1.capacity.Value) == 0 ? 1 : Math.Floor(totalweight / veh1.capacity.Value);
            number = t1.number;

            cost = (Math.Floor(veh1.factor.Value * distance) > db.tbl_vehicletype.Where(a => a.id == veh1.id).SingleOrDefault().mincost.Value) ? Math.Floor(veh1.factor.Value * distance) : db.tbl_vehicletype.Where(a => a.id == veh1.id).SingleOrDefault().mincost.Value;
            t1.cost = cost.ToString();
            totalcost = (t1.number * Convert.ToDecimal(cost));
            t1.totalcost = totalcost.ToString();
            t1.vehicletype = veh1.vehiclename;
            t1.vehicletypeid = veh1.id;
            t1.distance = distance.ToString() + " km";
            t1.address = "نحوه حمل پیشنهادی" + "= " + "حداقل کرایه کل";
            tlst.Add(t1);

            remainingweight = totalweight - (number * veh1.capacity.Value);
            if (remainingweight > 0)
            {

                foreach (var item2 in db.tbl_vehicletype)
                {
                    capacitydif2 = remainingweight - item2.capacity.Value;
                    if (capacitydif2 < 0)
                    {

                        lstveh2.Add(item2);
                    }

                }
                if (lstveh2.Count() > 0)
                {

                    selectedcapacity2 = lstveh2.Min(a => a.capacity.Value);
                    veh2 = lstveh.Where(a => a.capacity == selectedcapacity2).SingleOrDefault();

                    transportation t2 = new transportation();
                    t2.number = Math.Ceiling(remainingweight / veh2.capacity.Value);
                    cost = (Math.Floor(veh2.factor.Value * distance) > db.tbl_vehicletype.Where(a => a.id == veh2.id).SingleOrDefault().mincost.Value) ? Math.Floor(veh2.factor.Value * distance) : db.tbl_vehicletype.Where(a => a.id == veh2.id).SingleOrDefault().mincost.Value;
                    t2.cost = cost.ToString();
                    totalcost = (t2.number * Convert.ToDecimal(cost));
                    t2.totalcost = totalcost.ToString();
                    t2.vehicletype = veh2.vehiclename;
                    t2.vehicletypeid = veh2.id;
                    t2.distance = distance.ToString() + " km";
                    t2.address = "نحوه حمل پیشنهادی" + "= " + "حداقل کرایه کل";
                    tlst.Add(t2);
                }
                else
                {
                    transportation t2 = new transportation();
                    t2.number = 1;
                    cost = (Math.Floor(db.tbl_vehicletype.FirstOrDefault().factor.Value * distance) > db.tbl_vehicletype.FirstOrDefault().mincost.Value) ? Math.Floor(db.tbl_vehicletype.FirstOrDefault().factor.Value * distance) : db.tbl_vehicletype.FirstOrDefault().mincost.Value;
                    t2.cost = cost.ToString();
                    totalcost = (1 * Convert.ToDecimal(cost));
                    t2.totalcost = totalcost.ToString();
                    t2.vehicletype = db.tbl_vehicletype.FirstOrDefault().vehiclename;
                    t2.vehicletypeid = veh2.id;
                    t2.distance = distance.ToString() + " km";
                    t2.address = "نحوه حمل پیشنهادی" + "= " + "حداقل کرایه کل";
                    tlst.Add(t2);
                }


            }








            return tlst;

        }
        public decimal totalcostindescimal(transportation a)
        {
            decimal result = 0;
            result = Convert.ToDecimal(a.totalcost);



            return result;
        }
        public ActionResult transportation(string distance, string cartid, string lat, string lng)
        {


            decimal totalweight = 0;

            var cartlist = db.tbl_purchasekartitemlist.ToList().Where(a => a.perchasekart_id == int.Parse(cartid)).ToList();

            foreach (var item in cartlist)
            {
                totalweight = totalweight + (decimal)(item.number * (item.product_.weight.HasValue == true ? item.product_.weight.Value : 0));

            }
            decimal distanceint = Math.Ceiling(Convert.ToDecimal(distance) / 1000);

            List<transportation> t1 = new List<transportation>();
            t1 = getbesttcost(distanceint, totalweight);
            var q = db.tbl_transportationcost.ToList().Where(a => a.cart_id == Convert.ToInt32(cartid));
            if (q.Count() == 0)
            {
                foreach (var item in t1)
                {
                    tbl_transportationcost tcost = new tbl_transportationcost();

                    tcost.cart_id = Convert.ToInt32(cartid);
                    tcost.Lat = lat;
                    tcost.Lng = lng;
                    tcost.vehicle_id = item.vehicletypeid;
                    tcost.number = Convert.ToInt32(item.number);
                    tcost.tcost = (Convert.ToDecimal(item.cost) > db.tbl_vehicletype.Where(a => a.id == item.vehicletypeid).SingleOrDefault().mincost.Value) ? Convert.ToDecimal(item.cost) : db.tbl_vehicletype.Where(a => a.id == item.vehicletypeid).SingleOrDefault().mincost.Value;
                    tcost.totaltcost = Convert.ToDecimal(item.totalcost);
                    tcost.totalweight = totalweight;
                    tcost.distance = distanceint;
                    db.tbl_transportationcost.Add(tcost);
                }


            }
            else
            {
                db.tbl_transportationcost.RemoveRange(q);
                foreach (var item in t1)
                {
                    tbl_transportationcost tcost = new tbl_transportationcost();

                    tcost.cart_id = Convert.ToInt32(cartid);
                    tcost.Lat = lat;
                    tcost.Lng = lng;
                    tcost.vehicle_id = item.vehicletypeid;
                    tcost.number = Convert.ToInt32(item.number);
                    tcost.tcost = (Convert.ToDecimal(item.cost) > db.tbl_vehicletype.Where(a => a.id == item.vehicletypeid).SingleOrDefault().mincost.Value) ? Convert.ToDecimal(item.cost) : db.tbl_vehicletype.Where(a => a.id == item.vehicletypeid).SingleOrDefault().mincost.Value;
                    tcost.totaltcost = Convert.ToDecimal(item.totalcost);
                    tcost.distance = distanceint;
                    tcost.totalweight = totalweight;

                    db.tbl_transportationcost.Add(tcost);
                }


            }

            db.SaveChanges();

            var qnew = db.tbl_transportationcost.ToList().Where(a => a.cart_id == Convert.ToInt32(cartid)).Select(g => g);

            return PartialView("_transportation", qnew);
        }


        public ActionResult transportation2(string distance, string cartid, string lat, string lng)
        {


            decimal totalweight = 0;

            var cartlist = db.tbl_purchasekartitemlist.ToList().Where(a => a.perchasekart_id == int.Parse(cartid)).ToList();

            foreach (var item in cartlist)
            {
                totalweight = totalweight + (decimal)(item.number * (item.product_.weight.HasValue == true ? item.product_.weight.Value : 0));

            }

            decimal distanceint = Math.Ceiling(Convert.ToDecimal(distance));

            List<transportation> t1 = new List<transportation>();
            t1 = getbesttcost(distanceint, totalweight);
            var q = db.tbl_transportationcost.ToList().Where(a => a.cart_id == Convert.ToInt32(cartid));
            if (q.Count() == 0)
            {
                foreach (var item in t1)
                {
                    tbl_transportationcost tcost = new tbl_transportationcost();

                    tcost.cart_id = Convert.ToInt32(cartid);
                    tcost.Lat = lat;
                    tcost.Lng = lng;
                    tcost.vehicle_id = item.vehicletypeid;
                    tcost.number = Convert.ToInt32(item.number);
                    tcost.tcost = (Convert.ToDecimal(item.cost) > db.tbl_vehicletype.Where(a => a.id == item.vehicletypeid).SingleOrDefault().mincost.Value) ? Convert.ToDecimal(item.cost) : db.tbl_vehicletype.Where(a => a.id == item.vehicletypeid).SingleOrDefault().mincost.Value;
                    tcost.totaltcost = Convert.ToDecimal(item.totalcost);
                    tcost.totalweight = totalweight;
                    tcost.distance = distanceint;
                    db.tbl_transportationcost.Add(tcost);
                }


            }
            else
            {
                db.tbl_transportationcost.RemoveRange(q);
                foreach (var item in t1)
                {
                    tbl_transportationcost tcost = new tbl_transportationcost();

                    tcost.cart_id = Convert.ToInt32(cartid);
                    tcost.Lat = lat;
                    tcost.Lng = lng;
                    tcost.vehicle_id = item.vehicletypeid;
                    tcost.number = Convert.ToInt32(item.number);
                    tcost.tcost = (Convert.ToDecimal(item.cost) > db.tbl_vehicletype.Where(a => a.id == item.vehicletypeid).SingleOrDefault().mincost.Value) ? Convert.ToDecimal(item.cost) : db.tbl_vehicletype.Where(a => a.id == item.vehicletypeid).SingleOrDefault().mincost.Value;
                    tcost.totaltcost = Convert.ToDecimal(item.totalcost);
                    tcost.distance = distanceint;
                    tcost.totalweight = totalweight;

                    db.tbl_transportationcost.Add(tcost);
                }


            }

            db.SaveChanges();

            var qnew = db.tbl_transportationcost.ToList().Where(a => a.cart_id == Convert.ToInt32(cartid)).Select(g => g);

            return PartialView("_transportation", qnew);
        }

        public ActionResult getlatlngdest(int cartid = 0)
        {

            string lat, lng;
            var trans = db.tbl_transportationcost.Where(a => a.cart_id == cartid);
            if (trans.Count() > 0)
            {

                lat = db.tbl_transportationcost.Where(a => a.cart_id == cartid).FirstOrDefault().Lat;
                lng = db.tbl_transportationcost.Where(a => a.cart_id == cartid).FirstOrDefault().Lng;
            }
            else
            {
                lat = "";
                lng = "";

            }
            latlng l = new latlng();
            l.lat = lat;
            l.lng = lng;
            return Json(l);
        }

        public ActionResult transportationdel(int cartid = 0)
        {
            var transcosts = db.tbl_transportationcost.Where(a => a.cart_id == cartid);
            if (transcosts.Count() > 0)
            {
                db.tbl_transportationcost.RemoveRange(transcosts);
                if (db.tbl_transportaiondetails.Where(a => a.cartid == cartid).Count() > 0)
                {
                    db.tbl_transportaiondetails.RemoveRange(db.tbl_transportaiondetails.Where(a => a.cartid == cartid));
                }
                db.SaveChanges();
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }



        public ActionResult savetransportation(tbl_transportaiondetails t)
        {
            var cart = db.tbl_purchasekart.Where(a => a.id == t.cartid).SingleOrDefault();
            var transportcost = cart.tbl_transportationcost;
            var transportationdetail = db.tbl_transportaiondetails.Where(a => a.cartid == t.cartid);
            if (transportcost.Count() > 0)
            {
                if (transportationdetail.Count() == 0)
                {

                    t.lat = transportcost.FirstOrDefault().Lat.ToString();
                    t.lng = transportcost.FirstOrDefault().Lng.ToString();
                    t.distance = transportcost.FirstOrDefault().distance.ToString();
                    db.tbl_transportaiondetails.Add(t);
                }
                else
                {
                    db.tbl_transportaiondetails.RemoveRange(transportationdetail);
                    t.lat = transportcost.FirstOrDefault().Lat.ToString();
                    t.lng = transportcost.FirstOrDefault().Lng.ToString();
                    t.distance = transportcost.FirstOrDefault().distance.ToString();
                    db.tbl_transportaiondetails.Add(t);
                }
                if (ModelState.IsValid == true)
                {
                    db.SaveChanges();
                    var q = db.tbl_transportaiondetails.Where(a => a.cartid == t.cartid).SingleOrDefault();



                    return Json(new { success = 1, errors = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToList(), l_name = q.location_name, l_address = q.location_address, l_peygiri = q.person_peygiri, l_tell = q.tell });

                }
                else
                {
                    return Json(new { success = 2, errors = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToList() });

                }


            }
            else
            {
                return Json(new { success = 3 });

            }


        }



        public ActionResult updatedatacost(int cartid = 0)
        {
            var cartlist = db.tbl_purchasekartitemlist.Where(a => a.perchasekart_id == cartid);
            decimal totalprice = 0, totalweight = 0, totalcosttransportation = 0, discount = 0, totalweightsaved = 0;
            string totalpricestr, totalcosttransportationstr, discountstr, discountcode;
            var transportationdetails = db.tbl_transportaiondetails.Where(a => a.cartid == cartid);
            var transportaioncost = db.tbl_transportationcost.Where(a => a.cart_id == cartid);
            if (transportaioncost.Count() > 0 && transportationdetails.Count() == 0)
            {
                return Json(new { details = false });

            }
            foreach (var item in cartlist)
            {
                totalprice = totalprice + (decimal)(item.number * (item.product_.lastcellcost.HasValue == true ? item.product_.lastcellcost.Value : 0));
                totalweight = totalweight + (decimal)(item.number * (item.product_.weight.HasValue == true ? item.product_.weight.Value : 0));
            }
            if (transportaioncost.Count() > 0)
            {

                totalweightsaved = db.tbl_transportationcost.Where(a => a.cart_id == cartid).FirstOrDefault().totalweight.Value;
            }
            if (transportaioncost.Count() > 0 && totalweight != totalweightsaved)
            {
                return Json(new { status = false });
            }

            totalpricestr = string.Format("{0:#,##0.##}", totalprice) + " ریال";
            if (cartlist.Count()>0)
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
            if (db.tbl_transportationcost.Where(a => a.cart_id == cartid).Count() > 0)
            {

                totalcosttransportation = db.tbl_transportationcost.Where(a => a.cart_id == cartid).Sum(a => a.totaltcost.Value);
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

            return Json(new { status = true, totalprice = totalpricestr, discount = discountstr, totalcosttransportation = totalcosttransportationstr, payableprice = payablepricestr, discountcode = discountcode });

            }
            else
            {
                return Json(new { status = true, totalprice = "0", discount = "0", totalcosttransportation = "0", payableprice = "0", discountcode = "0" });

            }

        }


        public ActionResult applydiscount(string discountcode, int cartid = 0)
        {
            var q = db.tbl_purchasekart.Where(a => a.id == cartid).SingleOrDefault();
            DateTime now = DateTime.Now;

            var discountrecord = db.tbl_discount.ToList().Where(a => a.discountcode.ToLower() == discountcode.ToLower() && a.status == true);
            if (discountrecord.Count() > 0)
            {
                q.discount_id = discountrecord.SingleOrDefault().id;
                db.SaveChanges();
                //
                var cartlist = db.tbl_purchasekartitemlist.Where(a => a.perchasekart_id == cartid);
                decimal totalprice = 0, totalweight = 0, totalcosttransportation = 0, discount = 0;
                string totalpricestr, totalweightstr, totalcosttransportationstr, discountstr;
                foreach (var item in cartlist)
                {
                    totalprice = totalprice + (decimal)(item.number * (item.product_.lastcellcost.HasValue == true ? item.product_.lastcellcost.Value : 0));
                    totalweight = totalweight + (decimal)(item.number * (item.product_.weight.HasValue == true ? item.product_.weight.Value : 0));
                }



                totalpricestr = string.Format("{0:#,##0.##}", totalprice) + " ریال";
                if (cartlist.Count() > 0)
                {

                    if (cartlist.FirstOrDefault().perchasekart_.discount_id != null)
                    {
                        discount = Math.Floor(cartlist.FirstOrDefault().perchasekart_.discount_.percentage.Value * totalprice / 100);
                        discountstr = string.Format("{0:#,##0.##}", discount) + " ریال";
                    }
                    else
                    {
                        discount = 0;
                        discountstr = string.Format("{0:#,##0.##}", discount) + " ریال";
                    }
                    if (db.tbl_transportationcost.Where(a => a.cart_id == cartid).Count() > 0)
                    {

                        totalcosttransportation = db.tbl_transportationcost.Where(a => a.cart_id == cartid).Sum(a => a.totaltcost.Value);
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


                    //



                    return Json(new { status = true, totalprice = totalpricestr, discount = discountstr, totalcosttransportation = totalcosttransportationstr, payableprice = payablepricestr, discountcode = discountcode });
                }
                else {
                    return Json(new { status = true, totalprice = "0", discount = "0", totalcosttransportation = "0", payableprice = "0", discountcode = "" });
                    
                }
            }
            else
            {
                return Json(new { status = false, });
            }




        }


        public ActionResult deletediscount(int cartid = 0)
        {

            try
            {

                var cart = db.tbl_purchasekart.Where(a => a.id == cartid).SingleOrDefault();
                cart.discount_id = null;
                db.SaveChanges();
                return Json(true);
            }
            catch (Exception)
            {

                return Json(false);
            }
        }



        public ActionResult getupdatetransportcost(int cartid = 0)
        {
            var q = db.tbl_transportationcost.Where(a => a.cart_id == cartid);
            if (q.Count() > 0)
            {
                return Json(new { status = true, lat = q.FirstOrDefault().Lat, lng = q.FirstOrDefault().Lng });
            }
            else
            {
                return Json(new { status = false });
            }


        }

    }
}