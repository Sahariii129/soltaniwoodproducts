using SoltaniWeb.Models.structs;
using SoltaniWeb.Models.Domain;
using SoltaniWeb.Models.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;


using SoltaniWeb.Models.utility;

using FtpClass;
using System.Globalization;
using SoltaniWeb.Models;
using SoltaniWeb.Models.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using SoltaniWeb.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Net.Http;
using SoltaniWeb.Extensions;

namespace SoltaniWeb.Controllers
{
    //[HandleError(View = "Error")]
    //[getstatisticController]
    //[ResponseCache(Duration =600)]
    public class samplesController : Controller
    {
        // GET: samples
        //[HandleError(View = "Error")]
        _4820_soltaniwebContext db = new _4820_soltaniwebContext();
        public async Task<ActionResult> samples(int id01 = 0, string key = "")
        {

            



            var q =  db.tbl_samples.Where(a => a.productsid == id01);
            var qproduct = await db.tbl_products.Where(a => a.idproduct == id01).SingleOrDefaultAsync();

            int c = await q.CountAsync();


            if (c <=0)
            {
                ViewBag.idproduct = id01;
                ViewBag.message = "نمونه کاری وجود ندارد";
                ViewBag.title = "کالای چوب سلطانی" + "|" + " نمونه کارهای مرتبط با " + " " + qproduct.category.categoryname + " " + qproduct.name + " کد : " + qproduct.codename;
                return View();
            }
            else
            {
                ViewBag.idproduct = id01;
                ViewBag.title = "کالای چوب سلطانی" + "|" + " نمونه کارهای مرتبط با " + " " + q.FirstOrDefault().products.category.categoryname + " " + q.FirstOrDefault().products.name + " کد : " + q.FirstOrDefault().products.codename;
                return View(q);
            }





        }
       


        public ActionResult showsample(int id = 0, string key = "")
        {
           
            var q = db.tbl_files.Where(a => a.id == id).SingleOrDefault();

            ViewBag.Title = "نمونه کار به شماره " + q.id + "-" + " اثر: " + q.madeby.Fname + " " + q.madeby.Lname + " نوع کار : " + q.decortype.name; 
            return View(q);

        }

            

        public ActionResult madeby(int id = 0, string key="")
        {
            try
            {

            var q = db.tbl_files.Where(a => a.madeby.id == id).OrderByDescending(a=>a.id);
            ViewBag.title = "کالای چوب سلطانی" + "|" + " نمونه کارهای " + q.FirstOrDefault().madeby.Fname + " " + q.FirstOrDefault().madeby.Lname;

            return View(q);
            }
            catch (Exception e)
            {
                
                throw e;
            }


        }

        public ActionResult decortype(int id = 0, string key="")
        {
          
            var q = db.tbl_files.Where(a => a.decortype.id == id).OrderByDescending(a => a.id);
            ViewBag.title = "کالای چوب سلطانی" + "|" + " نمونه های " + q.FirstOrDefault().decortype.name.ToString();


            return View(q);

        }


        public ActionResult samples_catg(string key="" )
        {
        
            var q = db.tbl_DecorType.Where(a=> a.tbl_files.Count() > 0).OrderByDescending(a => a.tbl_files.Count());

            return View(q);

        }



        public ActionResult samples_madeby(string key = "")
        {
          
           
            var q = db.tbl_person.Where(a => a.tbl_files.Count() > 0).OrderByDescending(a => a.tbl_files.Count());
           
            ViewBag.title = " طراحان و مجریان ";


            return View(q);

        }

        public ActionResult slideshowmadeby(int id = 0, string key = "")
        {
         
    
            var q = db.tbl_files.Where(a => a.madeby.id == id).OrderByDescending(a=>a.id);
            ViewBag.title = "کالای چوب سلطانی" + "|" + " نمونه کارهای " + q.FirstOrDefault().madeby.Fname + " " + q.FirstOrDefault().madeby.Lname;

            return View(q);


        }




        public ActionResult slideshowdecortype(int id = 0, string key = "")
        {
          
            var q = db.tbl_files.Where(a => a.madeby.id == id).OrderByDescending(a => a.id);
            ViewBag.title = "کالای چوب سلطانی" + "|" + " نمونه های " + q.FirstOrDefault().decortype.name.ToString();


            return View(q);

        }




        
              public ActionResult slideshowsamples(int id01 = 0, string key = "")
        {
           
            var q = db.tbl_samples.Where(a => a.productsid == id01);
            var qproduct = db.tbl_products.Where(a => a.idproduct == id01).SingleOrDefault();




            if (q.Count() <=0)
            {
                ViewBag.idproduct = id01;
                ViewBag.message = "نمونه کاری وجود ندارد";
                ViewBag.title = "کالای چوب سلطانی" + "|" + " نمونه کارهای مرتبط با " + " " + qproduct.category.categoryname + " " + qproduct.name + " کد : " + qproduct.codename;
                return View("samples");
            }
            else
            {
                ViewBag.idproduct = 0;
                ViewBag.title = "کالای چوب سلطانی" + "|" + " نمونه کارهای مرتبط با " + " " + q.FirstOrDefault().products.category.categoryname + " " + q.FirstOrDefault().products.name + " کد : " + q.FirstOrDefault().products.codename;
                return View("slideshowsamples", q);
            }





        }

        [HttpGet]
              public ActionResult samples_AGT()
              {

                   return RedirectToAction("AGT","Products");
                  

              }
        [HttpGet]
        public ActionResult showsample_AGT()
        {
            
            return RedirectToAction("AGT", "Products");


        }

        [HttpGet]
        public ActionResult samples_Ack()
        {

           return RedirectToAction("ACK", "Products");


        }
        [HttpGet]
        public ActionResult showsample_Ack()
        {

          
            return RedirectToAction("ACK", "Products");


        }


    }
}
