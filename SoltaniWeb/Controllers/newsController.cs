using SoltaniWeb.Models;
using SoltaniWeb.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SoltaniWeb.Controllers
{
    [getstatisticController]

    public class newsController : Controller
    {
            _4820_soltaniwebContext db = new _4820_soltaniwebContext();
        // GET: news
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult newsdetails(int id = 0, string key = "")
        {
            //statistics.statisticssave("news", "newsdetails", Request.UserHostAddress, (Nullable<int>)Session["userid"]);

            var q = db.tbl_news.Where(a => a.id == id).SingleOrDefault();

            if (q == null)
                throw new Exception("چنین خبری وجود ندارد");

            ViewBag.title = "کالای چوب سلطانی" + "|" + q.title;
            ViewBag.description = q.abstractnews;

            return View(q);




        }
        //[HttpPost]
        //public JavaScriptResult InsertnewsComment(tbl_newscomments t)
        //{
        //    Database db = new Database();
        //    //var q=db.tbl_newscomments .Where(a=> a.newsid == t.newsid ).SingleOrDefault ();
        //    t.ip = Request.UserHostAddress;
        //    t.datecomment = DateTime.Now;
        //    t.comfirmation = false;
        //    t.parentid = 0;
        //    db.tbl_newscomments.Add(t);
        //    try
        //    {
        //        db.SaveChanges();
        //        return JavaScript(MessageBox.Show("نظر شما با موفقیت ارسال و پس از تائید در سایت ثبت می گردد", Location.topLeft, Type_me.success, Modal.WithModal));
        //    }
        //    catch (Exception e)
        //    {
        //        return JavaScript(MessageBox.Show("در ثبت نظر شما خطایی رخ داده است" + e.Message , Location.topLeft, Type_me.error, Modal.WithModal));
             
        //    }

        //}


        //[HttpPost]
        //public JavaScriptResult InsertreplynewsComment(tbl_newscomments t)
        //{
        //    Database db = new Database();
        //    //var q=db.tbl_newscomments .Where(a=> a.newsid == t.newsid ).SingleOrDefault ();
        //    t.ip = Request.UserHostAddress;
        //    t.datecomment = DateTime.Now;
        //    t.comfirmation = false;
        //    db.tbl_newscomments.Add(t);
        //    try
        //    {
        //        db.SaveChanges();
        //        return JavaScript(MessageBox.Show("نظر شما با موفقیت ارسال و پس از تائید در سایت ثبت می گردد", Location.topLeft, Type_me.success, Modal.WithModal));
        //    }
        //    catch (Exception e)
        //    {
        //        return JavaScript(MessageBox.Show("در ثبت نظر شما خطایی رخ داده است" + e.Message, Location.topLeft, Type_me.error, Modal.WithModal));

        //    }

        //}


    }
}