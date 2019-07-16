using SoltaniWeb.Models.structs;
using SoltaniWeb.Models.Domain;
using SoltaniWeb.Models.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Diagnostics;
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
using SoltaniWeb.Models.Services.Interfaces;
using Kendo.Mvc.UI;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using OfficeOpenXml;
using SoltaniWeb.Models.structs.PersonVM;
using SoltaniWeb.Models.Services.ArchiveSms;

namespace SoltaniWeb.Controllers
{
    //[HandleError(View = "Error")]

    [SWPAuthorizeIdentityController]
    public class AdminController : Controller
    {
        // GET: Admin
        _4820_soltaniwebContext db3 = new _4820_soltaniwebContext();
        private readonly IHubContext<ChatHub> _hub;
        private IUserServices _userServices;
        private IProductsServices _ProductsServices;
        private readonly IArchiveSmsService _archiveSmsService;

        public AdminController(IHubContext<ChatHub> hub, IUserServices userServices, IProductsServices productsServices, IArchiveSmsService archiveSmsService)
        {

            _hub = hub;
            _userServices = userServices;
            _ProductsServices = productsServices;
            _archiveSmsService = archiveSmsService;
        }





        //[HttpGet]
        //public ActionResult userpanel()
        //{

        //    string username = User.Identity.Name;

        //    var q = db3.tbl_user.Where(a => a.username == username && a.Access == 2).SingleOrDefault();

        //    return View(q);



        //}

        public ActionResult Report(int id = 0)
        {


            return View();
        }


        public ActionResult setlic()
        {
            // step 1: inject our patched license verification method in-memory
            //StimulsoftLicenseHelper.Activate();

            // step 2: set license key
            //var lic = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHlNHxonFYHbpjDx+27slnZs5ftj9P0vr+YW2Pss00P90qmOB2PRI4SjLhAufUnckk8d8LLVu4bgMi/81ymyeO7J3xLjnveXaZmAwzelnxcpnNxi4aUQulVJFvwK+qiOxltk2avtYSEE8jmq4WkdUTp9xske7sHZRhrGePQV5sn54z+RtcNe6cUUNgTwUc8dglgPK/WFe8jcs9XjzaRp3RM+l4PfDluVrp1RaQNJsb10wa5XZT2xv3nuWlW9OMBsPLc/63Hsm5zhEfIP3P6J0pV9x/P2lHAiXuDGKMbeKntP0fxrqxRGrhelfT9AV1O5eDpofH8TAi9HvDPn6LHKsddVVcRfXq658kn94eQWyZkEwfPQJWgQUmXhPcZbj5ecea/dnruFjI+1Y1sxLbJA2wzzYtCU2NEj0kAfxId97OiRITg7oQZs2IMiaOFJgvDL1hR5uv69OffNFZk6+h2bm8OB9GF/PZuisO+BzM+HQRlPQP8FWo6jd79o03LiCtH6mVOrQfEAhF8r0qeF7y8TV2OJ8hHnve7mXKTXr4KmGkRzzmDmfAMrpfoPFF4HtglInJTHbKYqiH2z1DDpJVUNa0+kn/Y9RzSZLtIRBVNzgUMubsB+CPJWMal80oD9bTW8/I3O7QFgvSGIUgCaFAs3rYlkJ3Obt7AVXexGQUuTph7Z4auM4/V4DDhhvdy+8+PIj1/kAilviTYXWxqhbLfciZOEMHeGCtepilOvgvY7arGeSqv5tUfIPqoyBh6RSczg+Hf0soFq0t7ysW3FyteYsfsbHppN1htRrfcP3fEn89vddsamdUgUBgZJ8nDKZ+4E0d+g3w+Jdegnu7PAjNjd5YKLt85NfBRwktdWGwp7fKvK2NpidjRVXRHi1CYlcDoxeaCqOqW7/vPv668X6omx6EsYOyWiFskWQxm/E7dpA4ixmNOyMgolUMRbqQK1uVIT1xeavP3r3C9Pm5f4pBkzpUxX54pc6Z+T/psyIXfR77C+kVXEt3+TA9TX+jZ7Zi8oSX1ImDJWn9wPFMCl11Gl3vHTYno2cvK2zeM8vasV/Q+C9GRJkXkDEceDLHaFbDiWQaJi4TJex/1sbYS239UhciSOVizEhpdVA1E7D6vPzSJ9TQjgWtrbANDmjpHzK2YiTnvbUKf9FibYLmtl0iJ3WUhzO1R03jDUJSvF030BPxGWUt5UMTSpaImLTboUbWbCkLnkDXPyxT9xZORjgWTQzVYNUmCXW2qEtzOyVbB+m16KzWvFf69QI1zMEo/N6a1JzT7499RnmqkuqPyBESnVvAaG7Qfz8jff5tK6ml+BTsCRX4MsHV/Sh100Tv4Kt3raiWKyBG6L2UVbKvtaFnGSYPJzNSY6ndlrxWC65VSSbeXxR6AzKVjLw9sgJLh9i6IQ5d5cUfEFzFKNWvCPDiEERKPpG2Vj4yR3HoayBrgpRqgaLAq1/V//hlWT/dcRIevJGjDgbZGv2E1KOiB4Llj/HX0HAO7cCZrtM57hf5jMnqGDvkYSOOHTPqMPv9QbLdBX+5RnHoa8xtXPp1vVPbJXZHJ/f9rVOnGCqjtR6pWAGDYOOZkXZ02y0zjow0go2JuCZnzYZw37U12hcrb2gxPAlmy/SQerFJXaySRLSXu0QW1pfw==";
            //StiLicense.Key = lic;
            return View();
        }



        public ActionResult chatroom2()
        {
            int userid = 78;
            tbl_user t = db3.tbl_user.Where(a => a.id == userid).SingleOrDefault();
            return View(t);
        }
        public ActionResult chatroom()
        {
            //int userid = int.Parse(User.Claims.FirstOrDefault().Value);
            int userid = int.Parse(User.Claims.FirstOrDefault().Value);
            tbl_user t = db3.tbl_user.Where(a => a.id == userid).SingleOrDefault();
            return View(t);
        }

        public ActionResult getsignalmsgfrom(int to_userid, int? from_userid)
        {


            var msglistvisible = db3.tbl_signalRmsg.Where(a => a.visibleforall != false && (a.msgcondition.HasValue ? a.msgcondition.Value != 1 : 1 == 1));

            if (from_userid == null)
            {

                var msglist = msglistvisible.Where(a => a.to_userid == from_userid).OrderByDescending(a => a.id).Take(300);
                var q = msglist.ToList().OrderBy(a => a.datetime_send).Select(g => new { msgid = g.id, fromusername = g.from_user.username, fromuserid = g.from_userid, tousername = "", touseid = "", message = g.msg_text, messagepure = g.msg_textpure, thisdatetime = (g.datetime_send.Value.ToPersianDate() + "  " + g.datetime_send.Value.ToString("HH:mm:ss")), isread = g.datetime_read.HasValue == true ? true : false, branch = g.from_user.branches_id.HasValue ? g.from_user.branches_id.Value : 0, contacttype = "public" });
                foreach (var item in db3.tbl_signalRmsg.Where(a => a.to_userid == from_userid && a.datetime_read == null))
                {
                    item.datetime_read = DateTime.Now;

                }
                db3.SaveChanges();

                return Json(q);
            }
            else
            {

                var msglist = msglistvisible.Where(a => (a.from_userid == from_userid && a.to_userid == to_userid && a.visibletoto != false) || (a.from_userid == to_userid && a.to_userid == from_userid && a.visibletofrom != false));
                var q = msglist.ToList().OrderBy(a => a.datetime_send).Select(g => new { msgid = g.id, fromusername = g.from_user.username, fromuserid = g.from_userid, tousername = g.from_user.username, touseid = g.to_userid, message = g.msg_text, messagepure = g.msg_textpure, thisdatetime = (g.datetime_send.Value.ToPersianDate() + "  " + g.datetime_send.Value.ToString("HH:mm:ss")), isread = g.datetime_read.HasValue == true ? true : false, contacttype = "single" });
                var unreadmsg = db3.tbl_signalRmsg.Where(a => a.to_userid == to_userid && a.from_userid == from_userid && a.datetime_read == null);
                foreach (var item in unreadmsg)
                {
                    item.datetime_read = DateTime.Now;

                    _hub.Clients.All.SendAsync("reading", item.id);
                }


                db3.SaveChanges();


                return Json(q);
            }
        }
        public ActionResult profile()
        {

            //int userid = int.Parse(User.Claims.FirstOrDefault().Value);
            int userid = int.Parse(User.Claims.FirstOrDefault().Value);

            var profile = db3.tbl_user.Where(a => a.id == userid).SingleOrDefault();


            return View(profile);
        }



        [HttpGet]
        public ActionResult manage_userpanel()
        {




            return View();

        }


        public ActionResult manage_accesslevels()
        {


            var q = db3.tbl_accesslevels;
            return View(q);



        }



        [HttpGet]
        public ActionResult editusers(int id = 0)
        {
            string username = User.Identity.Name;

            var q = db3.tbl_user.Where(a => a.username == username && a.Access == 0).SingleOrDefault();
            if (q != null)
            {

                var quser = db3.tbl_user.Where(a => a.id == id).SingleOrDefault();



                return View(quser);
            }
            else
            {
                return RedirectToAction("login", "home");
            }



        }
        [HttpPost]
        public ActionResult editusers(tbl_user t, IFormFile image)
        {




            var q = db3.tbl_user.Where(a => a.id == t.id).SingleOrDefault();



            if (image != null)
            {

                //var qTools = db3.Tbl_Tools.OrderByDescending(a => a.ID).FirstOrDefault();

                if (image.Length <= 2097152)
                {
                    if (image.ContentType == "image/jpeg")
                    {

                        var ms = new MemoryStream();


                        image.CopyTo(ms);
                        byte[] b = ms.ToArray();

                        System.Drawing.Image imgmem = System.Drawing.Image.FromStream(ms);
                        System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(imgmem, 400, 300);

                        System.IO.MemoryStream memThumbnail = new System.IO.MemoryStream();
                        bmp.Save(memThumbnail, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] bb = memThumbnail.ToArray();
                        q.img = bb;




                    }
                    else
                    {
                        ViewBag.Message = "فرمت فایل باید jpg باشد";
                        return View(t);
                    }
                }
                else
                {
                    ViewBag.Message = "حجم تصویر بیش از 512 کیلوبایت می باشد";
                    return View(t);
                }
            }

            q.name = t.name;
            q.Lname = t.Lname;
            q.password = t.password;
            q.status = t.status;
            q.userdescription = t.userdescription;
            q.Access = t.Access;
            q.ftpid = 5;





            db3.SaveChanges();
            //ViewBag.message = "حساب کاربری مورد نظر با موفقیت به روز رسانی گردید";
            return RedirectToAction("manage_userpanel", "admin");


        }
        [HttpPost]
        public ActionResult deleteuser(int id = 0)
        {


            var quser = db3.tbl_user.Where(a => a.id == id).SingleOrDefault();


            DeleteParametr d = new DeleteParametr();
            d.FtpID = (int)quser.ftpid;
            d.FileName = quser.image;

            if (Ftp.DeleteWithCkeck(d, false) == true)
            {
                db3.tbl_user.Remove(quser);
                db3.SaveChanges();
            }


            return RedirectToAction("manage_userpanel", "admin");
        }


        [HttpGet]
        public ActionResult adduser()
        {



            return View();

        }

        [HttpPost]
        public ActionResult adduser(tbl_user t, IFormFile image)
        {



            if (image != null)
            {

                //var qTools = db3.Tbl_Tools.OrderByDescending(a => a.ID).FirstOrDefault();

                if (image.Length <= 2097152)
                {
                    if (image.ContentType == "image/jpeg")
                    {


                        var ms = new MemoryStream();


                        image.CopyTo(ms);
                        byte[] b = ms.ToArray();

                        System.Drawing.Image imgmem = System.Drawing.Image.FromStream(ms);
                        System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(imgmem, 400, 300);

                        System.IO.MemoryStream memThumbnail = new System.IO.MemoryStream();
                        bmp.Save(memThumbnail, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] bb = memThumbnail.ToArray();
                        t.img = bb;


                    }
                    else
                    {
                        ViewBag.Message = "فرمت فایل باید jpg باشد";
                        return View(t);
                    }
                }
                else
                {
                    ViewBag.Message = "حجم تصویر بیش از 512 کیلوبایت می باشد";
                    return View(t);
                }
            }


            t.ftpid = 5;
            db3.tbl_user.Add(t);
            try
            {
                db3.SaveChanges();
                ViewBag.Message = "حساب کاربر مورد نظر با موفقیت ثبت گردید";

                return View();
            }
            catch (Exception ex)
            {

                ViewBag.Message = ex.Message;

                return View();
            }


        }
        [HttpGet]
        public ActionResult detailsuser(int id)
        {

            var qusers = db3.tbl_user.Where(a => a.id == id).SingleOrDefault();


            return View(qusers);
        }


        public ActionResult manage_sections()
        {

            var sections = db3.tbl_section.OrderBy(a => a.ordering);
            return View(sections);






        }

        public ActionResult addsection(tbl_section t)
        {

            t.ftpid = 7;
            db3.tbl_section.Add(t);
            db3.SaveChanges();
            return RedirectToAction("manage_sections", "admin");
        }
        [HttpPost]
        public ActionResult editsection(tbl_section t)
        {

            var section = db3.tbl_section.Where(a => a.id == t.id).SingleOrDefault();
            section.name = t.name;
            section.keywords = t.keywords;
            section.name_EN = t.name_EN;
            section.ordering = t.ordering;
            section.shortname = t.shortname;
            section.status = t.status;
            db3.SaveChanges();
            return RedirectToAction("manage_sections", "admin");

        }


        [HttpPost]
        public string editaccesslevel(int id = 0)
        {




            var q = db3.tbl_accesslevels.Where(a => a.id == id).SingleOrDefault();
            if (q == null)
            {
                return "No Action";
            }
            if (q.accessvalue == true)
            {
                q.accessvalue = false;
            }
            else
            {
                q.accessvalue = true;
            }
            db3.SaveChanges();

            return q.accessvalue.ToString();





        }







        [HttpGet]
        public ActionResult manage_products(int sectionid = 0)
        {


            var qproducts_catg = db3.tbl_category.Where(a => a.section_id == sectionid).OrderBy(a => a.id);

            ViewBag.sectionname = db3.tbl_section.Where(a => a.id == sectionid).SingleOrDefault().name;
            ViewBag.sectionid = db3.tbl_section.Where(a => a.id == sectionid).SingleOrDefault().id;

            return View(qproducts_catg);




        }

        [HttpGet]
        public ActionResult editproducts_catg(int id = 0)
        {


            var qcategory = db3.tbl_category.Where(a => a.id == id).SingleOrDefault();

            return View(qcategory);
        }
        [HttpPost]
        public ActionResult editproducts_catg(tbl_category t, IFormFile image)
        {

            var qcategory = db3.tbl_category.Where(a => a.id == t.id).SingleOrDefault();

            if (image != null)
            {

                //var qTools = db3.Tbl_Tools.OrderByDescending(a => a.ID).FirstOrDefault();

                if (image.Length <= 524288)
                {
                    if (image.ContentType.Contains("image"))
                    {

                        byte[] bb = saveimageinsql.perform(image, true, 256, 256);



                        qcategory.image = bb;

                    }
                    else
                    {
                        ViewBag.Message = "فرمت فایل باید jpg باشد";
                        return View(t);
                    }
                }
                else
                {
                    ViewBag.Message = "حجم تصویر بیش از 512 کیلوبایت می باشد";
                    return View(t);
                }
            }

            qcategory.categoryname = t.categoryname;
            qcategory.description = t.description;
            qcategory.shortname = t.shortname;
            qcategory.keywords = t.keywords;

            qcategory.status = t.status;



            db3.SaveChanges();
            //ViewBag.message = "حساب کاربری مورد نظر با موفقیت به روز رسانی گردید";
            return RedirectToAction("manage_products", "admin", new { sectionid = t.section_id });


        }
        [HttpGet]
        public ActionResult addproduct_catg(int sectionid = 0)
        {

            ViewBag.sectionname = db3.tbl_section.Where(a => a.id == sectionid).SingleOrDefault().name;
            ViewBag.sectionid = sectionid;
            return View();


        }
        [HttpPost]
        public ActionResult addproduct_catg(tbl_category t, IFormFile image)
        {

            if (image != null)
            {

                //var qTools = db3.Tbl_Tools.OrderByDescending(a => a.ID).FirstOrDefault();
                if (image.Length <= 524288)
                {
                    if (image.ContentType.Contains("image"))
                    {

                        t.image = saveimageinsql.perform(image, true);


                    }
                    else
                    {
                        ViewBag.Message = "فرمت فایل باید jpg باشد";
                        return View(t);
                    }
                }
                else
                {
                    ViewBag.Message = "حجم تصویر بیش از 512 کیلوبایت می باشد";
                    return View(t);
                }
            }



            db3.tbl_category.Add(t);
            try
            {
                db3.SaveChanges();
                ViewBag.Message = "محصول جدید مورد نظر با موفقیت ثبت گردید";

                return RedirectToAction("manage_products", "admin", new { sectionid = t.section_id });
            }
            catch (Exception ex)
            {

                ViewBag.Message = ex.Message;

                return RedirectToAction("addproduct_catg", "admin", new { sectionid = t.section_id });
            }



        }
        //[HttpPost]
        //public JavaScriptResult deleteproducts_catg(int id = 0)
        //{


        //    var qcatg = db3.tbl_category.Where(a => a.id == id).SingleOrDefault();
        //    if (qcatg.tbl_products.Count() == 0)
        //    {
        //        db3.tbl_category.Remove(qcatg);
        //        db3.SaveChanges();


        //        return JavaScript(MessageBox.Show("بخش مورد نظر با موفقیت حذف گردید", Location.topLeft, Type_me.success, Modal.WithModal) + "; setTimeout( function () { location.reload(); },3000)");
        //    }
        //    else
        //    {
        //        return JavaScript(MessageBox.Show("بخش مورد نظر غیر قابل حذف می باشد", Location.topLeft, Type_me.warning, Modal.WithModal) + "; setTimeout( function () { location.reload(); },3000)");
        //    }


        //}



        public int switchstatusproduct(int productid = 0)
        {

            var product = db3.tbl_products.Where(a => a.idproduct == productid).SingleOrDefault();

            if (product != null)
            {
                if (product.status == true)
                {
                    product.status = false;
                    db3.SaveChanges();
                    return 1;

                }
                else
                {
                    product.status = true;
                    db3.SaveChanges();
                    return 2;

                }


            }
            else
            {
                return 3;
            }


        }
        public ActionResult multichang(int[] idlist)
        {

            foreach (var item in idlist)
            {
                db3.tbl_products.Where(a => a.idproduct == item).SingleOrDefault().inventory = 10;
                db3.SaveChanges();

            }

            return RedirectToAction("detailsproducts_catg", "admin");
        }
        public ActionResult manage_productsatatusfalse(int catgid = 0)
        {

            var porductslist = db3.tbl_products.Where(a => a.categoryid == catgid && a.status == false).OrderByDescending(a => a.idproduct);
            if (porductslist.Count() != 0)
            {
                return View(porductslist);
            }
            else
            {
                return RedirectToAction("detailsproducts_catg", "admin", new { id = catgid });
            }
        }
        public ActionResult changeinventory(tbl_products t, int catgid = 0)
        {

            var product = db3.tbl_products.Where(a => a.idproduct == t.idproduct).SingleOrDefault();
            if (product != null)
            {
                product.inventory = t.inventory;
                db3.SaveChanges();
                return RedirectToAction("detailsproducts_catg", "admin", new { id = catgid });

            }
            else
            {
                return RedirectToAction("detailsproducts_catg", "admin", new { id = catgid });

            }


        }
        [HttpGet]
        public ActionResult detailsproducts_catg(int id = 0)
        {

            var qcategory = db3.tbl_category.Where(a => a.id == id).SingleOrDefault();
            ViewBag.id = qcategory.section_.id;
            ViewBag.sectionname = qcategory.section_.name;
            return View(qcategory);

        }
        [HttpGet]
        public ActionResult editproducts(int idproduct = 0)
        {


            var qproducts = db3.tbl_products.Where(a => a.idproduct == idproduct).SingleOrDefault();
            return View(qproducts);

        }
        [HttpPost]

        public ActionResult editproducts(productstructure p, IFormFile image)
        {
            string message = "";
            if (ModelState.IsValid)
            {



                try
                {

                    int pid = int.Parse(p.productid);
                    int catgid = int.Parse(p.categoryid);
                    var product = db3.tbl_products.Where(a => a.idproduct == pid).SingleOrDefault();

                    if (image != null)
                    {

                        //var qTools = db3.Tbl_Tools.OrderByDescending(a => a.ID).FirstOrDefault();

                        if (image.Length <= 524288)
                        {
                            if (image.ContentType.Contains("image"))
                            {


                                product.image = saveimageinsql.perform(image, false);
                                product.thumbnail_image = saveimageinsql.perform(image, true, 256, 256);

                            }
                            else
                            {
                                message = "فایل مورد نظر تصویر نیست.";
                                return Json(new { status = "error", message = message });
                            }
                        }
                        else
                        {
                            message = " تصویر بیش از 512 کیلوبایت است.";
                            return Json(new { status = "error", message = message });
                        }
                    }

                    decimal buycost = decimal.Parse(p.lastbuycost);
                    decimal sellcost = decimal.Parse(p.lastcellcost);
                    if (buycost != (product.lastbuycost.HasValue == true ? product.lastbuycost.Value : 0))
                    {
                        product.lastbuycost = buycost;
                        product.updatebuycost = DateTime.Now;
                    }
                    if (sellcost != (product.lastcellcost.HasValue == true ? product.lastcellcost.Value : 0))
                    {

                        product.lastcellcost = sellcost;
                        product.updatesellcost = DateTime.Now;
                    }
                    product.categoryid = catgid;
                    product.ftpid = 2;
                    product.date = DateTime.Now;



                    product.weight = decimal.Parse(p.weight);
                    product.inventory = int.Parse(p.inventory);
                    product.description = p.description;
                    product.codename = p.codename;
                    product.dimension = p.dimension;
                    product.grade = p.grade;
                    product.keywords = p.keywords;

                    product.users = int.Parse(User.Claims.FirstOrDefault().Value);
                    product.status = bool.Parse(p.status);
                    product.name = p.name.Replace("ي", "ی").Replace("ك", "ک");



                    db3.SaveChanges();
                    message = "محصول با موفقیت اصلاح گردید.";
                    string catg = db3.tbl_category.Where(a => a.id == product.categoryid).SingleOrDefault().categoryname;
                    string section = db3.tbl_category.Where(a => a.id == product.categoryid).SingleOrDefault().section_.name;
                    string newproductaddmessage = $"به اطلاع کلیه کاربران می رسد : محصولی در بخش {section} برند {catg} تحت عنوان {product.name} توسط {User.Identity.Name} هم اکنون اصلاح گردید. کد محصول {product.idproduct} ";
                    string thisdatetime = DateTime.Now.ToPersianDate() + "  " + DateTime.Now.ToString("HH:mm:ss");

                    tbl_signalRmsg msg = new tbl_signalRmsg { from_userid = product.users.Value, msg_text = newproductaddmessage, datetime_send = DateTime.Now, visibleforall = true, visibletofrom = true, visibletoto = true, msgcondition = 1 };
                    db3.tbl_signalRmsg.Add(msg);
                    db3.SaveChanges();
                    _hub.Clients.All.SendAsync("sendsystemmessgage", msg.id, product.users.Value, User.Identity.Name, newproductaddmessage, thisdatetime, msg.msgcondition);
                    var productedited = db3.tbl_products.Where(a => a.idproduct == pid).SingleOrDefault();
                    return PartialView("_partial1product", productedited);
                }
                catch (Exception ex)
                {


                    message = ex.Message;

                    return Json(new { status = "error", message = message });

                }

            }
            else
            {


                return Json(new { status = "errorvalidation", message = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToList() });

            }


        }



        [HttpGet]

        public ActionResult addproduct(int catgid = 0)
        {


            //ViewBag.categoryid = catgid;

            TempData["categoryid"] = catgid;

            return View();

        }
        [HttpPost]

        public ActionResult addproduct(productstructure p, IFormFile image)
        {
            string message = "";
            if (ModelState.IsValid)
            {



                try
                {


                    int catgid = int.Parse(p.categoryid);
                    //var qcategory = db3.tbl_category.Where(a => a.id == t.categoryid).SingleOrDefault();
                    tbl_products t = new tbl_products();
                    if (image != null)
                    {

                        //var qTools = db3.Tbl_Tools.OrderByDescending(a => a.ID).FirstOrDefault();

                        if (image.Length <= 524288)
                        {
                            if (image.ContentType.Contains("image"))
                            {

                                t.image = saveimageinsql.perform(image, false);
                                t.thumbnail_image = saveimageinsql.perform(image, true, 256, 256);

                            }
                            else
                            {
                                message = "فایل مورد نظر تصویر نیست.";
                                return Json(new { status = "error", message = message });
                            }
                        }
                        else
                        {
                            message = " تصویر بیش از 512 کیلوبایت است.";
                            return Json(new { status = "error", message = message });
                        }
                    }
                    t.lastbuycost = decimal.Parse(p.lastbuycost);
                    t.lastcellcost = decimal.Parse(p.lastcellcost);
                    t.categoryid = catgid;
                    t.ftpid = 2;
                    t.date = DateTime.Now;

                    t.updatebuycost = DateTime.Now;
                    t.updatesellcost = DateTime.Now;
                    t.weight = decimal.Parse(p.weight);
                    t.inventory = int.Parse(p.inventory);
                    t.description = p.description;
                    t.codename = p.codename;
                    t.dimension = p.dimension;
                    t.grade = p.grade;
                    t.keywords = p.keywords;

                    t.users = int.Parse(User.Claims.FirstOrDefault().Value);
                    t.status = bool.Parse(p.status);
                    t.name = p.name.Replace("ي", "ی").Replace("ك", "ک");

                    db3.tbl_products.Add(t);

                    db3.SaveChanges();
                    message = "محصول جدید با موفقیت ثبت گردید.";
                    string catg = db3.tbl_category.Where(a => a.id == t.categoryid).SingleOrDefault().categoryname;
                    string section = db3.tbl_category.Where(a => a.id == t.categoryid).SingleOrDefault().section_.name;
                    string newproductaddmessage = $"به اطلاع کلیه کاربران می رسد : محصول جدید در بخش {section} برند {catg} تحت عنوان {t.name} توسط {User.Identity.Name} هم اکنون اضافه گردید. ";
                    string thisdatetime = DateTime.Now.ToPersianDate() + "  " + DateTime.Now.ToString("HH:mm:ss");

                    tbl_signalRmsg msg = new tbl_signalRmsg { from_userid = t.users.Value, msg_text = newproductaddmessage, datetime_send = DateTime.Now, visibleforall = true, visibletofrom = true, visibletoto = true, msgcondition = 1 };
                    db3.tbl_signalRmsg.Add(msg);
                    db3.SaveChanges();

                    _hub.Clients.All.SendAsync("sendsystemmessage", msg.id, t.users.Value, User.Identity.Name, newproductaddmessage, thisdatetime, msg.msgcondition);
                    var productsofthiscatg = db3.tbl_products.Where(a => a.idproduct == t.idproduct).SingleOrDefault();
                    return PartialView("_partial1productadd", productsofthiscatg);
                }
                catch (Exception ex)
                {


                    message = ex.Message;

                    return Json(new { status = "error", message = message });

                }

            }
            else
            {


                return Json(new { status = "errorvalidation", message = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToList() });

            }
        }


        public ActionResult getcatgformthissection(int sectionid = 0)
        {

            var catg = db3.tbl_category.Where(a => a.section_id == sectionid);
            return PartialView("_partialforcatg", catg);


        }

        public ActionResult getproductinfo(int productid = 0)
        {
            var q = db3.tbl_products.Where(a => a.idproduct == productid).ToList().SingleOrDefault();
            return PartialView("_partialforproductinfo", q);

        }

        public ActionResult addproductform(int catgid = 0)

        {
            var q = db3.tbl_category.Find(catgid);
            return PartialView("_Partialforproductadd", q);

        }

        [HttpGet]
        public ActionResult detailsproducts(int idproduct = 0)
        {

            string username = User.Identity.Name;

            var q = db3.tbl_user.Where(a => a.username == username && a.Access == 0).SingleOrDefault();
            if (q == null)
            {
                return RedirectToAction("login", "home");
            }
            var qproduct = db3.tbl_products.Where(a => a.idproduct == idproduct).SingleOrDefault();

            return View(qproduct);
        }

        [HttpGet]
        public ActionResult manage_samples()
        {

            string username = User.Identity.Name;

            var q = db3.tbl_user.Where(a => a.username == username && a.Access == 0).SingleOrDefault();
            if (q == null)
            {
                return RedirectToAction("login", "home");
            }

            var qsamples = db3.tbl_files.OrderByDescending(a => a.id);
            if (qsamples != null)
            {
                return View(qsamples);
            }
            else
            {
                return View();
            }


        }


        [HttpGet]
        public ActionResult addsamplefile()
        {


            string username = User.Identity.Name;

            var q = db3.tbl_user.Where(a => a.username == username && a.Access == 0).SingleOrDefault();
            if (q == null)
            {
                return RedirectToAction("login", "home");
            }

            return View();

        }




        [HttpGet]
        public ActionResult editsamplefile(int id = 0)
        {

            string username = User.Identity.Name;

            var q = db3.tbl_user.Where(a => a.username == username && a.Access == 0).SingleOrDefault();
            if (q == null)
            {
                return RedirectToAction("login", "home");
            }

            var qfiles = db3.tbl_files.Where(a => a.id == id).SingleOrDefault();



            return View(qfiles);


        }



        [HttpGet]
        public ActionResult detailssamplefile(int id = 0)
        {

            string username = User.Identity.Name;

            var q = db3.tbl_user.Where(a => a.username == username && a.Access == 0).SingleOrDefault();
            if (q == null)
            {
                return RedirectToAction("login", "home");
            }
            var qsamples = db3.tbl_files.Where(a => a.id == id).SingleOrDefault();


            return View(qsamples);
        }






        [HttpPost]
        public JsonResult search(string text)
        {
            //text = Request.QueryString["term"];


            var catgname = db3.tbl_category.Where(a => a.categoryname.Contains(text)).Select(a => new { id = a.id, value = a.categoryname, lable = a.keywords });
            //var productname = db3.tbl_products.Where(a => a.name.Contains(text)).ToList();


            return Json(catgname);

        }

        [HttpPost]
        public JsonResult searchproduct(string text)
        {
            //text = Request.QueryString["term"];
            text = text.Replace("ي", "ی").Replace("ك", "ک");

            var productsname = db3.tbl_products.Where(a => a.name.Contains(text) || a.category.categoryname.Contains(text) || a.codename.Contains(text)).Select(a => new { id = a.idproduct, value = a.category.categoryname + " | " + a.name + " | " + (a.codename != null ? a.codename : "") + " | " + (a.dimension != null ? a.dimension : ""), lable = (a.keywords != null ? a.keywords : "") });
            //var productname = db3.tbl_products.Where(a => a.name.Contains(text)).ToList();


            return Json(productsname);

        }

        [HttpPost]
        public JsonResult searchcategory(string text)
        {
            //text = Request.QueryString["term"];


            //var categoryname = db3.tbl_category.Include(a=>a.tbl_products).Where(a => a.categoryname.Contains(text)).Select(a => new { id = a.id, value = a.categoryname, number = a.tbl_products.Count(), ave_cost = a.tbl_products.Average(x => x.lastcellcost), grade = a.tbl_products.GroupBy(h => h.grade).Select(g => new { grade = g.Key, value = g }).Count(), fgrade = a.tbl_products.GroupBy(h => h.grade).Select(g => new { grade = g.Key, value = g }).FirstOrDefault().grade });
            var categoryname = db3.tbl_category.Include(a => a.tbl_products).Where(a => a.categoryname.Contains(text)).Select(a => new
            {
                id = a.id,
                value = a.categoryname
            });


            return Json(categoryname);

        }


        public ActionResult getclass(string catgidstring)
        {
            int catgid = Int32.Parse(catgidstring);
            var q = db3.tbl_products.Where(a => a.categoryid == catgid).GroupBy(g => g.grade == null ? g.grade : g.grade.ToUpper()).Select(h => new { pgrade = h.Key, value = h });
            var pgrades = q.Select(a => new { pgrade = a.pgrade });
            return Json(pgrades);
        }
        public ActionResult getlastinfocatg(string grade, int catgid = 0)
        {
            if (grade == "null")
            {
                return Json(true);
            }
            else
            {

                var q = db3.tbl_products.Where(a => a.grade == grade && a.categoryid == catgid).FirstOrDefault();
                return Json(new { lastsellcost = q.lastcellcost.HasValue == true ? q.lastcellcost.Value : 0, lastselldate = q.updatesellcost == null ? "" : q.updatesellcost.ToPersianDate(), lastbuycost = q.lastbuycost.HasValue == true ? q.lastbuycost.Value : 0, lastbuydate = q.updatebuycost == null ? "" : q.updatebuycost.ToPersianDate() });

            }
        }

        public ActionResult getproductsofthisgrade(string grade, int catgid = 0)
        {
            if (grade == "null")
            {
                var products = db3.tbl_products.Where(a => a.grade == null && a.categoryid == catgid).Select(g => new { pname = g.name, p_id = g.idproduct, pcode = g.codename });
                return Json(products);
            }
            else
            {
                var products = db3.tbl_products.Where(a => a.grade == grade && a.categoryid == catgid).Select(g => new { pname = g.name, p_id = g.idproduct, pcode = g.codename });
                return Json(products);

            }

        }


        [HttpPost]
        public JsonResult searchdecortype(string text)
        {
            //text = Request.QueryString["term"];


            var decortype = db3.tbl_DecorType.Where(a => a.name.Contains(text)).Select(a => new { id = a.id, value = a.name });
            //var productname = db3.tbl_products.Where(a => a.name.Contains(text)).ToList();


            return Json(decortype);

        }


        [HttpPost]
        public JsonResult searchmadeby(string text)
        {
            //text = Request.QueryString["term"];


            var madeby = db3.tbl_person.Where(a => a.Fname.Contains(text) || a.Lname.Contains(text)).Select(a => new { id = a.id, value = a.Fname + " " + (a.Lname != null ? a.Lname : ""), cellphone = (a.cell != null ? a.cell : "") });


            return Json(madeby);

        }

        [HttpGet]
        public ActionResult manage_news()
        {


            string username = User.Identity.Name;

            var q = db3.tbl_user.Where(a => a.username == username && a.Access == 0).SingleOrDefault();
            if (q == null)
            {
                return RedirectToAction("login", "home");
            }

            var qnews = db3.tbl_news.OrderByDescending(a => a.id).Take(10);




            return View(qnews);


        }


        public ActionResult manage_imagefiles(int newsid = 0)
        {

            string username = User.Identity.Name;

            var q = db3.tbl_user.Where(a => a.username == username && a.Access == 0).SingleOrDefault();
            if (q == null)
            {
                return RedirectToAction("login", "home");
            }

            var qimagefiles = db3.tbl_newsimagefiles.OrderByDescending(a => a.id);

            TempData["newsid"] = newsid;
            if (qimagefiles != null)
            {

                return View(qimagefiles);
            }
            else
            {
                return View();
            }


        }

        [HttpGet]
        public ActionResult addsimagefile()
        {


            string username = User.Identity.Name;

            var q = db3.tbl_user.Where(a => a.username == username && a.Access == 0).SingleOrDefault();
            if (q == null)
            {
                return RedirectToAction("login", "home");
            }

            return View();
        }


        public ActionResult editnewsimagesfile(int id = 0)
        {


            string username = User.Identity.Name;

            var q = db3.tbl_user.Where(a => a.username == username && a.Access == 0).SingleOrDefault();
            if (q == null)
            {
                return RedirectToAction("login", "home");
            }

            var qfiles = db3.tbl_newsimagefiles.Where(a => a.id == id).SingleOrDefault();



            return View(qfiles);


        }

        public ActionResult detailsimagesfile(int id = 0)
        {


            string username = User.Identity.Name;

            var q = db3.tbl_user.Where(a => a.username == username && a.Access == 0).SingleOrDefault();
            if (q == null)
            {
                return RedirectToAction("login", "home");
            }
            var qimages = db3.tbl_newsimages.Where(a => a.newsimageid == id).OrderByDescending(a => a.id);

            return View(qimages);



        }


        [HttpGet]
        public ActionResult manage_carpenters()
        {

            var qcarpenters = db3.tbl_person;




            return View(qcarpenters);
        }
        [HttpGet]
        public ActionResult addperson()
        {


            return View();


        }

        public ActionResult addperson(tbl_person t)
        {


            db3.tbl_person.Add(t);
            try
            {
                db3.SaveChanges();
                ViewBag.Message = "شخص جدید مورد نظر با موفقیت ثبت گردید";

                return View();
            }
            catch (Exception ex)
            {

                ViewBag.Message = ex.Message;


                return View();
            }




        }

        [HttpGet]

        public ActionResult Edit_person(int id = 0)
        {

            var qperson = db3.tbl_person.Where(a => a.id == id).SingleOrDefault();



            return View(qperson);


        }
        [HttpPost]

        public ActionResult Edit_person(tbl_person t)
        {



            var qperson = db3.tbl_person.Where(a => a.id == t.id).SingleOrDefault();

            qperson.birthday = t.birthday;
            qperson.cell = t.cell;
            qperson.codemelli = t.codemelli;
            qperson.Fname = t.Fname;
            qperson.job = t.job;
            qperson.Lname = t.Lname;
            qperson.Pdescription = t.Pdescription;
            qperson.prefix = t.prefix;
            qperson.sex = t.sex;
            qperson.tell = t.tell;

            db3.SaveChanges();
            //ViewBag.message = "حساب کاربری مورد نظر با موفقیت به روز رسانی گردید";
            return RedirectToAction("manage_carpenters", "admin");
        }

        public ActionResult Details_person(int id = 0)
        {

            var qperson = db3.tbl_person.Where(a => a.id == id).SingleOrDefault();


            return View(qperson);
        }
        [HttpGet]
        public ActionResult manage_Decortype()
        {
            var qdecor = db3.tbl_DecorType;
            return View(qdecor);
        }

        [HttpGet]
        public ActionResult adddecortype()
        {



            return View();



        }

        public ActionResult adddecortype(tbl_DecorType t)
        {



            db3.tbl_DecorType.Add(t);
            try
            {
                db3.SaveChanges();
                ViewBag.Message = "طرح دکور جدید مورد نظر با موفقیت ثبت گردید";

                return View();
            }
            catch (Exception ex)
            {

                ViewBag.Message = ex.Message;


                return View();
            }

        }
        [HttpGet]
        public ActionResult Edit_decortype(int id = 0)
        {



            var qdecor = db3.tbl_DecorType.Where(a => a.id == id).SingleOrDefault();



            return View(qdecor);



        }

        [HttpPost]

        public ActionResult Edit_decortype(tbl_DecorType t)
        {



            var qdecor = db3.tbl_DecorType.Where(a => a.id == t.id).SingleOrDefault();
            qdecor.name = t.name;
            qdecor.keywords = t.keywords;



            db3.SaveChanges();
            //ViewBag.message = "حساب کاربری مورد نظر با موفقیت به روز رسانی گردید";
            return RedirectToAction("manage_Decortype", "admin");
        }


        public ActionResult manage_listkala()
        {

            var itemlist = db3.tbl_listkala97;
            ViewBag.skip = 0;
            ViewBag.take = 10;
            int take = 10;
            ViewBag.totalpages = 1;

            var productsQuery = itemlist.Select(a => new ItemListKala
            {
                id = a.id,
                sodoordate = a.sodoordate.ToPersianDate(),
                time = a.time,
                htype = a.htypeNavigation.name,
                anbarid = a.anbarid,
                anbarname = a.anbar.anbarname,
                productid = a.productid,
                category = a.product.category.categoryname,
                name = a.product.name + " " + a.product.dimension + " ::: [" + a.productid + "]",
                code = a.product.codename,
                kalanumber = a.kalanumber,
                cost = a.kalacost,
                totalcostkala = a.totalcost,
                kaladescription = a.kaladescription == null ? string.Empty : a.kaladescription,
                user = a.username.username,
                kalanumberm = a.kalanumberm
            }).OrderByDescending(a => a.id).Skip(0).Take(200);
            int number = productsQuery.Count();
            var totalPages = (int)Math.Ceiling(number / (float)take);
            ViewBag.totalpages = totalPages;
            TempData["firstpage"] = 1;
            return View(productsQuery);
        }


        public ActionResult manage_Recruitment()
        {
            var applist = db3.tbl_applicants;
            ViewBag.skip = 0;
            ViewBag.take = 10;
            int take = 10;
            //ViewBag.totalpages = 1;

            var Query = applist.Select(a => new applicantstructs
            {
                app_id = a.applicant_id,
                birthday = a.birthday.HasValue ? a.birthday.Value.ToPersianDate() : "",
                cellnumber = a.cellnumber,
                codemelli = a.codemelli,
                f_name = a.f_name,
                l_name = a.l_name,
                sabtdate = a.sabtdate.HasValue ? a.sabtdate.Value.ToPersianDate() : "",
                sex = a.sex.HasValue ? (a.sex.Value == 1 ? "مذکر" : "مونث") : "",
                Jobname = a.job_.jobtitle,


            }).OrderByDescending(a => a.app_id).Skip(0).Take(1000);
            int number = Query.Count();
            var totalPages = (int)Math.Ceiling(number / (float)take);
            ViewBag.totalpages = totalPages;
            TempData["firstpage"] = 1;
            return View(Query);
        }


        //public ActionResult get_historyappconnect(JqGridRequest request)
        //{

        //}

        //public JsonResult Subgrid(String id, JqGridRequest request)
        //{
        //    var list = db3.tbl_historyappconnect.ToList().Where(a => a.app_id == Convert.ToInt32(id)).Select(g => new
        //    {
        //        id = g.id,
        //        app_id = g.app_id,
        //        howtoconnect = g.howtoconnect,
        //        user = g.user.username,
        //        sabtdate = g.sabtdate.HasValue == true ? g.sabtdate.Value.ToPersianDate() : "",
        //        //sabtdate = shamsi.ToShamsinull(g.sabtdate),
        //        time = g.sabtdate.HasValue == true ? g.sabtdate.Value.ToString("HH:mm:ss") : "",
        //        result = g.result,


        //    });

        //    var pageIndex = request.page - 1;
        //    var pageSize = request.rows;
        //    var totalRecords = list.Count();
        //    var totalPages = (int)Math.Ceiling(totalRecords / (float)pageSize);

        //    var historylist = new JqGridData
        //    {
        //        Total = totalPages,
        //        Page = request.page,
        //        Records = totalRecords,
        //        Rows = (list.Select(g => new JqGridRowData
        //        {
        //            Id = g.id,
        //            RowCells = new List<string>
        //                       {


        //                           g.id.ToString(),
        //                           g.app_id.ToString(),
        //                            g.howtoconnect,
        //                            g.user,
        //                            g.sabtdate.ToString(),
        //                            g.time,
        //                            g.result
        //                        }
        //        })).ToArray()
        //    };
        //    return Json(historylist);

        //}

        public ActionResult edithistoryappconnect(tbl_historyappconnect t)
        {

            string username = User.Identity.Name;


            var q = db3.tbl_user.Where(a => a.username == username && (a.Access == 0 || a.Access == 1)).SingleOrDefault();

            var usernameid = db3.tbl_user.Where(a => a.username == username).SingleOrDefault();




            if (t == null)
                return Json(false);

            var g2 = db3.tbl_historyappconnect.Where(a => a.id == t.id).SingleOrDefault();
            g2.sabtdate = DateTime.Now;


            g2.userid = usernameid.id;


            g2.howtoconnect = t.howtoconnect;
            g2.result = t.result;
            try
            {

                db3.SaveChanges();

                return Json(true);
            }
            catch (Exception)
            {

                return Json(false);
            }
        }

        public ActionResult deletehistoryappconnect(tbl_historyappconnect t)
        {

            string username = User.Identity.Name;


            var q = db3.tbl_user.Where(a => a.username == username && (a.Access == 0 || a.Access == 1)).SingleOrDefault();

            var usernameid = db3.tbl_user.Where(a => a.username == username).SingleOrDefault();




            if (t == null)
                return Json(false);

            var g2 = db3.tbl_historyappconnect.Where(a => a.id == t.id).SingleOrDefault();
            db3.tbl_historyappconnect.Remove(g2);

            try
            {

                db3.SaveChanges();

                return Json(true);
            }
            catch (Exception)
            {

                return Json(false);
            }
        }


        [HttpPost]
        public ActionResult addhistoryappconnect(string pid, tbl_historyappconnect t)
        {
            //todo: Add product to repository

            string username = User.Identity.Name;


            var q = db3.tbl_user.Where(a => a.username == username && (a.Access == 0 || a.Access == 1)).SingleOrDefault();
            //if (q == null)
            //{
            //    return RedirectToAction("manage_Recr", "admin");
            //}
            var usernameid = db3.tbl_user.Where(a => a.username == username).SingleOrDefault();




            if (t == null)
                return Json(false);


            t.sabtdate = DateTime.Now;
            t.app_id = Convert.ToInt32(pid);

            t.userid = usernameid.id;


            db3.tbl_historyappconnect.Add(t);

            db3.SaveChanges();
            return Json(true);
        }





        //[HttpGet]

        //public ActionResult get_recruitment(JqGridRequest request, string hiddenColumns)
        //{

        //    //

        //    var list = db3.tbl_applicants.ToList().Select(g => new
        //    {
        //        id = g.applicant_id,
        //        fname = g.f_name,
        //        lname = g.l_name,
        //        job = g.job_id.HasValue == true ? g.job_.jobtitle : "",
        //        codemelli = g.codemelli,
        //        cellnumber = g.cellnumber,
        //        sex = g.sex.HasValue == true ? g.sex.Value == 1 ? "مذکر" : "مونث" : "",



        //        birthday = g.birthday.HasValue == true ? g.birthday.Value.ToPersianDate() : "",
        //        //sabtdate = shamsi.ToShamsinull(g.sabtdate),
        //        sabtdate = g.sabtdate.HasValue == true ? g.sabtdate.Value.ToPersianDate() : ""


        //    });


        //    var pageIndex = request.page - 1;
        //    var pageSize = request.rows;
        //    var totalRecords = list.Count();
        //    var totalPages = (int)Math.Ceiling(totalRecords / (float)pageSize);



        //    var productsQuery = new JqGridSearch(request, this.Request.Form, DateTimeType.Persian).ApplyFilter(list.AsQueryable());

        //    var productsList = productsQuery.OrderBy(request.sidx + " " + request.sord)
        //                       .Skip(pageIndex * pageSize)
        //                       .Take(pageSize)
        //                       .ToList();

        //    return Json(productsList);
        //}


        public ActionResult getmoreappinfo(int id)
        {
            var q = db3.tbl_skillappinfo.Where(a => a.appid == id).Select(g => g);
            ViewBag.fullname = db3.tbl_applicants.Where(a => a.applicant_id == id).SingleOrDefault().f_name + " " + db3.tbl_applicants.Where(a => a.applicant_id == id).SingleOrDefault().l_name;
            return View(q);
        }


        [HttpGet]
        public ActionResult Details_havale(int id = 0)
        {

            string username = User.Identity.Name;

            var q = db3.tbl_user.Where(a => a.username == username && (a.Access == 0 || a.Access == 1)).SingleOrDefault();
            if (q == null)
            {
                return RedirectToAction("login", "home");
            }


            return View();
        }


        [HttpGet]
        public ActionResult additem()
        {


            string username = User.Identity.Name;

            var q = db3.tbl_user.Where(a => a.username == username && (a.Access == 0 || a.Access == 1)).SingleOrDefault();
            if (q == null)
            {
                return RedirectToAction("login", "home");
            }

            return View();

        }
        [HttpGet]
        public ActionResult addnewitemin()
        {

            string username = User.Identity.Name;

            var q = db3.tbl_user.Where(a => a.username == username && (a.Access == 0 || a.Access == 1)).SingleOrDefault();
            if (q == null)
            {
                return RedirectToAction("login", "home");
            }



            return PartialView();
        }
        [HttpPost]
        public ActionResult addnewitemin(tbl_listkala t)
        {

            string username = User.Identity.Name;


            var q = db3.tbl_user.Where(a => a.username == username && (a.Access == 0 || a.Access == 1)).SingleOrDefault();
            if (q == null)
            {
                return RedirectToAction("manage_listkala", "admin");
            }
            var usernameid = db3.tbl_user.Where(a => a.username == username).SingleOrDefault();

            t.totalcost = t.kalacost * t.kalanumber;
            t.sodoordate = shamsi.ToMiladi(t.sodoordate);
            t.usernameid = usernameid.id;
            t.htype = 1;

            db3.tbl_listkala.Add(t);
            try
            {
                db3.SaveChanges();
                //ViewBag.Message = "حساب کاربر مورد نظر با موفقیت ثبت گردید";

                return RedirectToAction("manage_listkala", "admin");
            }
            catch (Exception ex)
            {

                //ViewBag.Message = ex.Message;

                return RedirectToAction("manage_listkala", "admin");
            }



        }

        [HttpGet]
        public ActionResult addnewitemout()
        {


            string username = User.Identity.Name;

            var q = db3.tbl_user.Where(a => a.username == username && (a.Access == 0 || a.Access == 1)).SingleOrDefault();
            if (q == null)
            {
                return RedirectToAction("login", "home");
            }



            return PartialView();
        }


        [HttpPost]
        public ActionResult addnewitemout(tbl_listkala t)
        {

            string username = User.Identity.Name;


            var q = db3.tbl_user.Where(a => a.username == username && (a.Access == 0 || a.Access == 1)).SingleOrDefault();
            if (q == null)
            {
                return RedirectToAction("manage_listkala", "admin");
            }
            var usernameid = db3.tbl_user.Where(a => a.username == username).SingleOrDefault();

            t.totalcost = t.kalacost * t.kalanumber;
            t.sodoordate = shamsi.ToMiladi(t.sodoordate);
            t.usernameid = usernameid.id;
            t.htype = -1;
            t.kalanumber = -1 * t.kalanumber;
            db3.tbl_listkala.Add(t);
            try
            {
                db3.SaveChanges();

                return RedirectToAction("manage_listkala", "admin");
            }
            catch (Exception ex)
            {

                //ViewBag.Message = ex.Message;

                return RedirectToAction("manage_listkala", "admin");
            }



        }



        [HttpGet]
        public ActionResult edititem(int id = 0)
        {

            string username = User.Identity.Name;

            var q = db3.tbl_user.Where(a => a.username == username && (a.Access == 0 || a.Access == 1)).SingleOrDefault();
            if (q == null)
            {
                return RedirectToAction("login", "home");
            }

            var qlist = db3.tbl_listkala.Where(a => a.id == id).SingleOrDefault();
            return PartialView(qlist);

        }
        [HttpPost]
        public ActionResult edititem(tbl_listkala t)
        {

            string username = User.Identity.Name;


            var q = db3.tbl_user.Where(a => a.username == username && (a.Access == 0 || a.Access == 1)).SingleOrDefault();
            if (q == null)
            {
                return RedirectToAction("manage_listkala", "admin");
            }
            var qitem = db3.tbl_listkala.Where(a => a.id == t.id).SingleOrDefault();
            var usernameid = db3.tbl_user.Where(a => a.username == username).SingleOrDefault();
            qitem.kalacost = t.kalacost;
            qitem.kaladescription = t.kaladescription;
            qitem.kalanumber = t.kalanumber;
            qitem.productid = t.productid;
            qitem.sodoordate = shamsi.ToMiladi(t.sodoordate);
            qitem.usernameid = usernameid.id;



            db3.SaveChanges();


            return RedirectToAction("manage_listkala", "admin");


        }



        [HttpGet]
        public ActionResult detaileitem(int id = 0)
        {

            string username = User.Identity.Name;

            var q = db3.tbl_user.Where(a => a.username == username && (a.Access == 0 || a.Access == 1)).SingleOrDefault();
            if (q == null)
            {
                return RedirectToAction("login", "home");
            }

            var qlist = db3.tbl_listkala.Where(a => a.id == id).SingleOrDefault();
            return PartialView(qlist);

        }
        // method


        // قسمت JQgrid  مدیریت انبار تحت وب



        //
        public ActionResult Mlistkala()
        {

            var itemlist = db3.tbl_listkala97;
            ViewBag.skip = 0;
            ViewBag.take = 10;
            int take = 10;
            ViewBag.totalpages = 1;

            var productsQuery = itemlist.Select(a => new ItemListKala
            {
                id = a.id,
                sodoordate = a.sodoordate.ToPersianDate(),
                time = a.time,
                htype = a.htypeNavigation.name,
                anbarid = a.anbarid,
                anbarname = a.anbar.anbarname,
                productid = a.productid,
                category = a.product.category.categoryname,
                name = a.product.name + " " + a.product.dimension + " ::: [" + a.productid + "]",
                code = a.product.codename,
                kalanumber = a.kalanumber,
                cost = a.kalacost,
                totalcostkala = a.totalcost,
                kaladescription = a.kaladescription == null ? string.Empty : a.kaladescription,
                user = a.username.username,
                kalanumberm = a.kalanumberm
            }).OrderByDescending(a => a.id).Skip(0).Take(200);
            int number = productsQuery.Count();
            var totalPages = (int)Math.Ceiling(number / (float)take);
            ViewBag.totalpages = totalPages;
            TempData["firstpage"] = 1;
            return View(productsQuery);

        }







        //[HttpGet]
        //public ActionResult GetProducts(JqGridRequest request, string hiddenColumns)
        //{


        //    var list = db3.tbl_listkala.ToList();
        //    int running_total = 0;






        //    var pageIndex = request.page - 1;
        //    var pageSize = request.rows;
        //    var totalRecords = list.Count;
        //    var totalPages = (int)Math.Ceiling(totalRecords / (float)pageSize);


        //    var productsQuery = list.AsQueryable().Select(a => new { id = a.id, sodoordate = a.sodoordate.ToPersianDate(), time = a.time, htype = a.htypeNavigation.name, anbarid = a.anbarid, anbarname = a.anbar.anbarname, productid = a.productid, category = a.product.category.categoryname, name = a.product.name + " " + a.product.dimension + " ::: [" + a.productid + "]", code = a.product.codename, kalanumber = a.kalanumber, cost = a.kalacost == null ? '0' : a.kalacost, totalcostkala = a.totalcost == null ? '0' : a.totalcost, kaladescription = a.kaladescription == null ? string.Empty : a.kaladescription, user = a.username.username, kalanumberm = a.kalanumberm });

        //    NameValueCollection n = new NameValueCollection();
        //    n = (NameValueCollection)this.Request.Form;

        //    JqGridSearch j = new JqGridSearch(request,n, DateTimeType.Persian);
        //    var productsQuery1 = j.ApplyFilter(productsQuery);

        //    //productsQuery = new JqGridSearch(request, DateTimeType.Persian).ApplyFilter(productsQuery);

        //    var productsList01 = productsQuery1.OrderBy(request.sidx + " " + request.sord)
        //                                    .Skip(pageIndex * pageSize)
        //                                    .Take(pageSize)
        //                                    .ToList();

        //    var productsList02 = (productsList01.OrderBy(i => i.id).Select(i =>
        //    {
        //        running_total += i.kalanumberm;
        //        return new
        //        {
        //            i.id,
        //            i.productid,
        //            i.cost,
        //            RunningTotal = running_total,
        //            i.kaladescription,
        //            i.kalanumber,
        //            i.sodoordate,
        //            i.time,
        //            htype = i.htype,
        //            i.totalcostkala,
        //            i.kalanumberm,
        //            i.anbarid,
        //            anbarname = i.anbarname,
        //            category = i.category,
        //            name = i.name,
        //            code = i.code,
        //            user = i.user

        //        };

        //    })).ToList();




        //    // to export pdf:




        //    //==========================================================================



        //    var productsData = new JqGridData
        //    {
        //        Total = totalPages,
        //        Page = request.page,
        //        Records = totalRecords,
        //        Rows = (productsList02.Select(product => new JqGridRowData
        //        {
        //            Id = product.id,
        //            RowCells = new List<string>
        //                       {

        //                           product.id.ToString(CultureInfo.InvariantCulture),
        //                           product.sodoordate,
        //                           product.time,
        //                           product.htype ,
        //                            product.anbarid.ToString(CultureInfo.InvariantCulture),
        //                            product.anbarname,
        //                             product.productid.ToString(CultureInfo.InvariantCulture),
        //                             product.category,
        //                          product.name  ,
        //                          product.code,
        //                             product.kalanumber.ToString(CultureInfo.InvariantCulture),
        //                              product.RunningTotal.ToString(CultureInfo.InvariantCulture),
        //                           product.cost.ToString(CultureInfo.InvariantCulture),
        //                           product.totalcostkala.ToString(CultureInfo.InvariantCulture),
        //                             product.kaladescription==null ? string.Empty : product.kaladescription,
        //                               product.user,
        //                               product.kalanumberm.ToString(CultureInfo.InvariantCulture),


        //                        }
        //        })).ToArray()
        //    };
        //    return Json(productsData);
        //}






        [HttpPost]
        public ActionResult DeleteProduct(int id)
        {

            var qitem = db3.tbl_listkala.Where(a => a.id == id).SingleOrDefault();
            //todo: Delete product

            if (qitem == null)
                return Json(false);

            db3.tbl_listkala.Remove(qitem);
            db3.SaveChanges();


            return Json(true);
        }

        [HttpPost]
        public ActionResult addnewitem(tbl_listkala t)
        {
            //todo: Add product to repository

            string username = User.Identity.Name;


            var q = db3.tbl_user.Where(a => a.username == username && (a.Access == 0 || a.Access == 1)).SingleOrDefault();
            if (q == null)
            {
                return RedirectToAction("manage_listkala", "admin");
            }
            var usernameid = db3.tbl_user.Where(a => a.username == username).SingleOrDefault();




            if (t == null)
                return Json(false);

            if (t.htype == 1)
            {
                t.kalanumberm = t.kalanumber;

            }
            if (t.htype == -1)
            {
                t.kalanumberm = -1 * t.kalanumber;
            }
            t.totalcost = t.kalacost * t.kalanumberm;
            t.sodoordate = t.sodoordate;
            t.time = DateTime.Now.ToString("HH:mm");
            t.usernameid = usernameid.id;


            db3.tbl_listkala.Add(t);

            db3.SaveChanges();
            return Json(true);
        }

        [HttpPost]
        public ActionResult EditProduct(tbl_listkala t)
        {

            //todo: Edit product based on postData

            string username = User.Identity.Name;

            var q = db3.tbl_user.Where(a => a.username == username && (a.Access == 0 || a.Access == 1)).SingleOrDefault();
            if (q == null)
            {
                return RedirectToAction("manage_listkala", "admin");
            }
            var usernameid = db3.tbl_user.Where(a => a.username == username).SingleOrDefault();





            var qitem = db3.tbl_listkala.Where(a => a.id == t.id).SingleOrDefault();
            //todo: Delete product

            if (qitem == null)
                return Json(false);
            qitem.productid = t.productid;
            qitem.htype = t.htype;
            if (t.htype == 1)
            {
                qitem.kalanumberm = t.kalanumber;
                qitem.totalcost = t.kalacost * t.kalanumber;
            }
            if (t.htype == -1)
            {
                qitem.kalanumberm = -1 * t.kalanumber;
                qitem.totalcost = -1 * t.kalacost * t.kalanumber;
            }
            qitem.kalanumber = t.kalanumber;
            qitem.sodoordate = t.sodoordate;
            qitem.kaladescription = t.kaladescription;
            qitem.kalacost = t.kalacost;
            qitem.anbarid = t.anbarid;

            qitem.usernameid = usernameid.id;

            db3.SaveChanges();

            return Json(true);

            //todo: Edit product based on postData

            //return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult manage_entity()
        {

            string username = User.Identity.Name;

            var q = db3.tbl_user.Where(a => a.username == username && (a.Access == 0 || a.Access == 1)).SingleOrDefault();
            if (q == null)
            {
                return RedirectToAction("login", "home");
            }


            return View();

        }

        static int getkalavalues(int productid, int anbarid)
        {
            _4820_soltaniwebContext db = new _4820_soltaniwebContext();
            int totalnumber;
            int inrecordnumber, runningnumber, diffnumber;
            runningnumber = 0;
            int result = 0;

            totalnumber = db.tbl_listkala.Where(a => a.productid == productid && a.anbarid == anbarid).Sum(a => a.kalanumberm);
            inrecordnumber = db.tbl_listkala.Where(a => a.productid == productid && a.anbarid == anbarid && a.htype == 1).Count();
            var inrecords = db.tbl_listkala.Where(a => a.productid == productid && a.anbarid == anbarid && a.htype == 1).OrderByDescending(a => a.sodoordate).ToList();
            if (inrecordnumber > 0)
            {
                foreach (var item in inrecords)
                {
                    runningnumber = runningnumber + item.kalanumber;
                    diffnumber = totalnumber - runningnumber;
                    if (diffnumber >= 0)
                    {
                        result = result + (item.kalanumber * (int)item.kalacost);
                    }
                    else
                    {
                        result = result + ((item.kalanumber + diffnumber) * (int)item.kalacost);
                        break;
                    }

                }



            }
            else
            {
                result = 0;
            }

            return result;
        }


        //[HttpGet]
        //public ActionResult get_entity(JqGridRequest request, string hiddenColumns)
        //{





        //    var lastprice = db3.tbl_listkala.OrderByDescending(a => a.sodoordate).ToList().GroupBy(a => new { a.productid, a.anbar.anbarname, a.product.name, a.anbarid }).Select(g2 => new
        //    {
        //        productid = g2.Key.productid,
        //        productname = g2.Key.name,
        //        anbar = g2.Key.anbarname,

        //        lastprice = g2.OrderByDescending(b => b.sodoordate).Where(b => b.htype == 1).Count() == 0 ? 0 : g2.OrderByDescending(b => b.sodoordate).Where(b => b.htype == 1).FirstOrDefault().kalacost,

        //        lastdate = g2.OrderByDescending(b => b.sodoordate).Where(b => b.htype == 1).Count() == 0 ? " " : g2.OrderByDescending(b => b.sodoordate).Where(b => b.htype == 1).FirstOrDefault().sodoordate.ToPersianDate(),

        //    }).ToList();

        //    //

        //    var lastsellprice = db3.tbl_listkala.OrderByDescending(a => a.sodoordate).ToList().GroupBy(a => new { a.productid, a.anbar.anbarname, a.product.name, a.anbarid }).Select(g2 => new
        //    {

        //        productid = g2.Key.productid,
        //        productname = g2.Key.name,
        //        anbar = g2.Key.anbarname,
        //        lastsellprice = g2.OrderByDescending(b => b.sodoordate).Where(b => b.htype == -1).Count() == 0 ? 0 : g2.OrderByDescending(b => b.sodoordate).Where(b => b.htype == -1).FirstOrDefault().kalacost,
        //        lastdatesell = g2.OrderByDescending(b => b.sodoordate).Where(b => b.htype == -1).Count() == 0 ? " " : g2.OrderByDescending(b => b.sodoordate).Where(b => b.htype == -1).FirstOrDefault().sodoordate.ToPersianDate()

        //    }).ToList();


        //    //

        //    var list01 = (from b in db3.tbl_listkala

        //                  group b by new
        //                  {

        //                      b.productid,
        //                      b.product.name,
        //                      b.product.codename,
        //                      b.product.dimension,

        //                      b.product.category.categoryname,
        //                      b.anbar.anbarname,
        //                      b.product.lastcellcost,
        //                      b.product.inventory,





        //                  } into g
        //                  select new
        //                  {
        //                      productid = g.Key.productid,
        //                      category = g.Key.categoryname,
        //                      anbar = g.Key.anbarname,
        //                      productname = g.Key.name + " کد : " + g.Key.codename + " | " + g.Key.dimension,
        //                      codename = g.Key.codename,
        //                      sellcost = g.Key.lastcellcost,
        //                      maxiventory = g.Key.inventory,
        //                      Totalentity = g.Sum(item => item.kalanumberm),



        //                  }).ToList();


        //    //


        //    var list = (from b in list01
        //                join item in lastprice on new { b.productid, b.anbar } equals new { item.productid, item.anbar }
        //                join item2 in lastsellprice on new { b.productid, b.anbar } equals new { item2.productid, item2.anbar }

        //                group b by new
        //                {

        //                    b.productid,
        //                    b.category,
        //                    b.productname,
        //                    b.codename,
        //                    b.anbar,
        //                    b.sellcost,
        //                    b.maxiventory,
        //                    b.Totalentity,
        //                    item.lastprice,
        //                    //item.kalavalues
        //                    item.lastdate,
        //                    item2.lastsellprice,
        //                    item2.lastdatesell


        //                } into g
        //                select new
        //                {
        //                    productid = g.Key.productid,
        //                    category = g.Key.category,
        //                    anbar = g.Key.anbar,
        //                    productname = g.Key.productname,
        //                    codename = g.Key.codename,
        //                    sellcost = g.Key.sellcost,
        //                    maxiventory = g.Key.maxiventory,
        //                    Totalentity = g.Key.Totalentity,
        //                    lastbuyprice = g.Key.lastprice,
        //                    //kalavalues = g.Key.kalavalues
        //                    lastdate = g.Key.lastdate,
        //                    lastsellprice = g.Key.lastsellprice,
        //                    lastdatesell = g.Key.lastdatesell

        //                }).ToList();



        //    //

        //    //






        //    var pageIndex = request.page - 1;
        //    var pageSize = request.rows;
        //    var totalRecords = list.Count();
        //    var totalPages = (int)Math.Ceiling(totalRecords / (float)pageSize);


        //    var productsQuery = list.AsQueryable().Select(a => new { id = a.productid, category = a.category, anbar = a.anbar, name = a.productname, codename = a.codename, sellcost = a.sellcost, Totalentity = a.Totalentity, inventory = a.maxiventory, lastbuyprice = a.lastbuyprice, lastdate = a.lastdate, lastsellprice = a.lastsellprice, lastdatesell = a.lastdatesell });

        //    productsQuery = new JqGridSearch(request, this.Request.Form, DateTimeType.Persian).ApplyFilter(productsQuery);






        //    if (string.IsNullOrWhiteSpace(request.oper))
        //    {
        //        productsQuery = productsQuery.OrderBy(request.sidx + " " + request.sord)
        //                            .Skip(pageIndex * pageSize)
        //                            .Take(pageSize);
        //    }
        //    else if (request.oper == "excel")
        //    {
        //        productsQuery = productsQuery
        //                            .Skip(pageIndex * pageSize);
        //    }


        //    var productsList = productsQuery.OrderBy(request.sidx + " " + request.sord)
        //                                    .Skip(pageIndex * pageSize)
        //                                    .Take(pageSize)
        //                                    .ToList();

        //    List<productent> datatopdf = new List<productent>();
        //    foreach (var item in productsList)
        //    {
        //        datatopdf.Add(new productent() { id = item.id, anbar = item.anbar, category = item.category, codename = item.codename, inventory = item.inventory, lastbuyprice = item.lastbuyprice, name = item.name, sellcost = item.sellcost, Totalentity = item.Totalentity, lastdate = item.lastdate, lastsellprice = item.lastsellprice, lastselldate = item.lastdatesell });

        //    }





        //    if (!string.IsNullOrWhiteSpace(request.oper) && request.oper == "excel")
        //    {
        //        new ProductsPdfReport().CreatePdfReport(datatopdf, hiddenColumns == null ? new string[] { } : hiddenColumns.Split(','));
        //    }

        //    var productsData = new JqGridData
        //    {
        //        Total = totalPages,
        //        Page = request.page,
        //        Records = totalRecords,
        //        Rows = (productsList.Select(product => new JqGridRowData
        //        {
        //            Id = product.id,
        //            RowCells = new List<string>
        //                       {

        //                           product.id.ToString(CultureInfo.InvariantCulture),
        //                            product.category ,
        //                            product.name,
        //                            product.codename,
        //                            product.anbar,
        //                            product.sellcost.HasValue == true ? product.sellcost.ToString() : "0",
        //                            product.Totalentity.ToString(),
        //                            product.inventory.HasValue == true ? product.inventory.ToString() : "0",

        //                            product.lastbuyprice.ToString(),
        //                            //product.kalavalues.ToString()
        //                            product.lastdate,
        //                            product.lastsellprice.ToString(),
        //                            product.lastdatesell,





        //                        }
        //        })).ToArray()
        //    };
        //    return Json(productsData);
        //}



        public ActionResult catg_Select()
        {

            var list = db3.tbl_category;




            var catg = list.Select(x => new SelectListItem
            {
                Text = x.categoryname,
                Value = x.categoryname,
            }).ToList();


            return PartialView("catg_Select", catg);
        }


        public ActionResult htype_Select()
        {

            var list = db3.tbl_havaletype;




            var catg = list.Select(x => new SelectListItem
            {
                Text = x.name,
                Value = x.name,
            }).ToList();


            return PartialView("htype_Select", catg);

        }
        public ActionResult anbar_Select()
        {

            var list = db3.tbl_anbar;




            var catg = list.Select(x => new SelectListItem
            {
                Text = x.anbarname,
                Value = x.anbarname,
            }).ToList();


            return PartialView("anbar_Select", catg);
        }

        public ActionResult ismarried()
        {

            return PartialView("ismarried");
        }


        public ActionResult whichsex()
        {

            return PartialView("sex");
        }
        public ActionResult howexcell()
        {

            return PartialView("howexcell");
        }

        [HttpPost]
        public JsonResult searchanbar(string text)
        {
            //text = Request.QueryString["term"];


            var anbarname = db3.tbl_anbar.Where(a => a.anbarname.Contains(text)).Select(a => new { id = a.id, value = a.anbarname });
            //var productname = db3.tbl_products.Where(a => a.name.Contains(text)).ToList();


            return Json(anbarname);

        }



        public ActionResult reporttoorder()
        {

            string username = User.Identity.Name;

            var q = db3.tbl_user.Where(a => a.username == username && (a.Access == 0 || a.Access == 1)).SingleOrDefault();
            if (q == null)
            {
                return RedirectToAction("login", "home");
            }


            return View();

        }


        //[HttpGet]
        //public ActionResult get_order(JqGridRequest request)
        //{



        //    var list = (from b in db3.tbl_listkala

        //                group b by new
        //                {

        //                    b.productid,
        //                    b.product.name,
        //                    b.product.codename,
        //                    b.product.dimension,
        //                    b.product.category.categoryname,
        //                    b.product.inventory,





        //                } into g

        //                select new
        //                {
        //                    productid = g.Key.productid,
        //                    category = g.Key.categoryname,
        //                    productname = g.Key.categoryname + " | " + g.Key.name + " کد : " + g.Key.codename + " | " + g.Key.dimension,
        //                    codename = g.Key.codename,
        //                    maxiventory = g.Key.inventory,
        //                    Totalentity = g.Sum(item => item.kalanumberm),



        //                }).Where(a => a.maxiventory > a.Totalentity).ToList();


        //    //



        //    var pageIndex = request.page - 1;
        //    var pageSize = request.rows;
        //    var totalRecords = list.Count;
        //    var totalPages = (int)Math.Ceiling(totalRecords / (float)pageSize);


        //    var productsQuery = list.AsQueryable().Select(a => new { id = a.productid, category = a.category, name = a.productname, codename = a.codename, Totalentity = a.Totalentity, inventory = a.maxiventory });

        //    productsQuery = new JqGridSearch(request, this.Request.Form, DateTimeType.Persian).ApplyFilter(productsQuery);

        //    var productsList = productsQuery.AsQueryable().OrderBy(request.sidx + " " + request.sord)
        //                                    .Skip(pageIndex * pageSize)
        //                                    .Take(pageSize)
        //                                    .ToList();

        //    var productsData = new JqGridData
        //    {
        //        Total = totalPages,
        //        Page = request.page,
        //        Records = totalRecords,
        //        Rows = (productsList.Select(product => new JqGridRowData
        //        {
        //            Id = product.id,
        //            RowCells = new List<string>
        //                       {

        //                           product.id.ToString(CultureInfo.InvariantCulture),
        //                            product.category ,
        //                            product.name,
        //                            product.codename,
        //                            product.Totalentity.ToString(),
        //                            product.inventory.HasValue == true ? product.inventory.ToString() : "0",





        //                        }
        //        })).ToArray()
        //    };
        //    return Json(productsData);
        //}





        public ActionResult lowrateproductsreport()
        {

            string username = User.Identity.Name;

            var q = db3.tbl_user.Where(a => a.username == username && (a.Access == 0 || a.Access == 1)).SingleOrDefault();
            if (q == null)
            {
                return RedirectToAction("login", "home");
            }


            return View();

        }


        static bool fourmounthnosell(int id)
        {
            _4820_soltaniwebContext db = new _4820_soltaniwebContext();
            var listkala = db.tbl_listkala.Where(a => a.productid == id && a.htype == 1).ToList();
            if (listkala.Count() == 0)
            {
                return false;
            }
            else
            {
                if (listkala.OrderByDescending(d => d.sodoordate).FirstOrDefault().sodoordate < DateTime.Now.AddMonths(-4))
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }






        }



        public ActionResult updatecostbygrade(string sellcost, string buycost, string grade, int catgid = 0)
        {
            if (grade == null)
            {
                return Json(new { messageid = "0", messagetext = "قیمت این کلاس قابل تغییر نمی باشد." });
            }
            else
            {
                var q = db3.tbl_products.Where(a => a.grade == grade && a.categoryid == catgid);
                foreach (var item in q)
                {
                    if (item.lastbuycost != Convert.ToDecimal(buycost))
                    {
                        item.lastbuycost = Convert.ToDecimal(buycost);
                        item.updatebuycost = DateTime.Now;

                    }
                    if (item.lastcellcost != Convert.ToDecimal(sellcost))
                    {

                        item.lastcellcost = Convert.ToDecimal(sellcost);
                        item.updatesellcost = DateTime.Now;
                    }
                }
                db3.SaveChanges();
                return Json(new { messageid = "1", messagetext = "قیمت ها با موفقیت به روز رسانی گردید" });
            }
        }

        [HttpGet]
        public ActionResult manage_cost(tbl_products t)
        {

            string username = HttpContext.User.Identity.Name;



            var qproductsingroups = db3.tbl_products.Where(a => a.categoryid == t.categoryid);





            return View(qproductsingroups);

        }

        public ActionResult insertpricetable(tbl_products t)
        {
            var qproductsingroups = db3.tbl_products.Where(a => a.categoryid == t.categoryid).Select(g => g);
            return PartialView("_pricetable", qproductsingroups);
        }

        public ActionResult lastnewproducts(int number = 10, int skip = 0)
        {

            string username = User.Identity.Name;

            var q = db3.tbl_user.Where(a => a.username == username && a.Access == 0).SingleOrDefault();
            if (q == null)
            {
                return RedirectToAction("login", "home");
            }

            var lastproducts = db3.tbl_products.OrderByDescending(a => a.idproduct).Skip(skip).Take(number);
            return View(lastproducts);


        }

        public ActionResult manage_slides()
        {
            var q = db3.tbl_slides;



            return View(q);
        }

        [HttpPost]
        public ActionResult uploadslideimage(tbl_slides t, IFormFile img)
        {

            if (img != null)
            {


                t.imge = saveimageinsql.perform(img, false);
                t.thumbnail_image = saveimageinsql.perform(img, true, 400, 200);
                t.nameimage = img.FileName;
                t.ftpid = 3;
                t.show = false;
                db3.tbl_slides.Add(t);
                db3.SaveChanges();
                ViewBag.result = "عملیات با موفقیت انجام شد";

            }
            else
            {
                ViewBag.result = "تصویری انتخاب نشده است";
            }
            var q = db3.tbl_slides.Where(a => a.slideID == t.slideID).Select(a => new { id = a.slideID, name = a.nameslide, first = a.first, image = a.nameimage, timage = a.thumbnail_image });
            var result = JsonConvert.SerializeObject(q);
            return Json(result);
        }

        public string selectfirstslide(int slideid = 0)
        {
            string result;
            result = db3.tbl_slides.Where(a => a.slideID == slideid).SingleOrDefault().nameimage;
            return result;

        }
        [HttpPost]
        public ActionResult applychangein1stimg(int slideid = 0)
        {
            db3.tbl_slides.ToList().ForEach(a =>
            {
                if (a.slideID == slideid)
                {
                    a.show = false;
                    a.first = true;
                }
                else
                {
                    a.first = false;
                }
            });
            db3.SaveChanges();
            var slides = db3.tbl_slides.Select(a => new { id = a.slideID, name = a.nameslide });

            return Json(slides);

        }


        public string showorhidenslide(int id = 0)
        {
            var q = db3.tbl_slides.Where(a => a.slideID == id).SingleOrDefault();
            if (q.show == true)
            {
                q.show = false;
            }
            else
            {
                q.show = true;
            }
            return q.show.ToString();
        }


        public ActionResult getslideinfo(int slideid = 0)
        {
            var slideinfo = db3.tbl_slides.Where(a => a.slideID == slideid).Select(a => new { id = a.slideID, name = a.nameslide, rank = a.rank, show = a.show });

            return Json(slideinfo);

        }
        [HttpPost]
        public ActionResult changeslide(int slideid, string nameslide, Nullable<int> rank, bool show)
        {
            var currentslide = db3.tbl_slides.Where(a => a.slideID == slideid).SingleOrDefault();
            currentslide.nameslide = nameslide;
            currentslide.show = show;
            currentslide.rank = rank;
            try
            {
                db3.SaveChanges();
                var q = db3.tbl_slides.Where(a => a.slideID == slideid).Select(a => new { id = a.slideID, name = a.nameslide, rank = a.rank, show = a.show });

                return Json(q);

            }
            catch (Exception)
            {

                throw;
            }




        }


        public int deleteslide(int slideid = 0)
        {
            var qslide = db3.tbl_slides.Where(a => a.slideID == slideid).SingleOrDefault();


            DeleteParametr d = new DeleteParametr();
            d.FtpID = (int)qslide.ftpid;
            d.FileName = qslide.nameimage;

            if (Ftp.DeleteWithCkeck(d, false) == true)
            {
                db3.tbl_slides.Remove(qslide);
                db3.SaveChanges();
            }

            return slideid;
        }



        public ActionResult getotherinfo(int id = 0)
        {

            var q = db3.tbl_applicants.Where(a => a.applicant_id == id).Select(g => new applicantrep()
            {
                applicant_id = g.applicant_id,
                f_name = g.f_name,
                l_name = g.l_name,
                fathername = g.fathername,
                codemelli = g.codemelli,
                shenasnamenum = g.shenasnamenum,
                cellnumber = g.cellnumber,
                sex = g.sex,
                degree_id = g.degree_id,
                job_id = g.job_id,
                Married = g.Married,
                borncity = g.borncity,
                birthday = g.birthday.HasValue == true ? g.birthday.Value : DateTime.Now,
                Religion = g.Religion,
                Mentalphysicalhealth = g.Mentalphysicalhealth,
                militaryservicestatus = g.militaryservicestatus,
                Insurance = g.Insurance,
                Insurancepriod = g.Insurancepriod,
                Insurancenumber = g.Insurancenumber,
                method_introduction = g.method_introduction,
                Expected_salary = g.Expected_salary,
                workingnow = g.workingnow,
                nationality = g.nationality,
                number_child = g.number_child,
                status_smoking = g.status_smoking,
                sabtdate = g.sabtdate,
                edu_field = g.edu_field,
                driverlicense = g.driverlicense,
                uniqekey = g.uniqekey,
                Type_cooperation = g.Type_cooperation,
                applicant_image = g.applicant_image


            }).SingleOrDefault();

            return PartialView("appl_info", q);
        }



        public ActionResult getcontactlist(int id = 0)
        {
            var appfull = db3.tbl_applicants.Where(a => a.applicant_id == id).FirstOrDefault();
            var q = db3.tbl_appcontactsinfo.Where(a => a.applicant_id == id).Select(g => new appcontactlistview
            {
                id = g.id,
                applicant_id = g.applicant_id,
                contacttype_id = g.contacttype_id,
                actionaccesstype = g.contacttype_.contacttype,
                contact_value = g.contact_value,


            });
            ViewBag.fullname = appfull.f_name + " " + appfull.l_name;
            return PartialView("getcontactlist", q);




        }

        public ActionResult expriencehistory(int id = 0)
        {
            var appfull = db3.tbl_applicants.Where(a => a.applicant_id == id).FirstOrDefault();
            var q = db3.tbl_jobexperiences.Where(a => a.applicant_id == id).Select(g => new appljobhistory
            {
                id = g.id,
                applicant_id = g.applicant_id,
                CompanyName = g.CompanyName,
                post = g.post,
                jobname = g.jobname,
                Periodhistory = g.Periodhistory,
                startdate = g.startdate,
                enddate = g.enddate,
                leavingreson = g.leavingreson


            });
            ViewBag.fullname = appfull.f_name + " " + appfull.l_name;
            return PartialView("expriencehistory", q);




        }

        public ActionResult delete_product(int idproduct = 0)
        {

            string username = User.Identity.Name;

          

            var qproduct = db3.tbl_products.Where(a => a.idproduct == idproduct).SingleOrDefault();

            if (qproduct == null)
            {
                return Json(false);

            }

            if ((qproduct.tbl_samples.Count() == 0) && (qproduct.tbl_listkala.Count() == 0))
            {


                    db3.tbl_products.Remove(qproduct);
                    db3.SaveChanges();
                    return Json(true);

            }
                else
                {
                    return Json(false);
                }




        }

        public ActionResult updateprofile(int userid = 0)
        {


            var q = db3.tbl_usermoreinfo.Where(a => a.user_id == userid).FirstOrDefault();

            ViewBag.user_id = userid;
            return View(q);


        }

        public ActionResult updateoraddprofile(tbl_usermoreinfo t)
        {

            var q = db3.tbl_usermoreinfo.Where(a => a.user_id == t.user_id);
            if (q.Count() == 0)
            {
                // add record
                t.birthday = t.birthday.HasValue == true ? shamsi.ToMiladi(t.birthday.Value) : DateTime.Now;
                db3.tbl_usermoreinfo.Add(t);

            }
            else
            {
                //update record
                var q1 = q.SingleOrDefault();
                q1.cellphone1 = t.cellphone1;
                q1.cellphone2 = t.cellphone2;
                q1.codemelli = t.codemelli;
                q1.fathername = t.fathername;
                q1.home_address = t.home_address;
                q1.home_tell = t.home_tell;
                q1.job = t.job;
                if (t.sex.HasValue)
                {
                    q1.sex = t.sex.Value;

                }
                else
                {
                    q1.sex = null;

                }
                q1.work_address = t.work_address;
                q1.work_tell = t.work_tell;
                q1.birthday = t.birthday.HasValue == true ? shamsi.ToMiladi(t.birthday.Value) : DateTime.Now;

            }
            db3.SaveChanges();
            return RedirectToAction("profile", "admin");

        }


        public ActionResult editcurrentuser(int id, string fname, string lname, IFormFile image)
        {
            var q = db3.tbl_user.Where(a => a.id == id).SingleOrDefault();
            q.name = fname;
            q.Lname = lname;

            if (image != null)
            {


                if (image.Length <= 2097152)
                {
                    if (image.ContentType == "image/jpeg")
                    {


                        q.img = saveimageinsql.perform(image, true, 400, 300);


                    }
                    else
                    {
                        string message = "errorfiletype";
                        return Json(new { message = message });
                    }
                }
                else
                {
                    string message = "errorfilesize";
                    return Json(new { message = message });
                }
            }

            db3.SaveChanges();
            var q2 = db3.tbl_user.Where(a => a.id == id).Select(g => new { message = "ok", fname = g.name, lname = g.Lname, image = g.img });
            //var q3 = new { message = "ok", fname = q2.name, lname = q2.Lname, image = q2.img };
            var result = JsonConvert.SerializeObject(q2);
            return Json(result);
        }


        public ActionResult manage_pcart(int take = 6, int skip = 0)
        {


            return View();
        }

        public ActionResult manage_pcartajax(List<int> model, int take = 6, int skip = 0)
        {

            var q = db3.tbl_purchasekart.Where(a => model.Contains(a.id)).OrderByDescending(a => a.id);
            int number = model.Count();

            var totalPages = (int)Math.Ceiling(number / (float)take);
            ViewBag.pagesize = take;
            ViewBag.totalpages = totalPages;
            int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
            ViewBag.pagenumber = pagenumber;
            ViewBag.skip = skip;
            TempData["search"] = "1";
            return PartialView("_partialcarts", q);




        }

        [AllowAnonymous]
        public ActionResult manage_orderajax(List<int> model, int take = 6, int skip = 0)
        {

            var q = db3.tbl_order.Where(a => model.Contains(a.id)).OrderByDescending(a => a.id);
            int number = model.Count();

            var totalPages = (int)Math.Ceiling(number / (float)take);
            ViewBag.pagesize = take;
            ViewBag.totalpages = totalPages;
            int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
            ViewBag.pagenumber = pagenumber;
            ViewBag.skip = skip;
            TempData["search"] = "1";
            return PartialView("_partialorders", q);




        }

        public ActionResult filtercartsbyispaid(int status = 0, int take = 6, int skip = 0, int userid = 0)
        {
            if (userid == 0)
            {


                if (status == 0)
                {

                    var q = db3.tbl_purchasekart.OrderByDescending(a => a.id).Select(g => g);
                    int number = q.Count();
                    var totalPages = (int)Math.Ceiling(number / (float)take);
                    ViewBag.totalpages = totalPages;
                    ViewBag.pagesize = take;
                    ViewBag.skip = skip;
                    int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                    ViewBag.pagenumber = pagenumber;
                    return PartialView("_partialcarts", q);
                }
                else if (status == 1)
                {
                    var q = db3.tbl_purchasekart.Where(a => a.ispaid == true).OrderByDescending(a => a.id).Select(g => g);
                    int number = q.Count();
                    var totalPages = (int)Math.Ceiling(number / (float)take);
                    ViewBag.totalpages = totalPages;
                    ViewBag.pagesize = take;
                    ViewBag.skip = skip;
                    int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                    ViewBag.pagenumber = pagenumber;
                    return PartialView("_partialcarts", q);
                }
                else
                {
                    var q = db3.tbl_purchasekart.Where(a => a.ispaid == false).OrderByDescending(a => a.id).Select(g => g);
                    int number = q.Count();
                    var totalPages = (int)Math.Ceiling(number / (float)take);
                    ViewBag.totalpages = totalPages;
                    ViewBag.pagesize = take;
                    ViewBag.skip = skip;
                    int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                    ViewBag.pagenumber = pagenumber;
                    return PartialView("_partialcarts", q);
                }
            }
            else
            {
                if (status == 0)
                {

                    var q = db3.tbl_purchasekart.Where(a => a.userid == userid).OrderByDescending(a => a.id).Select(g => g);
                    int number = q.Count();
                    var totalPages = (int)Math.Ceiling(number / (float)take);
                    ViewBag.totalpages = totalPages;
                    ViewBag.pagesize = take;
                    ViewBag.skip = skip;
                    int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                    ViewBag.pagenumber = pagenumber;
                    return PartialView("_partialcarts", q);
                }
                else if (status == 1)
                {
                    var q = db3.tbl_purchasekart.Where(a => a.ispaid == true && a.userid == userid).OrderByDescending(a => a.id).Select(g => g);
                    int number = q.Count();
                    var totalPages = (int)Math.Ceiling(number / (float)take);
                    ViewBag.totalpages = totalPages;
                    ViewBag.pagesize = take;
                    ViewBag.skip = skip;
                    int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                    ViewBag.pagenumber = pagenumber;
                    return PartialView("_partialcarts", q);
                }
                else
                {
                    var q = db3.tbl_purchasekart.Where(a => a.ispaid == false && a.userid == userid).OrderByDescending(a => a.id).Select(g => g);
                    int number = q.Count();
                    var totalPages = (int)Math.Ceiling(number / (float)take);
                    ViewBag.totalpages = totalPages;
                    ViewBag.pagesize = take;
                    ViewBag.skip = skip;
                    int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                    ViewBag.pagenumber = pagenumber;
                    return PartialView("_partialcarts", q);
                }
            }

        }





        public ActionResult searchcartbyusername(string username, int status = 0, int take = 6, int skip = 0)
        {
            TempData["search"] = "2";
            TempData["searchtext"] = "جستجو بر اساس نام کاربری : " + username;
            if (status == 0)
            {

                var q = db3.tbl_purchasekart.Where(a => a.user.username.Contains(username)).OrderByDescending(a => a.id).Select(g => g);
                int number = q.Count();
                var totalPages = (int)Math.Ceiling(number / (float)take);
                ViewBag.totalpages = totalPages;
                ViewBag.pagesize = take;
                ViewBag.skip = skip;
                int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                ViewBag.pagenumber = pagenumber;
                return PartialView("_partialcarts", q);
            }
            else if (status == 1)
            {
                var q = db3.tbl_purchasekart.Where(a => a.user.username.Contains(username) && a.ispaid == true).OrderByDescending(a => a.id).Select(g => g);
                int number = q.Count();
                var totalPages = (int)Math.Ceiling(number / (float)take);
                ViewBag.totalpages = totalPages;
                ViewBag.pagesize = take;
                ViewBag.skip = skip;
                int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                ViewBag.pagenumber = pagenumber;
                return PartialView("_partialcarts", q);
            }
            else
            {
                var q = db3.tbl_purchasekart.Where(a => a.user.username.Contains(username) && a.ispaid == false).OrderByDescending(a => a.id).Select(g => g);
                int number = q.Count();
                var totalPages = (int)Math.Ceiling(number / (float)take);
                ViewBag.totalpages = totalPages;
                ViewBag.pagesize = take;
                ViewBag.skip = skip;
                int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                ViewBag.pagenumber = pagenumber;
                return PartialView("_partialcarts", q);
            }


        }

        //
        public ActionResult searchbyfinalstatus(Nullable<bool> finalstatus, int status = 0, int take = 6, int skip = 0)
        {

            TempData["search"] = "3";
            TempData["searchtext"] = "جستجو بر اساس وضعیت حمل : ";
            if (status == 0)
            {

                var q = db3.tbl_purchasekart.Where(a => a.isdeleverd == finalstatus).OrderByDescending(a => a.id).Select(g => g);
                int number = q.Count();
                var totalPages = (int)Math.Ceiling(number / (float)take);
                ViewBag.totalpages = totalPages;
                ViewBag.pagesize = take;
                ViewBag.skip = skip;
                int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                ViewBag.pagenumber = pagenumber;
                return PartialView("_partialcarts", q);
            }
            else if (status == 1)
            {
                var q = db3.tbl_purchasekart.Where(a => a.isdeleverd == finalstatus && a.ispaid == true).OrderByDescending(a => a.id).Select(g => g);
                int number = q.Count();
                var totalPages = (int)Math.Ceiling(number / (float)take);
                ViewBag.totalpages = totalPages;
                ViewBag.pagesize = take;
                ViewBag.skip = skip;
                int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                ViewBag.pagenumber = pagenumber;
                return PartialView("_partialcarts", q);
            }
            else
            {
                var q = db3.tbl_purchasekart.Where(a => a.isdeleverd == finalstatus && a.ispaid == false).OrderByDescending(a => a.id).Select(g => g);
                int number = q.Count();
                var totalPages = (int)Math.Ceiling(number / (float)take);
                ViewBag.totalpages = totalPages;
                ViewBag.pagesize = take;
                ViewBag.skip = skip;
                int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                ViewBag.pagenumber = pagenumber;
                return PartialView("_partialcarts", q);
            }


        }

        //

        public ActionResult searchbypaydate(string paydate, int status = 0, int take = 6, int skip = 0)
        {

            TempData["search"] = "3";
            TempData["searchtext"] = "جستجو بر اساس وضعیت حمل : ";
            DateTime dt = DateTime.Parse(paydate);
            DateTime miladidt = shamsi.ToMiladi(dt);

            if (status == 0)
            {

                var q = db3.tbl_purchasekart.ToList().Where(a => miladidt == (a.purchasedateend.HasValue == true ? a.purchasedateend.Value.Date : DateTime.Now.AddYears(1000).Date)).OrderByDescending(a => a.id).Select(g => g);
                int number = q.Count();
                var totalPages = (int)Math.Ceiling(number / (float)take);
                ViewBag.totalpages = totalPages;
                ViewBag.pagesize = take;
                ViewBag.skip = skip;
                int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                ViewBag.pagenumber = pagenumber;
                return PartialView("_partialcarts", q);
            }
            else if (status == 1)
            {
                var q = db3.tbl_purchasekart.ToList().Where(a => miladidt == (a.purchasedateend.HasValue == true ? a.purchasedateend.Value.Date : DateTime.Now.AddYears(1000).Date) && a.ispaid == true).OrderByDescending(a => a.id).Select(g => g);
                int number = q.Count();
                var totalPages = (int)Math.Ceiling(number / (float)take);
                ViewBag.totalpages = totalPages;
                ViewBag.pagesize = take;
                ViewBag.skip = skip;
                int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                ViewBag.pagenumber = pagenumber;
                return PartialView("_partialcarts", q);
            }
            else
            {
                var q = db3.tbl_purchasekart.ToList().Where(a => miladidt == (a.purchasedateend.HasValue == true ? a.purchasedateend.Value.Date : DateTime.Now.AddYears(1000).Date) && a.ispaid == false).OrderByDescending(a => a.id).Select(g => g);
                int number = q.Count();
                var totalPages = (int)Math.Ceiling(number / (float)take);
                ViewBag.totalpages = totalPages;
                ViewBag.pagesize = take;
                ViewBag.skip = skip;
                int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                ViewBag.pagenumber = pagenumber;
                return PartialView("_partialcarts", q);
            }


        }

        //
        public ActionResult searchbypaydaterange(string paydatefrom, string paydateto, int status = 0, int take = 6, int skip = 0)
        {

            TempData["search"] = "3";
            TempData["searchtext"] = "جستجو بر اساس وضعیت حمل : ";
            DateTime dt1 = DateTime.Parse(paydatefrom);
            DateTime miladidt1 = shamsi.ToMiladi(dt1);

            DateTime dt2 = DateTime.Parse(paydateto);
            DateTime miladidt2 = shamsi.ToMiladi(dt2);

            if (status == 0)
            {

                var q = db3.tbl_purchasekart.ToList().Where(a => (miladidt1 <= (a.purchasedateend.HasValue == true ? a.purchasedateend.Value.Date : DateTime.Now.AddYears(1000).Date) && miladidt2 >= (a.purchasedateend.HasValue == true ? a.purchasedateend.Value.Date : DateTime.Now.AddYears(1000).Date))).OrderByDescending(a => a.id).Select(g => g);
                int number = q.Count();
                var totalPages = (int)Math.Ceiling(number / (float)take);
                ViewBag.totalpages = totalPages;
                ViewBag.pagesize = take;
                ViewBag.skip = skip;
                int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                ViewBag.pagenumber = pagenumber;
                return PartialView("_partialcarts", q);
            }
            else if (status == 1)
            {
                var q = db3.tbl_purchasekart.ToList().Where(a => (miladidt1 <= (a.purchasedateend.HasValue == true ? a.purchasedateend.Value.Date : DateTime.Now.AddYears(1000).Date) && miladidt2 >= (a.purchasedateend.HasValue == true ? a.purchasedateend.Value.Date : DateTime.Now.AddYears(1000).Date)) && a.ispaid == true).OrderByDescending(a => a.id).Select(g => g);
                int number = q.Count();
                var totalPages = (int)Math.Ceiling(number / (float)take);
                ViewBag.totalpages = totalPages;
                ViewBag.pagesize = take;
                ViewBag.skip = skip;
                int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                ViewBag.pagenumber = pagenumber;
                return PartialView("_partialcarts", q);
            }
            else
            {
                var q = db3.tbl_purchasekart.ToList().Where(a => (miladidt1 <= (a.purchasedateend.HasValue == true ? a.purchasedateend.Value.Date : DateTime.Now.AddYears(1000).Date) && miladidt2 >= (a.purchasedateend.HasValue == true ? a.purchasedateend.Value.Date : DateTime.Now.AddYears(1000).Date)) && a.ispaid == false).OrderByDescending(a => a.id).Select(g => g);
                int number = q.Count();
                var totalPages = (int)Math.Ceiling(number / (float)take);
                ViewBag.totalpages = totalPages;
                ViewBag.pagesize = take;
                ViewBag.skip = skip;
                int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                ViewBag.pagenumber = pagenumber;
                return PartialView("_partialcarts", q);
            }


        }

        //
        public ActionResult searchbyDemandtransportation(bool Demandtransportation, int status = 0, int take = 6, int skip = 0)
        {

            TempData["search"] = "3";
            TempData["searchtext"] = "جستجو بر اساس وضعیت حمل : ";


            if (status == 0)
            {
                if (Demandtransportation == true)
                {

                    var q = db3.tbl_purchasekart.ToList().Where(a => a.tbl_transportationcost.Count() != 0).OrderByDescending(a => a.id).Select(g => g);
                    int number = q.Count();
                    var totalPages = (int)Math.Ceiling(number / (float)take);
                    ViewBag.totalpages = totalPages;
                    ViewBag.pagesize = take;
                    ViewBag.skip = skip;
                    int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                    ViewBag.pagenumber = pagenumber;
                    return PartialView("_partialcarts", q);
                }
                else
                {
                    var q = db3.tbl_purchasekart.ToList().Where(a => a.tbl_transportationcost.Count() == 0).OrderByDescending(a => a.id).Select(g => g);
                    int number = q.Count();
                    var totalPages = (int)Math.Ceiling(number / (float)take);
                    ViewBag.totalpages = totalPages;
                    ViewBag.pagesize = take;
                    ViewBag.skip = skip;
                    int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                    ViewBag.pagenumber = pagenumber;
                    return PartialView("_partialcarts", q);
                }


            }
            else if (status == 1)
            {
                if (Demandtransportation == true)
                {

                    var q = db3.tbl_purchasekart.ToList().Where(a => a.tbl_transportationcost.Count() != 0 && a.ispaid == true).OrderByDescending(a => a.id).Select(g => g);
                    int number = q.Count();
                    var totalPages = (int)Math.Ceiling(number / (float)take);
                    ViewBag.totalpages = totalPages;
                    ViewBag.pagesize = take;
                    ViewBag.skip = skip;
                    int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                    ViewBag.pagenumber = pagenumber;
                    return PartialView("_partialcarts", q);
                }
                else
                {
                    var q = db3.tbl_purchasekart.ToList().Where(a => a.tbl_transportationcost.Count() == 0 && a.ispaid == true).OrderByDescending(a => a.id).Select(g => g);
                    int number = q.Count();
                    var totalPages = (int)Math.Ceiling(number / (float)take);
                    ViewBag.totalpages = totalPages;
                    ViewBag.pagesize = take;
                    ViewBag.skip = skip;
                    int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                    ViewBag.pagenumber = pagenumber;
                    return PartialView("_partialcarts", q);
                }



            }
            else
            {
                if (Demandtransportation == true)
                {

                    var q = db3.tbl_purchasekart.ToList().Where(a => a.tbl_transportationcost.Count() != 0 && a.ispaid == false).OrderByDescending(a => a.id).Select(g => g);
                    int number = q.Count();
                    var totalPages = (int)Math.Ceiling(number / (float)take);
                    ViewBag.totalpages = totalPages;
                    ViewBag.pagesize = take;
                    ViewBag.skip = skip;
                    int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                    ViewBag.pagenumber = pagenumber;
                    return PartialView("_partialcarts", q);
                }
                else
                {
                    var q = db3.tbl_purchasekart.ToList().Where(a => a.tbl_transportationcost.Count() == 0 && a.ispaid == false).OrderByDescending(a => a.id).Select(g => g);
                    int number = q.Count();
                    var totalPages = (int)Math.Ceiling(number / (float)take);
                    ViewBag.totalpages = totalPages;
                    ViewBag.pagesize = take;
                    ViewBag.skip = skip;
                    int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                    ViewBag.pagenumber = pagenumber;
                    return PartialView("_partialcarts", q);
                }

            }


        }
        // to search orderds

        public ActionResult searchordersbyfrombranch(int frombranch_id, int take = 6, int skip = 0)
        {
            var Allorder = db3.tbl_order.ToList().OrderByDescending(a => a.id);
            var myorder = Allorder.Where(a => a.from_branchid == frombranch_id).Select(g => g);
            var q = (frombranch_id == 0 ? Allorder : myorder);
            int number = q.Count();
            var totalPages = (int)Math.Ceiling(number / (float)take);
            ViewBag.totalpages = totalPages;
            ViewBag.pagesize = take;
            ViewBag.skip = skip;
            int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
            ViewBag.pagenumber = pagenumber;
            return PartialView("_partialorders", q);
            //return PartialView("_Partialtest");
        }
        // search by orderid
        [AllowAnonymous]
        public ActionResult searchordersbyorderid(int orderid, int take = 6, int skip = 0)
        {
            var Allorder = db3.tbl_order.ToList().OrderByDescending(a => a.id);
            var myorder = Allorder.Where(a => a.id == orderid).Select(g => g);
            var q = myorder;
            int number = q.Count();
            var totalPages = (int)Math.Ceiling(number / (float)take);
            ViewBag.totalpages = totalPages;
            ViewBag.pagesize = take;
            ViewBag.skip = skip;
            int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
            ViewBag.pagenumber = pagenumber;
            return PartialView("_partialorders", q);
        }
        // search orders by sodoordate
        [AllowAnonymous]
        public ActionResult searchorderbysodoordate(string sodoordate, int frombranch_id, int take = 6, int skip = 0)
        {
            var Allorder = db3.tbl_order.ToList().OrderByDescending(a => a.id);
            DateTime thisdate = (sodoordate == null ? DateTime.Now : shamsi.PersianDateToGregorianDate(sodoordate));
            var myorder = Allorder.Where(a => a.sodoor_date.Date == thisdate.Date).Select(g => g);
            IEnumerable<tbl_order> q = null;
            q = (frombranch_id == 0 ? myorder : myorder.Where(a => a.from_branchid == frombranch_id));

            int number = q.Count();
            var totalPages = (int)Math.Ceiling(number / (float)take);
            ViewBag.totalpages = totalPages;
            ViewBag.pagesize = take;
            ViewBag.skip = skip;
            int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
            ViewBag.pagenumber = pagenumber;
            return PartialView("_partialorders", q);
        }
        // search orders by sodoordaterange
        [AllowAnonymous]
        public ActionResult searchordersbysodoordaterange(string sodoordatefrom, string sodoordateto, int frombranch_id, int take = 6, int skip = 0)
        {
            var Allorder = db3.tbl_order.ToList().OrderByDescending(a => a.id);
            DateTime fromdate = (sodoordatefrom == null ? DateTime.Now.AddYears(-1000).Date : shamsi.PersianDateToGregorianDate(sodoordatefrom));
            DateTime todate = (sodoordateto == null ? DateTime.Now.AddDays(1).Date : shamsi.PersianDateToGregorianDate(sodoordateto));
            var myorder = Allorder.Where(a => a.sodoor_date.Date >= fromdate.Date && a.sodoor_date.Date < todate.Date).Select(g => g);
            IEnumerable<tbl_order> q = null;
            q = (frombranch_id == 0 ? myorder : myorder.Where(a => a.from_branchid == frombranch_id));

            int number = q.Count();
            var totalPages = (int)Math.Ceiling(number / (float)take);
            ViewBag.totalpages = totalPages;
            ViewBag.pagesize = take;
            ViewBag.skip = skip;
            int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
            ViewBag.pagenumber = pagenumber;
            return PartialView("_partialorders", q);
        }
        // search orders based on Done status
        [AllowAnonymous]
        public ActionResult searchordersbydone(string orderdone, int frombranch_id, int take = 6, int skip = 0)
        {
            var Allorder = db3.tbl_order.ToList().OrderByDescending(a => a.id);
            var myorder = (orderdone == "0" ? Allorder.Select(g => g) : Allorder.Where(a => a.done == bool.Parse(orderdone)).Select(g => g));

            IEnumerable<tbl_order> q = null;
            q = (frombranch_id == 0 ? myorder : myorder.Where(a => a.from_branchid == frombranch_id));

            int number = q.Count();
            var totalPages = (int)Math.Ceiling(number / (float)take);
            ViewBag.totalpages = totalPages;
            ViewBag.pagesize = take;
            ViewBag.skip = skip;
            int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
            ViewBag.pagenumber = pagenumber;
            return PartialView("_partialorders", q);
        }

        public ActionResult addvertificationofdeliver(int cartid = 0)
        {
            ViewBag.cartid = cartid;

            return PartialView("_Partialdeliverinfo");
        }


        public ActionResult deliverinfosave(int? id, int cartid, string deliver_date, string driver_name, string driver_cellphone, string description, IFormFile receipt_img)
        {
            var cart = db3.tbl_purchasekart.Where(a => a.id == cartid).SingleOrDefault();

            if (id == null)
            {
                //add
                tbl_transportationdeliverinfo t = new tbl_transportationdeliverinfo();
                t.cartid = cartid;
                t.deliver_date = deliver_date.Trim().Length != 0 ? shamsi.ToMiladi(DateTime.Parse(deliver_date)) : DateTime.Now;
                t.description = description;
                t.driver_cellphone = driver_cellphone;
                t.driver_name = driver_name;
                if (receipt_img != null)
                {


                    if (receipt_img.Length <= 2097152)
                    {
                        if (receipt_img.ContentType == "image/jpeg")
                        {

                            //byte[] b = new byte[receipt_img.Length];
                            //receipt_img.InputStream.Read(b, 0, receipt_img.Length);

                            //System.IO.MemoryStream mem = new System.IO.MemoryStream(b);
                            //System.Drawing.Image imgmem = System.Drawing.Image.FromStream(mem);
                            //System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(imgmem, 400, 300);
                            //System.IO.MemoryStream memThumbnail = new System.IO.MemoryStream();
                            //bmp.Save(memThumbnail, System.Drawing.Imaging.ImageFormat.Jpeg);
                            //byte[] bb = memThumbnail.ToArray();
                            t.receipt_img = saveimageinsql.perform(receipt_img, false);


                        }
                        else
                        {
                            string message = "errorfiletype";
                            return Json(new { message = message });
                        }
                    }
                    else
                    {
                        string message = "errorfilesize";
                        return Json(new { message = message });
                    }


                }

                db3.tbl_transportationdeliverinfo.Add(t);
                cart.isdeleverd = true;

                db3.SaveChanges();
                string message0 = "success";
                return Json(new { message = message0 });

            }
            else
            {
                // update
                var q = db3.tbl_transportationdeliverinfo.Where(a => a.id == id).SingleOrDefault();
                q.deliver_date = deliver_date.Trim().Length != 0 ? shamsi.ToMiladi(DateTime.Parse(deliver_date)) : DateTime.Now;
                q.description = description;
                q.driver_cellphone = driver_cellphone;
                q.driver_name = driver_name;
                if (receipt_img != null)
                {


                    if (receipt_img.Length <= 2097152)
                    {
                        if (receipt_img.ContentType == "image/jpeg")
                        {

                            //byte[] b = new byte[receipt_img.Length];
                            //receipt_img.InputStream.Read(b, 0, receipt_img.Length);

                            //System.IO.MemoryStream mem = new System.IO.MemoryStream(b);
                            //System.Drawing.Image imgmem = System.Drawing.Image.FromStream(mem);
                            //System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(imgmem, 400, 300);
                            //System.IO.MemoryStream memThumbnail = new System.IO.MemoryStream();
                            //bmp.Save(memThumbnail, System.Drawing.Imaging.ImageFormat.Jpeg);
                            //byte[] bb = memThumbnail.ToArray();
                            q.receipt_img = saveimageinsql.perform(receipt_img, false);


                        }
                        else
                        {
                            string message3 = "errorfiletype";
                            return Json(new { message = message3 });
                        }
                    }
                    else
                    {
                        string message4 = "errorfilesize";
                        return Json(new { message = message4 });
                    }
                }
                cart.isdeleverd = true;
                db3.SaveChanges();
                string message7 = "success";
                return Json(new { message = message7 });
            }





            //var q2 = db3.tbl_user.Where(a => a.id == id).Select(g => new { message = "ok", fname = g.name, lname = g.Lname, image = g.img });
            ////var q3 = new { message = "ok", fname = q2.name, lname = q2.Lname, image = q2.img };
            //var result = JsonConvert.SerializeObject(q2);

        }


        public ActionResult showimagereceipt(int id = 0)
        {
            var q = db3.tbl_transportationdeliverinfo.Where(a => a.id == id).ToList().Select(g => g).SingleOrDefault();
            return PartialView("_showimagereceipt", q);
        }


        public ActionResult manage_mypcart(int take = 6, int skip = 0)
        {


            int userid = int.Parse(User.Claims.FirstOrDefault().Value);



            return View();
        }


        public ActionResult GetControllerActions()
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            var allcontrollers = asm.GetTypes()
                .Where(
                    x => typeof(Controller).IsAssignableFrom(typeof(Controller))
                    && x.Namespace == "SoltaniWeb.Controllers" && x.Name.StartsWith("<") != true);

            allcontrollers.ToList().ForEach(x =>
            {

                tbl_controllers c = new tbl_controllers
                {
                    controller_name = x.Name,
                    tbl_actions = new List<tbl_actions>()
                };

                x.GetMethods(BindingFlags.Public | BindingFlags.Instance).ToList().
                Where(z => z.DeclaringType.Name == x.Name &&
                        z.IsVirtual == false && z.IsAbstract == false && z.IsConstructor == false).ToList().
                ForEach(y =>
                {
                    tbl_actions action = new tbl_actions { action_name = y.Name };
                    c.tbl_actions.Add(action);
                });
                db3.tbl_controllers.Add(c);
                db3.SaveChanges();
            });

            return RedirectToAction("Index", "home");
        }
        public ActionResult GetControllerActions2()
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            var allcontrollers = asm.GetTypes()
                .Where(
                    x => typeof(Controller).IsAssignableFrom(typeof(Controller))
                    && x.Namespace == "SoltaniWeb.Controllers" && x.Name.StartsWith("<") != true);
            allcontrollers.ToList().ForEach(x =>
            {
                var q0 = db3.tbl_controllers.Where(a => a.controller_name == x.Name);
                if (q0.Count() == 0)
                {

                    tbl_controllers c = new tbl_controllers
                    {
                        controller_name = x.Name,
                        tbl_actions = new List<tbl_actions>()
                    };
                    x.GetMethods(BindingFlags.Public | BindingFlags.Instance).ToList().
                    Where(z => z.DeclaringType.Name == x.Name &&
                            z.IsVirtual == false && z.IsAbstract == false && z.IsConstructor == false).ToList().
                    ForEach(y =>
                    {
                        tbl_actions action = new tbl_actions { action_name = y.Name, menu_id = 6 };
                        c.tbl_actions.Add(action);
                    });
                    db3.tbl_controllers.Add(c);
                }
                else
                {
                    x.GetMethods(BindingFlags.Public | BindingFlags.Instance).ToList().
                    Where(z => z.DeclaringType.Name == x.Name &&
                            z.IsVirtual == false && z.IsAbstract == false && z.IsConstructor == false).ToList().
                    ForEach(y =>
                    {
                        var q1 = db3.tbl_actions.Where(a => a.controller_.controller_name == x.Name && a.action_name == y.Name);
                        int controllerid = db3.tbl_controllers.Where(a => a.controller_name == x.Name).SingleOrDefault().id;
                        if (q1.Count() == 0)
                        {
                            tbl_actions action = new tbl_actions { action_name = y.Name, controller_id = controllerid, menu_id = 6 };

                            db3.tbl_actions.Add(action);

                        }
                    });




                }


            });
            db3.SaveChanges();
            string msg = "توابع جدید به جدول اکشن ها اضافه شده است. ";
            //TempData["message"] = msg;
            return Json(msg);

        }



        public ActionResult updateaccessleveltouser(int userid = 0)
        {
            bool result = _userServices.updateaccessleveltouser(userid);

            return Json(result);
        }



        public ActionResult manage_actionaccessibility()
        {

            var records = db3.tbl_actionaccessibility.Include(a => a.acction_);
            var models = records.GroupBy(a => a.user.username).Select(g => new Accessclass { user = g.Key, actionaccesslist = g.OrderBy(l => l.acction_id) });
            return View(models);
        }

        public ActionResult changepermission(int id = 0)
        {
            var q = db3.tbl_actionaccessibility.Where(a => a.id == id).SingleOrDefault();
            if (q.permission == true)
            {
                q.permission = false;
            }
            else
            {
                q.permission = true;
            }
            db3.SaveChanges();
            return Json(true);
        }


        public ActionResult activeallforthisuser(string user)
        {

            var q = db3.tbl_actionaccessibility.Where(a => a.user.username == user).ToList();

            foreach (var item in q)
            {
                item.permission = true;
            }
            db3.SaveChanges();
            return Json(true);
        }


        public ActionResult disactiveallforthisuser(string user)
        {

            var q = db3.tbl_actionaccessibility.Where(a => a.user.username == user).ToList();

            foreach (var item in q)
            {
                item.permission = false;
            }
            db3.SaveChanges();
            return Json(true);
        }

        public ActionResult activeallforthisuserandcontroller(string user, string controller)
        {

            var q = db3.tbl_actionaccessibility.Where(a => a.user.username == user && a.acction_.controller_.controller_name == controller).ToList();

            foreach (var item in q)
            {
                item.permission = true;
            }
            db3.SaveChanges();
            return Json(true);
        }
        public ActionResult disactiveallforthisuserandcontroller(string user, string controller)
        {

            var q = db3.tbl_actionaccessibility.Where(a => a.user.username == user && a.acction_.controller_.controller_name == controller).ToList();

            foreach (var item in q)
            {
                item.permission = false;
            }
            db3.SaveChanges();
            return Json(true);
        }

        public ActionResult manage_transactions()
        {
            return View();

        }


        //[HttpGet]
        //public ActionResult Gettransaction(JqGridRequest request)
        //{



        //    var list = db3.tbl_transaction.ToList().Select(g => new
        //    {

        //        id = g.id,
        //        cartid = g.cartid,
        //        amount = g.amount,
        //        sharh = g.sharh,
        //        transid = g.transid,
        //        userid = g.user_id,
        //        username = g.user_.username,
        //        varizdate = g.varizdate.ToPersianDate(),
        //        transtime = g.varizdate.ToString("HH:mm:ss")

        //    }).ToList();




        //    //



        //    var pageIndex = request.page - 1;
        //    var pageSize = request.rows;
        //    var totalRecords = list.Count;
        //    var totalPages = (int)Math.Ceiling(totalRecords / (float)pageSize);


        //    var productsQuery = list.AsQueryable();

        //    productsQuery = new JqGridSearch(request, this.Request.Form, DateTimeType.Persian).ApplyFilter(productsQuery);

        //    var productsList = productsQuery.OrderBy(request.sidx + " " + request.sord)
        //                                    .Skip(pageIndex * pageSize)
        //                                    .Take(pageSize)
        //                                    .ToList();

        //    var productsData = new JqGridData
        //    {
        //        Total = totalPages,
        //        Page = request.page,
        //        Records = totalRecords,
        //        Rows = (productsList.Select(product => new JqGridRowData
        //        {
        //            Id = product.id,
        //            RowCells = new List<string>
        //                       {

        //                           product.id.ToString(CultureInfo.InvariantCulture),
        //                            product.cartid.ToString(),
        //                           product.varizdate.ToString(),
        //                           product.transtime,
        //                            product.amount.ToString() ,
        //                            product.sharh,
        //                            product.transid.ToString(),
        //                            product.userid.ToString(),
        //                            product.username




        //                        }
        //        })).ToArray()
        //    };
        //    return Json(productsData);
        //}


        public ActionResult addnewtransaction(tbl_transaction t)
        {
            int userid = int.Parse(User.Claims.FirstOrDefault().Value);
            if (t.amount > 0)
            {
                return Json(false);
            }
            else
            {

                string username = User.Identity.Name;
                t.user_id = userid;
                t.varizdate = t.varizdate + DateTime.Now.TimeOfDay;
                db3.tbl_transaction.Add(t);
                db3.SaveChanges();


                return Json(true);
            }
        }


        public ActionResult Edittransaction(tbl_transaction t)
        {
            if (t.amount > 0)
            {
                return Json(false);

            }
            else
            {
                var q = db3.tbl_transaction.Where(a => a.id == t.id).SingleOrDefault();
                q.amount = t.amount;
                q.varizdate = t.varizdate;
                q.sharh = t.sharh;
                q.transid = t.transid;
                db3.SaveChanges();


                return Json(true);
            }
        }


        public ActionResult Deletetransaction(tbl_transaction t)
        {
            var q = db3.tbl_transaction.Where(a => a.id == t.id).SingleOrDefault();
            db3.tbl_transaction.Remove(q);
            db3.SaveChanges();
            return Json(true);

        }

        public ActionResult setreadtimeofchat(int from = 0, int to = 0)
        {
            var unreadmsg = db3.tbl_signalRmsg.Where(a => a.to_userid == to && a.from_userid == from && a.datetime_read == null);
            if (unreadmsg.Count() > 0)
            {
                string msgid = unreadmsg.FirstOrDefault().id.ToString();
                foreach (var item in unreadmsg)
                {
                    item.datetime_read = DateTime.Now;

                }
                db3.SaveChanges();
                //int msgid = unreadmsg.SingleOrDefault().id;
                //var context = GlobalHost.ConnectionManager.GetHubContext<chatHub>();
                //context.Clients.All.reading(msgid);
            }

            return Json(true);
        }


        [HttpPost]

        public JsonResult uploadfile(string username, string userid, string caption, IFormFile img)
        {
            string results;
            tbl_filestosend t = new tbl_filestosend();

            t.user_id = int.Parse(userid);
            t.caption = caption;
            DateTime dt = DateTime.Now;
            t.uploaddatetime = dt;
            string affix = $"{dt.Year}{dt.Month}{dt.DayOfYear}{dt.Hour}{dt.Minute}{dt.Second}";
            t.ftp_id = 8;
            if (img != null)
            {

                //var qTools = db3.Tbl_Tools.OrderByDescending(a => a.ID).FirstOrDefault();
                if (img.Length <= 52428800)
                {
                    if (img.ContentType.Contains("image") || img.ContentType.Contains("pdf"))
                    {



                        t.filecontent = saveimageinsql.perform(img, false);

                        //Image.SaveAs(Path);

                        t.imagename = img.FileName;
                        db3.tbl_filestosend.Add(t);
                        db3.SaveChanges();

                        results = "ok";
                        return Json(new { results = results, id = t.id, caption = t.caption });
                    }
                    else
                    {
                        results = "فرمت فایل صحیح نمی باشد";
                        return Json(new { results = results });
                    }
                }
                else
                {
                    results = "حجم تصویر بیش از اندازه می باشد";
                    return Json(new { results = results });
                }
            }
            else
            {
                results = "noimage";
                return Json(new { results = results });
            }

        }


        [HttpGet]

        public FileResult DownLoadFile(int id)
        {


            var ObjFiles = db3.tbl_filestosend.Where(a => a.id == id).SingleOrDefault();


            //return File(ObjFiles.filecontent,)
            return File(ObjFiles.filecontent, System.Net.Mime.MediaTypeNames.Application.Octet, ObjFiles.imagename);

        }

        public ActionResult getmsgtext(int msgid = 0)
        {
            string msg = db3.tbl_signalRmsg.Where(a => a.id == msgid).SingleOrDefault().msg_text;
            return Json(msg);

        }

        public ActionResult getmsgrecord(int msgid = 0)
        {
            tbl_signalRmsg msg = db3.tbl_signalRmsg.Where(a => a.id == msgid).SingleOrDefault();
            return Json(msg);
        }



        public ActionResult msgtopin(int msgid = 0)
        {
            var msg = db3.tbl_signalRmsg.Where(a => a.id == msgid).SingleOrDefault();
            msg.pin = true;
            msg.datetime_pin = DateTime.Now;
            db3.SaveChanges();
            return Json(true);

        }





        public ActionResult getmsginfo(int msgid = 0)
        {
            var msg = db3.tbl_signalRmsg.Where(a => a.id == msgid).SingleOrDefault();

            return Json(new { msgtext = msg.msg_text, msgtextpure = msg.msg_textpure, from = msg.from_user.username, from_id = msg.from_user.id, msgid = msg.id, msgtime_send = msg.datetime_send.Value.ToString("HH:mm:ss"), msgdate_send = msg.datetime_send.Value.ToPersianDate(), topic = msg.topic_pin });

        }

        public ActionResult filterusersbystatus(bool? status, int take = 6, int skip = 0, int userid = 0)
        {
            if (status == null)

            {
                var q = db3.tbl_user.OrderByDescending(a => a.id).Select(g => g);
                int number = q.Count();
                var totalPages = (int)Math.Ceiling(number / (float)take);
                ViewBag.totalpages = totalPages;
                ViewBag.pagesize = take;
                ViewBag.skip = skip;
                int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                ViewBag.pagenumber = pagenumber;
                return PartialView("~/Views/Shared/PartialUsersManagment/_partialusers.cshtml", q);
            }
            else
            {
                var q = db3.tbl_user.Where(a => a.status == status).OrderByDescending(a => a.id).Select(g => g);
                int number = q.Count();
                var totalPages = (int)Math.Ceiling(number / (float)take);
                ViewBag.totalpages = totalPages;
                ViewBag.pagesize = take;
                ViewBag.skip = skip;
                int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                ViewBag.pagenumber = pagenumber;
                return PartialView("~/Views/Shared/PartialUsersManagment/_partialusers.cshtml", q);
            }


        }


        public ActionResult manage_usersajax(List<int> model, int take = 6, int skip = 0)
        {

            var q = db3.tbl_user.Where(a => model.Contains(a.id)).OrderByDescending(a => a.id);
            int number = model.Count();

            var totalPages = (int)Math.Ceiling(number / (float)take);
            ViewBag.pagesize = take;
            ViewBag.totalpages = totalPages;
            int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
            ViewBag.pagenumber = pagenumber;
            ViewBag.skip = skip;
            TempData["search"] = "1";
            return PartialView("~/Views/Shared/PartialUsersManagment/_partialusers.cshtml", q);




        }

        public ActionResult searchusersbystatus(bool? status, int take = 6, int skip = 0)
        {
            if (status == null)
            {

                var q = db3.tbl_user.OrderByDescending(a => a.id).Select(g => g);
                int number = q.Count();
                var totalPages = (int)Math.Ceiling(number / (float)take);
                ViewBag.totalpages = totalPages;
                ViewBag.pagesize = take;
                ViewBag.skip = skip;
                int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                ViewBag.pagenumber = pagenumber;
                return PartialView("~/Views/Shared/PartialUsersManagment/_partialusers.cshtml", q);
            }
            else
            {
                var q = db3.tbl_user.Where(a => a.status == status).OrderByDescending(a => a.id).Select(g => g);
                int number = q.Count();
                var totalPages = (int)Math.Ceiling(number / (float)take);
                ViewBag.totalpages = totalPages;
                ViewBag.pagesize = take;
                ViewBag.skip = skip;
                int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                ViewBag.pagenumber = pagenumber;
                return PartialView("~/Views/Shared/PartialUsersManagment/_partialusers.cshtml", q);
            }
        }



        public ActionResult searchusersbyusername(string username, bool? status, int take = 6, int skip = 0)
        {
            TempData["search"] = "2";
            TempData["searchtext"] = "جستجو بر اساس نام کاربری : " + username;
            if (status == null)
            {

                var q = db3.tbl_user.Where(a => a.username.Contains(username)).OrderByDescending(a => a.id).Select(g => g);
                int number = q.Count();
                var totalPages = (int)Math.Ceiling(number / (float)take);
                ViewBag.totalpages = totalPages;
                ViewBag.pagesize = take;
                ViewBag.skip = skip;
                int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                ViewBag.pagenumber = pagenumber;
                return PartialView("~/Views/Shared/PartialUsersManagment/_partialusers.cshtml", q);
            }
            else
            {
                var q = db3.tbl_user.Where(a => a.username.Contains(username) && a.status == status).OrderByDescending(a => a.id).Select(g => g);
                int number = q.Count();
                var totalPages = (int)Math.Ceiling(number / (float)take);
                ViewBag.totalpages = totalPages;
                ViewBag.pagesize = take;
                ViewBag.skip = skip;
                int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                ViewBag.pagenumber = pagenumber;
                return PartialView("~/Views/Shared/PartialUsersManagment/_partialusers.cshtml", q);
            }


        }


        public ActionResult searchusersbyname(string fname, bool? status, int take = 6, int skip = 0)
        {

            if (status == null)
            {

                var q = db3.tbl_user.Where(a => a.name.Contains(fname) || a.Lname.Contains(fname)).OrderByDescending(a => a.id).Select(g => g);
                int number = q.Count();
                var totalPages = (int)Math.Ceiling(number / (float)take);
                ViewBag.totalpages = totalPages;
                ViewBag.pagesize = take;
                ViewBag.skip = skip;
                int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                ViewBag.pagenumber = pagenumber;
                return PartialView("~/Views/Shared/PartialUsersManagment/_partialusers.cshtml", q);
            }
            else
            {
                var q = db3.tbl_user.Where(a => (a.name.Contains(fname) || a.Lname.Contains(fname)) && a.status == status).OrderByDescending(a => a.id).Select(g => g);
                int number = q.Count();
                var totalPages = (int)Math.Ceiling(number / (float)take);
                ViewBag.totalpages = totalPages;
                ViewBag.pagesize = take;
                ViewBag.skip = skip;
                int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                ViewBag.pagenumber = pagenumber;
                return PartialView("~/Views/Shared/PartialUsersManagment/_partialusers.cshtml", q);
            }


        }

        public ActionResult searchbysignupdate(string signupdate, bool? status, int take = 6, int skip = 0)
        {
            TempData["search"] = "3";
            TempData["searchtext"] = "جستجو بر اساس تاریخ ثبت نام : ";
            DateTime dt = DateTime.Parse(signupdate);
            DateTime miladidt = shamsi.ToMiladi(dt);

            if (status == null)
            {

                var q = db3.tbl_user.ToList().Where(a => miladidt.Date == (a.signupdate.HasValue == true ? a.signupdate.Value.Date : DateTime.Now.AddYears(1000).Date)).OrderByDescending(a => a.id).Select(g => g);
                int number = q.Count();
                var totalPages = (int)Math.Ceiling(number / (float)take);
                ViewBag.totalpages = totalPages;
                ViewBag.pagesize = take;
                ViewBag.skip = skip;
                int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                ViewBag.pagenumber = pagenumber;
                return PartialView("~/Views/Shared/PartialUsersManagment/_partialusers.cshtml", q);
            }
            else
            {
                var q = db3.tbl_user.ToList().Where(a => miladidt.Date == (a.signupdate.HasValue == true ? a.signupdate.Value.Date : DateTime.Now.AddYears(1000).Date) && a.status == status).OrderByDescending(a => a.id).Select(g => g);
                int number = q.Count();
                var totalPages = (int)Math.Ceiling(number / (float)take);
                ViewBag.totalpages = totalPages;
                ViewBag.pagesize = take;
                ViewBag.skip = skip;
                int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                ViewBag.pagenumber = pagenumber;
                return PartialView("~/Views/Shared/PartialUsersManagment/_partialusers.cshtml", q);
            }
        }


        public ActionResult searchbysignupdaterange(string signupdatefrom, string signupdateto, bool? status, int take = 6, int skip = 0)
        {

            TempData["search"] = "3";
            TempData["searchtext"] = "جستجو بر اساس وضعیت حمل : ";
            DateTime dt1 = DateTime.Parse(signupdatefrom);
            DateTime miladidt1 = shamsi.ToMiladi(dt1);

            DateTime dt2 = DateTime.Parse(signupdateto);
            DateTime miladidt2 = shamsi.ToMiladi(dt2);

            if (status == null)
            {

                var q = db3.tbl_user.ToList().Where(a => (miladidt1.Date <= (a.signupdate.HasValue == true ? a.signupdate.Value.Date : DateTime.Now.AddYears(1000).Date) && miladidt2.Date >= (a.signupdate.HasValue == true ? a.signupdate.Value.Date : DateTime.Now.AddYears(1000).Date))).OrderByDescending(a => a.id).Select(g => g);
                int number = q.Count();
                var totalPages = (int)Math.Ceiling(number / (float)take);
                ViewBag.totalpages = totalPages;
                ViewBag.pagesize = take;
                ViewBag.skip = skip;
                int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                ViewBag.pagenumber = pagenumber;
                return PartialView("~/Views/Shared/PartialUsersManagment/_partialusers.cshtml", q);
            }
            else
            {
                var q = db3.tbl_user.ToList().Where(a => (miladidt1.Date <= (a.signupdate.HasValue == true ? a.signupdate.Value.Date : DateTime.Now.AddYears(1000).Date) && miladidt2.Date >= (a.signupdate.HasValue == true ? a.signupdate.Value.Date : DateTime.Now.AddYears(1000).Date)) && a.status == status).OrderByDescending(a => a.id).Select(g => g);
                int number = q.Count();
                var totalPages = (int)Math.Ceiling(number / (float)take);
                ViewBag.totalpages = totalPages;
                ViewBag.pagesize = take;
                ViewBag.skip = skip;
                int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
                ViewBag.pagenumber = pagenumber;
                return PartialView("~/Views/Shared/PartialUsersManagment/_partialusers.cshtml", q);
            }


        }

        public ActionResult getuser(int userid = 0)
        {
            var userselected = db3.tbl_user.Where(a => a.id == userid).ToList().SingleOrDefault();


            return PartialView("_partialsendsms", userselected);
        }


        public ActionResult sendsms(string cellnumber, string smstext)
        {

            Cls_SMS.ClsSend sms_Single = new Cls_SMS.ClsSend();
            string[] ret1 = new string[2];

            ret1 = sms_Single.SendSMS_Single(smstext, cellnumber, "100008001", "koohi8", "87g5820", "http://193.104.22.14:2055/CPSMSService/Access", "KOOHI", false);
            return Json(true);
        }


        public ActionResult getuserforedit(int userid = 0)
        {
            var userselected = db3.tbl_user.Where(a => a.id == userid).ToList().SingleOrDefault();


            return PartialView("~/Views/Shared/PartialUsersManagment/_partialedituser.cshtml", userselected); ;
        }




        public ActionResult edituserbyajax([FromForm]userinfomodelview t, IFormFile image)
        {

            string message = "";
            if (t.password != t.confirmpass)
            {
                message = "کلمه های عبور وارده یکسان نمی باشد.";
                return Json(new { status = "errorinpass", message = message });

            }
            if (ModelState.IsValid)
            {



                try
                {


                    var user = db3.tbl_user.Where(a => a.id == t.id).SingleOrDefault();

                    if (image != null)
                    {

                        //var qTools = db3.Tbl_Tools.OrderByDescending(a => a.ID).FirstOrDefault();

                        if (image.Length <= 524288)
                        {
                            if (image.ContentType.Contains("image"))
                            {



                                user.img = saveimageinsql.perform(image, false);


                            }
                            else
                            {
                                message = "فایل مورد نظر تصویر نیست.";
                                return Json(new { status = "error", message = message });
                            }
                        }
                        else
                        {
                            message = " تصویر بیش از 512 کیلوبایت است.";
                            return Json(new { status = "error", message = message });
                        }
                    }
                    user.Lname = t.Lname;
                    user.name = t.name;
                    user.password = t.password;
                    user.userdescription = t.userdescription;
                    user.username = t.username;
                    user.confirmpass = t.confirmpass;
                    user.Access = t.Access;
                    user.status = t.status;
                    user.cellphone = t.cellphone;

                    db3.SaveChanges();
                    message = "محصول با موفقیت اصلاح گردید.";


                    var useredited = db3.tbl_user.Where(a => a.id == t.id).SingleOrDefault();
                    //return PartialView("~/Views/Shared/PartialUsersManagment/_partialeachuser", useredited);
                    return Json(new { status = "success", message = "اطلاعات مربوط به کاربر مورد نظر با موفقیت ویرایش گردید" });
                }
                catch (Exception ex)
                {


                    message = ex.Message;

                    return Json(new { status = "error", message = message });

                }

            }
            else
            {


                return Json(new { status = "errorvalidation", message = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToList() });

            }


        }

        public ActionResult editproducts_catgbyajax(int catgid = 0)
        {
            var selectedcatg = db3.tbl_category.Where(a => a.id == catgid).SingleOrDefault();
            return PartialView("~/Views/shared/partialviewforproducts/_partialcatgedit.cshtml", selectedcatg);


        }


        public ActionResult Editcatgbyajax(productcategory t, IFormFile image)
        {
            string message = "";

            if (ModelState.IsValid)
            {



                try
                {


                    var catg = db3.tbl_category.Where(a => a.id == t.id).SingleOrDefault();

                    if (image != null)
                    {

                        //var qTools = db3.Tbl_Tools.OrderByDescending(a => a.ID).FirstOrDefault();

                        if (image.Length <= 524288)
                        {
                            if (image.ContentType.Contains("image"))
                            {


                                catg.image = saveimageinsql.perform(image, false);


                            }
                            else
                            {
                                message = "فایل مورد نظر تصویر نیست.";
                                return Json(new { status = "error", message = message });
                            }
                        }
                        else
                        {
                            message = " تصویر بیش از 512 کیلوبایت است.";
                            return Json(new { status = "error", message = message });
                        }
                    }
                    catg.actionname = t.actionname;
                    catg.categoryname = t.categoryname;
                    catg.description = t.description;
                    catg.fulldescription = t.fulldescription;
                    catg.keywords = t.keywords;
                    catg.purchaseonline = t.purchaseonline;
                    catg.section_id = t.section_id;
                    catg.shortname = t.shortname;
                    catg.showprice = t.showprice;
                    catg.status = t.status;
                    catg.weight = t.weight;

                    db3.SaveChanges();
                    message = "بخش با موفقیت اصلاح گردید.";


                    var catgedited = db3.tbl_category.Where(a => a.id == t.id).SingleOrDefault();
                    return PartialView("~/Views/shared/partialviewforproducts/_partialcatg1.cshtml", catgedited);
                }
                catch (Exception ex)
                {


                    message = ex.Message;

                    return Json(new { status = "error", message = message });

                }

            }
            else
            {


                return Json(new { status = "errorvalidation", message = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToList() });

            }
        }

        public ActionResult addproducts_catgbyajax(int sectionid=0)
        {
            ViewBag.sectionid = sectionid;
            return PartialView("~/Views/shared/partialviewforproducts/_partialcatgadd.cshtml", new tbl_category() { });
        }



        public ActionResult addcatgbyajax(productcategory t, IFormFile image)
        {
            string message = "";

            if (ModelState.IsValid)
            {



                try
                {


                    tbl_category catg = new tbl_category();

                    if (image != null)
                    {

                        //var qTools = db3.Tbl_Tools.OrderByDescending(a => a.ID).FirstOrDefault();

                        if (image.Length <= 524288)
                        {
                            if (image.ContentType.Contains("image"))
                            {


                                catg.image = saveimageinsql.perform(image, false);


                            }
                            else
                            {
                                message = "فایل مورد نظر تصویر نیست.";
                                return Json(new { status = "error", message = message });
                            }
                        }
                        else
                        {
                            message = " تصویر بیش از 512 کیلوبایت است.";
                            return Json(new { status = "error", message = message });
                        }
                    }
                    catg.actionname = t.actionname;
                    catg.categoryname = t.categoryname;
                    catg.description = t.description;
                    catg.fulldescription = t.fulldescription;
                    catg.keywords = t.keywords;
                    catg.purchaseonline = t.purchaseonline;
                    catg.section_id = t.section_id;
                    catg.shortname = t.shortname;
                    catg.showprice = t.showprice;
                    catg.status = t.status;
                    catg.weight = t.weight;
                    db3.tbl_category.Add(catg);
                    db3.SaveChanges();



                    var catgadded = db3.tbl_category.Where(a => a.id == catg.id).SingleOrDefault();
                    return PartialView("~/Views/shared/partialviewforproducts/_partialeachcatg.cshtml", catgadded);
                }
                catch (Exception ex)
                {


                    message = ex.Message;

                    return Json(new { status = "error", message = message });

                }

            }
            else
            {


                return Json(new { status = "errorvalidation", message = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToList() });

            }
        }


        public ActionResult deletecatgbyajax(int catgid = 0)
        {
            try
            {


                var catgsel = db3.tbl_category.Where(a => a.id == catgid).SingleOrDefault();
                if (catgsel != null && catgsel.tbl_products.Count() == 0)
                {
                    db3.tbl_category.Remove(catgsel);
                    db3.SaveChanges();
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }

            }
            catch (Exception)
            {

                return Json(false);
            }
        }


        public ActionResult checkconnectionid(string connectionid)
        {
            var record = db3.tbl_tbl_onlineusers.Where(a => a.connection_id == connectionid).SingleOrDefault();
            if (record == null)
            {
                return Json(false);

            }
            else
            {
                return Json(true);
            }

        }

        public ActionResult issupporter(int id = 0)
        {
            var user = db3.tbl_supporterinonlinechat.Where(a => a.user_id == id).SingleOrDefault();
            if (user != null)

            {
                return Json(new { issupporter = true, username = user.user_.username, email = user.user_.email });
            }
            else
            {
                return Json(new { issupporter = false });

            }
        }

        public ActionResult userinfo(int id = 0)
        {
            var user = db3.tbl_user.Where(a => a.id == id).SingleOrDefault();
            if (user != null)

            {
                return Json(new { isuser = true, fname = user.name, username = user.username, email = user.email });
            }
            else
            {
                return Json(new { isuser = false, });

            }
        }


        public async Task<ActionResult> getmsgofonlineuser(int onlineuserid, int supporterid)
        {
            int supporteridinonlineuserstbl = db3.tbl_tbl_onlineusers.Where(a => a.user_.id == supporterid).SingleOrDefault().id;
            string supportername = db3.tbl_supporterinonlinechat.Where(a => a.user_.id == supporterid).SingleOrDefault().supporte_name;


            var q = await db3.tbl_onlinechatmsg.Where(a => (a.onlineuser_id == onlineuserid && a.towhom == supporteridinonlineuserstbl) || (a.onlineuser_id == supporteridinonlineuserstbl && a.towhom == onlineuserid)).ToListAsync();
            var msg = q.Select(g => new { onlineuserid = g.onlineuser_id, uname = g.onlineuser_.user_id.HasValue ? supportername : g.onlineuser_.nameu, text = g.textmsg, id = g.id, senddate = g.sendmsg_date.ToString("HH:mm"), towhom = g.towhom });
            //(uname, msg, newmsg.id, newmsg.sendmsg_date.ToString("HH:mm")
            return Json(msg);

        }

        public ActionResult getthissupporterid(int id = 0)
        {
            var supporter = db3.tbl_tbl_onlineusers.Where(a => a.user_id == id).SingleOrDefault();
            int supporterid = supporter.id;
            return Json(supporterid);

        }



        public async Task<ActionResult> getsystemmessages()
        {

            var data = await db3.tbl_signalRmsg.Where(a => (a.msgcondition.HasValue ? a.msgcondition.Value == 1 : a.id == 0)).ToListAsync();
            var lst = data.Select(g => new { msgid = g.id, systemmsg_text = g.msg_text, changedatetime = (g.datetime_send.Value.ToPersianDate() + "  " + g.datetime_send.Value.ToString("HH:mm:ss")) });




            return Json(lst);
        }
        // managment order form


        public ActionResult manage_order(int take = 6, int skip = 0)
        {
            //var q =await db3.tbl_order.ToListAsync();


            return View();
        }


        public ActionResult showorderform()
        {
            return PartialView("_partialorderform", null);

        }

        public ActionResult showorderformforedit(int orderid = 0)
        {
            var order = db3.tbl_order.Where(a => a.id == orderid).SingleOrDefault();
            return PartialView("_partialorderform", order);
        }

        public ActionResult toshowpartialadditemtoorder()
        {
            return PartialView("_partialadditemtooredr");
        }

        public ActionResult toshowpartialedititemtoorder(int itemid, int itemnumber, string itemname, string pdesc, int row)
        {
            myorderlist m = new myorderlist() { itemid = itemid, itemname = itemname, itemnumber = itemnumber, pdesc = pdesc, row = row };

            return PartialView("_partialedititemtooredr", m);
        }



        public ActionResult addneworder([FromBody]myorder order)
        {
            try
            {
                tbl_order o = new tbl_order();
                o.from_branchid = order.frombranch;
                o.orderforid = order.forperson;
                o.sharh = order.orderdesc;
                try
                {
                    DateTime t1, t2;
                    t2 = DateTime.Now;
                    t1 = DateTime.Parse(order.orderdate);
                    //t3 = t1.AddHours(t2.Hour).AddMinutes(t2.Minute).AddSeconds(t2.Second);
                    //o.sodoor_date = shamsi.ToMiladi(t3);
                    o.sodoor_date = shamsi.PersianDateToGregorianDate(order.orderdate).AddHours(t2.Hour).AddMinutes(t2.Minute).AddSeconds(t2.Second);
                }
                catch (Exception)
                {
                    o.sodoor_date = DateTime.Now;
                }
                o.to_branchid = order.tobranch;
                o.userid = int.Parse(User.Claims.FirstOrDefault().Value);
                o.valid = true;
                db3.tbl_order.Add(o);
                db3.SaveChanges();
                List<tbl_orderdetails> lstitem = new List<tbl_orderdetails>();
                if (order.itemlist != null)
                {

                    foreach (var item in order.itemlist)
                    {
                        tbl_orderdetails t = new tbl_orderdetails();
                        t.number = item.itemnumber;
                        t.order_id = o.id;
                        t.product_id = item.itemid;
                        t.description = item.pdesc;
                        lstitem.Add(t);
                    }
                    db3.tbl_orderdetails.AddRange(lstitem);
                }

                // check cellphone and update if nessecery
                var person = db3.tbl_person.Where(a => a.id == order.forperson).SingleOrDefault();
                if (person.cell != order.cellphone)
                {
                    person.cell = order.cellphone;

                }

                db3.SaveChanges();

                //
                return Json(new { message = "حواله با موفقیت ثبت گردید", orderid = o.id });
            }
            catch (Exception e)
            {

                return Json(new { message = "در ثبت حواله خطایی رخ داده است" });
            }

        }


        public ActionResult editorder([FromBody]myorder order)
        {
            try
            {
                var s = db3.tbl_order.Where(a => a.id == order.orderid).SingleOrDefault();



                s.from_branchid = order.frombranch;
                s.orderforid = order.forperson;
                s.sharh = order.orderdesc;
                DateTime tnow = DateTime.Now;
                s.sodoor_date = shamsi.PersianDateToGregorianDate(order.orderdate).AddHours(tnow.Hour).AddMinutes(tnow.Minute).AddSeconds(tnow.Second);
                s.to_branchid = order.tobranch;
                s.userid = int.Parse(User.Claims.FirstOrDefault().Value);

                db3.SaveChanges();
                var sitemlist = db3.tbl_orderdetails.Where(a => a.order_id == order.orderid.Value);
                db3.tbl_orderdetails.RemoveRange(sitemlist);
                db3.SaveChanges();
                List<tbl_orderdetails> lstitem = new List<tbl_orderdetails>();
                if (order.itemlist != null)
                {

                    foreach (var item in order.itemlist)
                    {
                        tbl_orderdetails t = new tbl_orderdetails();
                        t.number = item.itemnumber;
                        t.order_id = s.id;
                        t.product_id = item.itemid;
                        t.description = item.pdesc;
                        lstitem.Add(t);
                    }
                    db3.tbl_orderdetails.AddRange(lstitem);
                }


                // update cellphone for person

                var person = db3.tbl_person.Where(a => a.id == order.forperson).SingleOrDefault();
                if (person.cell != order.cellphone)
                {
                    person.cell = order.cellphone;

                }

                //

                db3.SaveChanges();

                return Json(new { message = "حواله با موفقیت به روز رسانی گردید" });
            }
            catch (Exception e)
            {

                return Json(new { message = e.InnerException });
            }

        }


        public ActionResult addnewperson(string fullname)
        {
            tbl_person p = new tbl_person() { Fname = fullname };
            db3.tbl_person.Add(p);
            db3.SaveChanges();
            return Json(p.id);
        }



        public ActionResult getorderdetails(int orderid = 0)
        {
            var q = db3.tbl_orderdetails.Where(a => a.order_id == orderid).ToList().Select(g => g);
            return PartialView("_porderdetails", q);
        }
        [AllowAnonymous]
        public ActionResult changevalidationoforder(int orderid = 0)
        {
            var order = db3.tbl_order.Where(a => a.id == orderid).SingleOrDefault();
            bool result;
            if (order.done == true)
            {


                return Json(new { msg = "notallowed" });
            }
            else
            {
                if (order.valid == true)
                {
                    order.valid = false;
                }
                else
                {
                    order.valid = true;
                }

                db3.SaveChanges();
                result = order.valid.Value;

                return Json(new { result = result, msg = "allowed" });
            }



        }



        // to show deliver order form 
        [AllowAnonymous]
        public ActionResult showdeliverorderform(int orderid)
        {
            var order = db3.tbl_order.Where(a => a.id == orderid).SingleOrDefault();
            if (order.valid == false)
            {
                return Json(new { err = "cancelled", errmsg = "این حواله باطل شده است" });
            }
            else
            {

                deliverorderinfo d = new deliverorderinfo();

                d.orderid = order.id;
                d.deliverdate = order.done_datetime.HasValue ? order.done_datetime.Value.ToPersianDate() : DateTime.Now.ToPersianDate();
                d.userid = order.done_userid ?? null;
                d.username = order.done_userid.HasValue ? (order.user.username) : "";
                d.done = order.done.ToString();
                d.deliverorderdesc = order.done_description;





                return PartialView("_partialdeliverorder", d);
            }
        }
        [AllowAnonymous]
        public ActionResult savedeliverorderinfo([FromBody] deliverorderinfo d)
        {
            var order = db3.tbl_order.Where(a => a.id == d.orderid).SingleOrDefault();
            // update deliver info of this order
            order.done = bool.Parse(d.done);
            if (bool.Parse(d.done) == true)
            {
                order.done_userid = d.userid;
                order.done_datetime = shamsi.ToMiladi(DateTime.Parse(d.deliverdate));
                order.done_description = d.deliverorderdesc;
            }
            else
            {
                order.done_userid = null;
                order.done_datetime = null;
                order.done_description = null;
            }


            db3.SaveChanges();


            return Json(new { orderid = d.orderid, done = d.done, donemsg = (d.done == "true" ? "تحویل مشتری" : "عدم تحویل") });
        }




        //refresh listkalatable
        public async Task<ActionResult> Getlastlistitem(string searchitem = "", int skip = 0, int take = 10)
        {
            var q = db3.tbl_listkala97.Where(a => a.product.name.Contains(searchitem) || a.product.category.categoryname.Contains(searchitem) || a.product.codename.Contains(searchitem));
            int number = q.Count();
            var totalPages = (int)Math.Ceiling(number / (float)take);
            ViewBag.totalpages = totalPages;
            ViewBag.pagesize = take;
            ViewBag.skip = skip;
            int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
            ViewBag.pagenumber = pagenumber;
            var productsQuery = await q.OrderByDescending(a => a.id).Skip(skip).Take(take).Select(a => new ItemListKala
            {
                id = a.id,
                sodoordate = a.sodoordate.ToPersianDate(),
                time = a.time,
                htype = a.htypeNavigation.name,
                anbarid = a.anbarid,
                anbarname = a.anbar.anbarname,
                productid = a.productid,
                category = a.product.category.categoryname,
                name = a.product.name + " " + a.product.dimension + " ::: [" + a.productid + "]",
                code = a.product.codename,
                kalanumber = a.kalanumber,
                cost = a.kalacost,
                totalcostkala = a.totalcost,
                kaladescription = a.kaladescription == null ? string.Empty : a.kaladescription,
                user = a.username.username,
                kalanumberm = a.kalanumberm
            }).ToListAsync();
            return PartialView("~/Views/Shared/Partiallistkala/_tablebodylistkala.cshtml", productsQuery);
        }


        // manage listkala by ajax

        public ActionResult reorderlistkala(List<int> model, int skip = 0, int take = 0)
        {
            var q = db3.tbl_listkala97.Where(a => model.Contains(a.id)).OrderByDescending(a => a.id);
            int number = q.Count();
            var totalPages = (int)Math.Ceiling(number / (float)take);
            ViewBag.totalpages = totalPages;
            ViewBag.pagesize = take;
            ViewBag.skip = skip;
            int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
            ViewBag.pagenumber = pagenumber;
            var productsQuery = q.OrderByDescending(a => a.id).Skip(skip).Take(take).Select(a => new ItemListKala
            {
                id = a.id,
                sodoordate = a.sodoordate.ToPersianDate(),
                time = a.time,
                htype = a.htypeNavigation.name,
                anbarid = a.anbarid,
                anbarname = a.anbar.anbarname,
                productid = a.productid,
                category = a.product.category.categoryname,
                name = a.product.name + " " + a.product.dimension + " ::: [" + a.productid + "]",
                code = a.product.codename,
                kalanumber = a.kalanumber,
                cost = a.kalacost,
                totalcostkala = a.totalcost,
                kaladescription = a.kaladescription == null ? string.Empty : a.kaladescription,
                user = a.username.username,
                kalanumberm = a.kalanumberm
            });
            return PartialView("~/Views/Shared/Partiallistkala/_tablebodylistkala.cshtml", productsQuery);

        }

        public ActionResult searchlistkalabysabtdate(string sabtdate, int take)
        {
            var AllRecords = db3.tbl_listkala97.ToList().OrderByDescending(a => a.id);
            DateTime thisdate = (sabtdate == null ? DateTime.Now : shamsi.PersianDateToGregorianDate(sabtdate));
            var MyRecords = AllRecords.Where(a => a.sodoordate.Date == thisdate.Date);
            int number = MyRecords.Count();
            var totalPages = (int)Math.Ceiling(number / (float)take);
            ViewBag.totalpages = totalPages;
            ViewBag.pagesize = take;
            var q = MyRecords.Select(a => new ItemListKala
            {
                id = a.id,
                sodoordate = a.sodoordate.ToPersianDate(),
                time = a.time,
                htype = a.htypeNavigation.name,
                anbarid = a.anbarid,
                anbarname = a.anbar.anbarname,
                productid = a.productid,
                category = a.product.category.categoryname,
                name = a.product.name + " " + a.product.dimension + " ::: [" + a.productid + "]",
                code = a.product.codename,
                kalanumber = a.kalanumber,
                cost = a.kalacost,
                totalcostkala = a.totalcost,
                kaladescription = a.kaladescription == null ? string.Empty : a.kaladescription,
                user = a.username.username,
                kalanumberm = a.kalanumberm

            }

            );



            return PartialView("~/Views/Shared/Partiallistkala/_TableListKala.cshtml", q);
        }

        // advancedserach in listkala


        public ActionResult advancedsearchlistkal(string sabtdate, int productid, int anbarid, int havalehtypeid, int usernameid, string desc, int take)
        {
            var MyRecords = _ProductsServices.AdvancedSearchListkala(sabtdate, productid, anbarid, havalehtypeid, usernameid, desc);

            int number = MyRecords.Count();
            var totalPages = (int)Math.Ceiling(number / (float)take);
            ViewBag.totalpages = totalPages;
            ViewBag.pagesize = take;
            var q = MyRecords.Select(a => new ItemListKala
            {
                id = a.id,
                sodoordate = a.sodoordate.ToPersianDate(),
                time = a.time,
                htype = a.htypeNavigation.name,
                anbarid = a.anbarid,
                anbarname = a.anbar.anbarname,
                productid = a.productid,
                category = a.product.category.categoryname,
                name = a.product.name + " " + a.product.dimension + " ::: [" + a.productid + "]",
                code = a.product.codename,
                kalanumber = a.kalanumber,
                cost = a.kalacost,
                totalcostkala = a.totalcost,
                kaladescription = a.kaladescription == null ? string.Empty : a.kaladescription,
                user = a.username.username,
                kalanumberm = a.kalanumberm

            }

            );



            return PartialView("~/Views/Shared/Partiallistkala/_TableListKala.cshtml", q);
        }

        // search listkala by product name
        public ActionResult searchlistkalbyproductname(int productid, int take)
        {
            var AllRecords = db3.tbl_listkala97.ToList().OrderByDescending(a => a.id);

            var MyRecords = AllRecords.Where(a => a.productid == productid);
            int number = MyRecords.Count();
            var totalPages = (int)Math.Ceiling(number / (float)take);
            ViewBag.totalpages = totalPages;
            ViewBag.pagesize = take;
            var q = MyRecords.Select(a => new ItemListKala
            {
                id = a.id,
                sodoordate = a.sodoordate.ToPersianDate(),
                time = a.time,
                htype = a.htypeNavigation.name,
                anbarid = a.anbarid,
                anbarname = a.anbar.anbarname,
                productid = a.productid,
                category = a.product.category.categoryname,
                name = a.product.name + " " + a.product.dimension + " ::: [" + a.productid + "]",
                code = a.product.codename,
                kalanumber = a.kalanumber,
                cost = a.kalacost,
                totalcostkala = a.totalcost,
                kaladescription = a.kaladescription == null ? string.Empty : a.kaladescription,
                user = a.username.username,
                kalanumberm = a.kalanumberm

            }

            );



            return PartialView("~/Views/Shared/Partiallistkala/_TableListKala.cshtml", q);
        }





        // search inventory by searchlistkalabysabtdate

        public ActionResult searchinventorysbysectioncategory(int section_id, int category_id, int take)

        {

            var AllRecords = db3.tbl_listkala97.OrderByDescending(a => a.id);
            var MyRecordsinfirstfilter = (section_id == 0 ? AllRecords : AllRecords.Where(a => a.product.category.section_.id == section_id));
            var MyRecordsinSecondfilter = (category_id == 0 ? MyRecordsinfirstfilter : MyRecordsinfirstfilter.Where(a => a.product.categoryid == category_id));
            int number = MyRecordsinSecondfilter.Count();
            var totalPages = (int)Math.Ceiling(number / (float)take);
            ViewBag.totalpages = totalPages;
            ViewBag.pagesize = take;
            var InventoryQ = MyRecordsinSecondfilter.ToList()
                .GroupBy(a => new { category_id = a.product.categoryid, category_name = a.product.category.categoryname })
                .Select(g => new ShowAbstractInventory
                {
                    Id = g.Key.category_id,
                    Name = g.Key.category_name,
                    Inventory = g.Sum(d => d.kalanumberm)
                });



            //var inventorylist = InventoryQ.ToList();
            return PartialView("~/Views/Shared/Partiallistkala/_Tableinventory.cshtml", InventoryQ);

        }

        public ActionResult productstoorderreports(int section_id, int category_id, int take)

        {

            var AllRecords = db3.tbl_listkala97.OrderByDescending(a => a.id);
            var MyRecordsinfirstfilter = (section_id == 0 ? AllRecords : AllRecords.Where(a => a.product.category.section_.id == section_id));
            var MyRecordsinSecondfilter = (category_id == 0 ? MyRecordsinfirstfilter : MyRecordsinfirstfilter.Where(a => a.product.categoryid == category_id));
            var MyRecordsin3rdfilter = MyRecordsinSecondfilter.ToList()
                 .GroupBy(a => new { productid = a.productid, productname = a.product.name, dim = a.product.dimension, mininventory = a.product.inventory, codename = a.product.codename, catgid = a.product.categoryid, catgname = a.product.category.categoryname })
                 .Select(g => new inventoryTable
                 {
                     id = g.Key.productid,
                     productname = g.Key.productname + " | " + g.Key.dim,
                     catgid = g.Key.catgid,
                     catgname = g.Key.catgname,
                     inventory = g.Sum(a => a.kalanumberm),
                     mininventory = g.Key.mininventory.HasValue ? g.Key.mininventory.Value : 0,

                 }).OrderBy(a => a.code).Select(h => h);
            var myrecord = MyRecordsin3rdfilter.Where(a => a.inventory < a.mininventory);

            int number = myrecord.Count();
            var totalPages = (int)Math.Ceiling(number / (float)take);
            ViewBag.totalpages = totalPages;
            ViewBag.pagesize = take;
            var InventoryQ = myrecord.ToList()
                .GroupBy(a => new { category_id = a.catgid, category_name = a.catgname })
                .Select(g => new ShowAbstracttoorder
                {
                    Id = g.Key.category_id,
                    Name = g.Key.category_name,
                    numberofproducttoorder = g.Count()
                });



            //var inventorylist = InventoryQ.ToList();
            return PartialView("~/Views/Shared/Partiallistkala/_TableToOrder.cshtml", InventoryQ);

        }
        public ActionResult GetProductsToOrderReport(int catgid = 0)
        {
            var InventoryQ = db3.tbl_listkala97.Where(a => a.product.categoryid == catgid).ToList()
                 .GroupBy(a => new { productid = a.productid, productname = a.product.name, dim = a.product.dimension, mininventory = a.product.inventory, codename = a.product.codename, catgid = a.product.categoryid, catgname = a.product.category.categoryname })
                 .Select(g => new inventoryTable
                 {
                     id = g.Key.productid,
                     productname = g.Key.productname + " | " + g.Key.dim,
                     catgid = g.Key.catgid,
                     catgname = g.Key.catgname,
                     inventory = g.Sum(a => a.kalanumberm),
                     mininventory = g.Key.mininventory.HasValue ? g.Key.mininventory.Value : 0,
                     code = g.Key.codename,

                 }).Where(a => a.inventory < a.mininventory).OrderBy(a => a.inventory).Select(h => h);

            return Json(InventoryQ);
        }
        public ActionResult GetInventoryReportOfProducts(int catgid = 0)
        {
            var InventoryQ = db3.tbl_listkala97.Where(a => a.product.categoryid == catgid).ToList()
                 .GroupBy(a => new { productid = a.productid, productname = a.product.name, dim = a.product.dimension, mininventory = a.product.inventory, codename = a.product.codename, catgid = a.product.categoryid, catgname = a.product.category.categoryname })
                 .Select(g => new inventoryTable
                 {
                     id = g.Key.productid,
                     productname = g.Key.productname + " | " + g.Key.dim,
                     catgid = g.Key.catgid,
                     catgname = g.Key.catgname,
                     inventory = g.Sum(a => a.kalanumberm),
                     mininventory = g.Key.mininventory.HasValue ? g.Key.mininventory.Value : 0,
                     code = g.Key.codename,
                     lastBuyDate = g.Where(a => a.htype == 1).Count() != 0 ? g.Where(a => a.htype == 1).OrderByDescending(a => a.sodoordate).FirstOrDefault().sodoordate.ToPersianDate() : "",
                     lastSellDate = g.Where(a => a.htype == -1).Count() != 0 ? g.Where(a => a.htype == -1).OrderByDescending(a => a.sodoordate).FirstOrDefault().sodoordate.ToPersianDate() : "",
                     lastBuyPrice = g.Where(a => a.htype == 1).Count() != 0 ? g.Where(a => a.htype == 1).OrderByDescending(a => a.sodoordate).FirstOrDefault().kalacost.ToString() : "",
                     lastSellPrice = g.Where(a => a.htype == -1).Count() != 0 ? g.Where(a => a.htype == -1).OrderByDescending(a => a.sodoordate).FirstOrDefault().kalacost.ToString() : "",
                 }).OrderBy(a => a.code).Select(h => h);

            return Json(InventoryQ);
        }

        // to expand and show inventory of each product in every warehouse
        public ActionResult GetDetailAboutIneventory(int productid)
        {
            var Q = db3.tbl_listkala97.Where(a => a.productid == productid).ToList()
                 .GroupBy(a => new { productid = a.productid, anbarid = a.anbarid, anbarname = a.anbar.anbarname })
                 .Select(g => new
                 {
                     id = g.Key.productid,
                     anbarid = g.Key.anbarid,
                     anbarname = g.Key.anbarname,
                     inventory = g.Sum(a => a.kalanumberm),

                 }).OrderBy(d => d.anbarid).Select(d => d);


            return Json(Q);
        }


        // search by listkalaid
        public ActionResult searchbylistkalaid(int id, int take)
        {
            var AllRecords = db3.tbl_listkala97.ToList().OrderByDescending(a => a.id);
            var MyRecords = AllRecords.Where(a => a.id == id);
            int number = MyRecords.Count();
            var totalPages = (int)Math.Ceiling(number / (float)take);
            ViewBag.totalpages = totalPages;
            ViewBag.pagesize = take;
            var q = MyRecords.Select(a => new ItemListKala
            {
                id = a.id,
                sodoordate = a.sodoordate.ToPersianDate(),
                time = a.time,
                htype = a.htypeNavigation.name,
                anbarid = a.anbarid,
                anbarname = a.anbar.anbarname,
                productid = a.productid,
                category = a.product.category.categoryname,
                name = a.product.name + " " + a.product.dimension + " ::: [" + a.productid + "]",
                code = a.product.codename,
                kalanumber = a.kalanumber,
                cost = a.kalacost,
                totalcostkala = a.totalcost,
                kaladescription = a.kaladescription == null ? string.Empty : a.kaladescription,
                user = a.username.username,
                kalanumberm = a.kalanumberm

            }

            );



            return PartialView("~/Views/Shared/Partiallistkala/_TableListKala.cshtml", q);
        }


        public ActionResult ShowAddItemFormModal()
        {
            return PartialView("~/Views/Shared/Partiallistkala/_partialtoaddkalatolistkala.cshtml");
        }

        public ActionResult ShowEditItemFormModal(int id = 0)
        {
            var q = db3.tbl_listkala97.Where(a => a.id == id).SingleOrDefault();
            return PartialView("~/Views/Shared/Partiallistkala/_partialtoaddkalatolistkala.cshtml", q);
        }


        public ActionResult Removeitemfromlistkala(int id = 0)
        {
            var q = db3.tbl_listkala97.Where(a => a.id == id).SingleOrDefault();
            db3.tbl_listkala97.Remove(q);
            db3.SaveChanges();
            return Json(q.id);

        }





        public ActionResult toclosesalemali()
        {

            //    var lastprice = db3.tbl_listkala.OrderByDescending(a => a.sodoordate).ToList().GroupBy(a => new { a.productid, a.anbar.anbarname, a.product.name, a.anbarid }).Select(g2 => new
            //    {
            //        productid = g2.Key.productid,
            //        productname = g2.Key.name,
            //        anbar = g2.Key.anbarname,

            //        lastprice = g2.OrderByDescending(b => b.sodoordate).Where(b => b.htype == 1).Count() == 0 ? 0 : g2.OrderByDescending(b => b.sodoordate).Where(b => b.htype == 1).FirstOrDefault().kalacost,

            //        lastdate = g2.OrderByDescending(b => b.sodoordate).Where(b => b.htype == 1).Count() == 0 ? " " : g2.OrderByDescending(b => b.sodoordate).Where(b => b.htype == 1).FirstOrDefault().sodoordate.ToPersianDate(),

            //    }).ToList();




            var q = db3.tbl_listkala.GroupBy(a => new { a.productid, a.anbarid }).Select(g => new
            {
                productid = g.Key.productid,
                anbarid = g.Key.anbarid,
                number = g.Sum(a => a.kalanumberm),
                lastcost = g.Where(a => a.htype == 1).OrderByDescending(a => a.id).Take(1).SingleOrDefault().kalacost,

                cost = g.Sum(a => a.totalcost)



            }).Where(d => d.number > 0).ToList();

            var q2 = q.ToList().Select(a => new
            {
                productid = a.productid,
                anbarid = a.anbarid,
                number = a.number,
                lastcost = a.lastcost,
                totalcost = a.number * a.lastcost

            });
            List<tbl_listkala97> listkala = new List<tbl_listkala97>();
            q2.ToList().ForEach(a =>
            {

                listkala.Add(new tbl_listkala97
                {
                    productid = a.productid,
                    kalanumber = a.number,
                    kalacost = a.lastcost,
                    kaladescription = "انتقال از سال قبل",
                    totalcost = a.totalcost,
                    sodoordate = DateTime.Now,
                    usernameid = 78,
                    htype = 1,
                    kalanumberm = a.number,
                    anbarid = a.anbarid,
                    time = DateTime.Now.ToShortTimeString()

                });




            });

            db3.tbl_listkala97.AddRange(listkala);
            db3.SaveChanges();


            return Json(q);
        }


        public ActionResult Get_Inventory(int productid = 0)
        {
            int Inventory = db3.tbl_listkala97.Where(a => a.productid == productid).Sum(a => a.kalanumberm);
            int MinInventory = db3.tbl_products.Where(a => a.idproduct == productid).SingleOrDefault().inventory.HasValue ? db3.tbl_products.Where(a => a.idproduct == productid).SingleOrDefault().inventory.Value : 0;
            productsrepository per = new productsrepository();
            var q = per.Get_InventoryofProductByIDineachanbar(productid);

            return Json(new { inventory = Inventory, minInventory = MinInventory, inventorylist = q });



        }

        public ActionResult Saveitemtolistkala([FromBody] ItemListKalatoadd t)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                try
                {

                    if (t.htype > 0)
                    {
                        t.kalanumberm = t.kalanumber;
                    }
                    else
                    {
                        t.kalanumberm = -1 * t.kalanumber;
                    }
                    if (t.id == null)
                    {
                        // Add Mode
                        tbl_listkala97 R = new tbl_listkala97();
                        R.kalacost = t.kalacost;
                        R.htype = t.htype;
                        R.htypeNavigation = db3.tbl_havaletype.Find(t.htype);
                        R.kaladescription = t.kaladescription;
                        R.anbarid = t.anbarid;
                        R.anbar = db3.tbl_anbar.Find(t.anbarid);
                        R.kalanumber = t.kalanumber;
                        R.kalanumberm = t.kalanumberm;
                        R.productid = t.productid;
                        R.sodoordate = (t.sodoordate == null ? DateTime.Now : shamsi.PersianDateToGregorianDate(t.sodoordate));
                        R.time = DateTime.Now.ToShortTimeString();
                        R.totalcost = t.totalcost;

                        R.usernameid = _userServices.GetUseridByUsername(User.Identity.Name);
                        //R.username = _userServices.GetUserByUsername(t.username);
                        R.product = db3.tbl_products.Find(t.productid);
                        db3.tbl_listkala97.Add(R);

                        db3.SaveChanges();
                        tbl_listkala97 q = db3.tbl_listkala97.Find(R.id);
                        message = "عملیات با موفقیت انجام شد";
                        return Json(new
                        {
                            status = "ADDMODE",
                            message = message,
                            id = q.id,
                            sodoordate = q.sodoordate.ToPersianDate(),
                            time = q.time,
                            htype = q.htypeNavigation.name,
                            anbarid = q.anbarid,
                            anbarname = q.anbar.anbarname,
                            productid = q.productid,
                            category = q.product.category.categoryname,
                            name = q.product.name + " " + q.product.dimension + " ::: [" + q.productid + "]",
                            code = q.product.codename,
                            kalanumber = q.kalanumber,
                            cost = string.Format("{0:#,##0.##}", q.kalacost),
                            totalcostkala = string.Format("{0:#,##0.##}", q.totalcost),
                            kaladescription = q.kaladescription == null ? string.Empty : q.kaladescription,
                            user = User.Identity.Name,
                            kalanumberm = q.kalanumberm,


                        });



                    }
                    else
                    {
                        // Edit Mode
                        var q = db3.tbl_listkala97.Where(a => a.id == t.id).SingleOrDefault();
                        q.htype = t.htype;
                        q.kalacost = t.kalacost;
                        q.anbarid = t.anbarid;
                        q.kaladescription = t.kaladescription;
                        q.kalanumber = t.kalanumber;
                        q.kalanumberm = t.kalanumberm;
                        q.productid = t.productid;
                        q.sodoordate = shamsi.PersianDateToGregorianDate(t.sodoordate);
                        q.totalcost = t.totalcost;
                        q.usernameid = _userServices.GetUseridByUsername(User.Identity.Name);
                        db3.SaveChanges();
                        message = "عملیات با موفقیت انجام شد";
                        return Json(new
                        {
                            status = "EDITMODE",
                            message = message,
                            id = q.id,
                            sodoordate = q.sodoordate.ToPersianDate(),
                            time = q.time,
                            htype = q.htypeNavigation.name,
                            anbarid = q.anbarid,
                            anbarname = q.anbar.anbarname,
                            productid = q.productid,
                            category = q.product.category.categoryname,
                            name = q.product.name + " " + q.product.dimension + " ::: [" + q.productid + "]",
                            code = q.product.codename,
                            kalanumber = q.kalanumber,
                            cost = string.Format("{0:#,##0.##}", q.kalacost),
                            totalcostkala = string.Format("{0:#,##0.##}", q.totalcost),
                            kaladescription = q.kaladescription == null ? string.Empty : q.kaladescription,
                            user = q.username.username,
                            kalanumberm = q.kalanumberm,


                        });







                    }








                }
                catch (Exception ex)
                {

                    message = ex.Message;

                    return Json(new { status = "error", message = message });
                }

            }
            else
            {
                return Json(new { status = "errorvalidation", message = "کلیه فیلدها باید به درستی تکمیل شوند. " });

            }


        }

        public ActionResult Getcategory(int section_id)
        {
            var q = db3.tbl_category.Where(a => a.section_id == section_id && (a.status.HasValue ? a.status.Value == true : 1 == 2)).ToList().Select(g => new { id = g.id, catgname = g.categoryname });
            return Json(q);
        }

        public ActionResult MRecruitment()
        {
            var applist = db3.tbl_applicants;
            ViewBag.skip = 0;
            ViewBag.take = 10;
            int take = 10;
            //ViewBag.totalpages = 1;

            var Query = applist.Select(a => new applicantstructs
            {
                app_id = a.applicant_id,
                birthday = a.birthday.HasValue ? a.birthday.Value.ToPersianDate() : "",
                cellnumber = a.cellnumber,
                codemelli = a.codemelli,
                f_name = a.f_name,
                l_name = a.l_name,
                sabtdate = a.sabtdate.HasValue ? a.sabtdate.Value.ToPersianDate() : "",
                sex = a.sex.HasValue ? (a.sex.Value == 1 ? "مذکر" : "مونث") : "",
                Jobname = a.job_.jobtitle,


            }).OrderByDescending(a => a.app_id).Skip(0).Take(1000);
            int number = Query.Count();
            var totalPages = (int)Math.Ceiling(number / (float)take);
            ViewBag.totalpages = totalPages;
            TempData["firstpage"] = 1;
            return View(Query);

        }


        public ActionResult GetHistoryofAppconnect(int appid = 0)
        {
            var records = db3.tbl_historyappconnect.Where(a => a.app_id == appid).ToList().Select(a =>
            new
            {
                id = a.id,
                appid = a.app_id,
                howtoconnect = a.howtoconnect,
                user = a.user.username,
                date = a.sabtdate.HasValue ? a.sabtdate.Value.ToPersianDate() : "",
                time = a.sabtdate.HasValue ? a.sabtdate.Value.ToShortTimeString() : "",
                result = a.result


            });
            return Json(records);
        }
        public ActionResult toshowtoolsband(bool navigation, bool print, bool search, bool refresh)
        {
            return PartialView("~/Views/Shared/Partiallistkala/_Toolsband.cshtml", new Toolsbar() { Navigation = navigation, print = print, Search = search, Refresh = refresh });
        }



        // manage listkala by ajax

        public ActionResult reorderlistapplicants(List<int> model, int skip = 0, int take = 0)
        {
            var q = db3.tbl_applicants.Where(a => model.Contains(a.applicant_id)).OrderByDescending(a => a.applicant_id);
            int number = q.Count();
            var totalPages = (int)Math.Ceiling(number / (float)take);
            ViewBag.totalpages = totalPages;
            ViewBag.pagesize = take;
            ViewBag.skip = skip;
            int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
            ViewBag.pagenumber = pagenumber;
            var productsQuery = q.OrderByDescending(a => a.applicant_id).Skip(skip).Take(take).Select(a => new applicantstructs
            {
                app_id = a.applicant_id,
                birthday = a.birthday.HasValue ? a.birthday.Value.ToPersianDate() : "",
                cellnumber = a.cellnumber,
                codemelli = a.codemelli,
                f_name = a.f_name,
                l_name = a.l_name,
                sabtdate = a.sabtdate.HasValue ? a.sabtdate.Value.ToPersianDate() : "",
                sex = a.sex.HasValue ? (a.sex.Value == 1 ? "مذکر" : "مونث") : "",
                Jobname = a.job_.jobtitle,


            });
            return PartialView("~/Views/Shared/PartialforRecruitment/_Tablebody.cshtml", productsQuery);

        }

        public ActionResult ShowFormForHistoryApp(int appid = 0)
        {
            ViewBag.appid = appid;
            return PartialView("~/Views/Shared/PartialforRecruitment/_partialtoshowformforhistoryapp.cshtml");
        }

        public ActionResult Saveiteminhistoryappconnect([FromBody] historyapp t)
        {

            string message = "";
            if (ModelState.IsValid)
            {
                try
                {
                    if (t.id == null)
                    {
                        // Add Mode
                        tbl_historyappconnect R = new tbl_historyappconnect();
                        R.app_ = db3.tbl_applicants.Find(t.app_id);
                        R.app_id = t.app_id;
                        R.howtoconnect = t.howtoconnect;
                        R.result = t.result;
                        R.sabtdate = (t.sabtdate == null ? DateTime.Now : shamsi.ToMiladi(DateTime.Parse(t.sabtdate)));
                        R.time = DateTime.Now.ToShortTimeString();
                        R.user = db3.tbl_user.Find(t.userid);
                        R.userid = t.userid.Value;

                        db3.tbl_historyappconnect.Add(R);

                        db3.SaveChanges();
                        tbl_historyappconnect q = db3.tbl_historyappconnect.Find(R.id);
                        message = "عملیات با موفقیت انجام شد";
                        return Json(new
                        {
                            status = "ADDMODE",
                            message = message,
                            id = q.id,
                            app_id = q.app_id,
                            howtoconnect = q.howtoconnect,
                            sabtdate = q.sabtdate.Value.ToPersianDate(),
                            user = q.user.username,
                            result = q.result,
                            time = q.time

                        });



                    }
                    else
                    {
                        // Edit Mode
                        var q = db3.tbl_historyappconnect.Where(a => a.id == t.id).SingleOrDefault();

                        q.app_id = t.app_id;
                        q.howtoconnect = t.howtoconnect;
                        q.result = t.result;
                        q.sabtdate = (t.sabtdate == null ? DateTime.Now : shamsi.ToMiladi(DateTime.Parse(t.sabtdate)));
                        q.time = DateTime.Now.ToShortTimeString();

                        q.userid = t.userid.Value;
                        db3.SaveChanges();
                        message = "عملیات با موفقیت انجام شد";
                        return Json(new
                        {
                            status = "EDITMODE",
                            message = message,
                            id = q.id,
                            app_id = q.app_id,
                            howtoconnect = q.howtoconnect,
                            sabtdate = q.sabtdate.Value.ToPersianDate(),
                            user = q.user.username,
                            result = q.result,
                            time = q.time


                        });







                    }


                }
                catch (Exception ex)
                {

                    message = ex.Message;

                    return Json(new { status = "error", message = message });
                }

            }
            else
            {
                return Json(new { status = "errorvalidation", message = "کلیه فیلدها باید به درستی تکمیل شوند. " });

            }

        }
        public ActionResult removehistoryofapp(List<int> selectedidlist)
        {
            var q = db3.tbl_historyappconnect.Where(a => selectedidlist.Contains(a.id)).ToList();
            db3.tbl_historyappconnect.RemoveRange(q);
            db3.SaveChanges();
            return Json(true);
        }

        // show form to edit app history connect
        public ActionResult ShowFormForHistoryApptoEdit(int id = 0)
        {
            var q = db3.tbl_historyappconnect.Where(a => a.id == id).SingleOrDefault();
            ViewBag.appid = q.app_id;
            return PartialView("~/Views/Shared/PartialforRecruitment/_partialtoshowformforhistoryapp.cshtml", q);


        }
        public ActionResult searchApplicantsappid(int app_id, int take = 10)
        {
            var AllRecords = db3.tbl_applicants.ToList().OrderByDescending(a => a.applicant_id);
            var MyRecords = AllRecords.Where(a => a.applicant_id == app_id);
            int number = MyRecords.Count();
            var totalPages = (int)Math.Ceiling(number / (float)take);
            ViewBag.totalpages = totalPages;
            ViewBag.pagesize = take;
            var q = MyRecords.Select(a => new applicantstructs
            {
                app_id = a.applicant_id,
                birthday = a.birthday.HasValue ? a.birthday.Value.ToPersianDate() : "",
                cellnumber = a.cellnumber,
                codemelli = a.codemelli,
                f_name = a.f_name,
                l_name = a.l_name,
                sabtdate = a.sabtdate.HasValue ? a.sabtdate.Value.ToPersianDate() : "",
                sex = a.sex.HasValue ? (a.sex.Value == 1 ? "مذکر" : "مونث") : "",
                Jobname = a.job_.jobtitle,

            }

            );



            return PartialView("~/Views/Shared/PartialforRecruitment/_Table.cshtml", q);

        }

        public ActionResult searchApplicantsbyjob(int jobid, int take = 10)
        {
            var AllRecords = db3.tbl_applicants.ToList().OrderByDescending(a => a.applicant_id);
            var MyRecords = (jobid != 0 ? AllRecords.Where(a => a.job_id == jobid) : AllRecords);
            int number = MyRecords.Count();
            var totalPages = (int)Math.Ceiling(number / (float)take);
            ViewBag.totalpages = totalPages;
            ViewBag.pagesize = take;
            var q = MyRecords.Select(a => new applicantstructs
            {
                app_id = a.applicant_id,
                birthday = a.birthday.HasValue ? a.birthday.Value.ToPersianDate() : "",
                cellnumber = a.cellnumber,
                codemelli = a.codemelli,
                f_name = a.f_name,
                l_name = a.l_name,
                sabtdate = a.sabtdate.HasValue ? a.sabtdate.Value.ToPersianDate() : "",
                sex = a.sex.HasValue ? (a.sex.Value == 1 ? "مذکر" : "مونث") : "",
                Jobname = a.job_.jobtitle,

            }

            );



            return PartialView("~/Views/Shared/PartialforRecruitment/_Table.cshtml", q);

        }
        public ActionResult searchappbyconnectionsataus(int connectionsataus, int take = 10)
        {
            var AllRecords = db3.tbl_applicants.ToList().OrderByDescending(a => a.applicant_id);
            var MyRecords = (connectionsataus == 0 ? AllRecords : (connectionsataus == 1 ? AllRecords.Where(a => a.tbl_historyappconnect.Count() > 0) : AllRecords.Where(a => a.tbl_historyappconnect.Count() == 0)));
            int number = MyRecords.Count();
            var totalPages = (int)Math.Ceiling(number / (float)take);
            ViewBag.totalpages = totalPages;
            ViewBag.pagesize = take;
            var q = MyRecords.Select(a => new applicantstructs
            {
                app_id = a.applicant_id,
                birthday = a.birthday.HasValue ? a.birthday.Value.ToPersianDate() : "",
                cellnumber = a.cellnumber,
                codemelli = a.codemelli,
                f_name = a.f_name,
                l_name = a.l_name,
                sabtdate = a.sabtdate.HasValue ? a.sabtdate.Value.ToPersianDate() : "",
                sex = a.sex.HasValue ? (a.sex.Value == 1 ? "مذکر" : "مونث") : "",
                Jobname = a.job_.jobtitle,

            }

            );



            return PartialView("~/Views/Shared/PartialforRecruitment/_Table.cshtml", q);

        }

        public ActionResult searchappsbysabtdaterange(string sabtdatefrom, string sabtdateto, int take = 10)
        {
            var AllRecords = db3.tbl_applicants.ToList().OrderByDescending(a => a.applicant_id);
            DateTime fromdate = (sabtdatefrom == "" ? DateTime.Now.AddYears(-1000).Date : shamsi.ToMiladi(DateTime.Parse(sabtdatefrom)));
            DateTime todate = (sabtdateto == "" ? DateTime.Now.Date : shamsi.ToMiladi(DateTime.Parse(sabtdateto)));
            var MyRecords = AllRecords.Where(a => a.sabtdate.Value.Date >= fromdate.Date && a.sabtdate.Value.Date < todate.Date).Select(g => g);
            int number = MyRecords.Count();
            var totalPages = (int)Math.Ceiling(number / (float)take);
            ViewBag.totalpages = totalPages;
            ViewBag.pagesize = take;
            var q = MyRecords.Select(a => new applicantstructs
            {
                app_id = a.applicant_id,
                birthday = a.birthday.HasValue ? a.birthday.Value.ToPersianDate() : "",
                cellnumber = a.cellnumber,
                codemelli = a.codemelli,
                f_name = a.f_name,
                l_name = a.l_name,
                sabtdate = a.sabtdate.HasValue ? a.sabtdate.Value.ToPersianDate() : "",
                sex = a.sex.HasValue ? (a.sex.Value == 1 ? "مذکر" : "مونث") : "",
                Jobname = a.job_.jobtitle,

            }

            );



            return PartialView("~/Views/Shared/PartialforRecruitment/_Table.cshtml", q);

        }
        [ResponseCache(Duration =600)]
        public IActionResult UserPanel()
        {
            int userid = int.Parse(User.Claims.FirstOrDefault().Value);
            string username = db3.tbl_user.Where(a => a.id == userid).SingleOrDefault().username;
            string fullname = db3.tbl_user.Where(a => a.id == userid).SingleOrDefault().name + " " + db3.tbl_user.Where(a => a.id == userid).SingleOrDefault().Lname;
            byte[] image = db3.tbl_user.Where(a => a.id == userid).SingleOrDefault().img;
            var accsesstypes = db3.tbl_accesstypes.Where(a => a.actionname != null && a.actiontype == 1 && a.status == true);
            List<int> accesslevelforuser = db3.tbl_accesslevels.Where(a => a.user.id == userid && a.accessvalue == true).Select(d => d.accessid).ToList();
            var q = accsesstypes.OrderBy(a => a.ordernumber).Where(a => accesslevelforuser.Contains(a.id)).Select(d => new UserPanelViewModel()
            {
                useid = userid,
                actionname = d.actionname,
                accesscaption = d.accesscaption,
                username = username,
                fullname = fullname,
                image = image
            });




            return PartialView("~/Views/Shared/PartialLayout/_PartialUserMenu.cshtml", q);
        }

        public ActionResult Activeallforthisuserbyuserid(int userid = 0)
        {

            var q = db3.tbl_actionaccessibility.Where(a => a.userid == userid).ToList();

            foreach (var item in q)
            {
                item.permission = true;
            }
            db3.SaveChanges();
            return Json(true);
        }


        public ActionResult GiveAccessMenuToUser(int userid = 0)
        {
            bool result = false;
            result = _userServices.GiveAccessMenuToUser(userid);
            return Json(result);
        }

        public ActionResult GetActionAccessItems(int userid = 0)
        {
            var result = db3.tbl_actionaccessibility.Where(a => a.userid == userid).GroupBy(a => new { a.acction_.menu_id, a.acction_.menu_.accesscaption, a.acction_.menu_.actionname }).Select(g => new MenuViewModel { menuid = g.Key.menu_id.Value, menuname = g.Key.accesscaption, userid = userid, menunameinEN = g.Key.actionname });
            return PartialView("/Views/Shared/PartialUsersManagment/_partialactionaccess.cshtml", result);


        }


        public ActionResult GetActionsOfThisMenu(int menuid = 0, int userid = 0)
        {
            var result = db3.tbl_actionaccessibility.Where(a => a.userid == userid && a.acction_.menu_id == menuid).Select(a => new ActionAccessModelView
            {
                id = a.id,
                actionname = a.acction_.action_name,
                actionid = a.acction_id.Value,
                permission = a.permission,
                userid = userid,
                menuid = menuid

            });
            return PartialView("/Views/Shared/PartialUsersManagment/_partialactionaccessdetails.cshtml", result);
        }
        public ActionResult checkallactionforthisusermenu(int menuid = 0, int userid = 0, bool targetstatus = true)
        {
            //var records = db3.tbl_actionaccessibility.Where(a => (a.userid.HasValue? a.userid == userid: 1==2));


            db3.tbl_actionaccessibility.Where(a => a.userid.Value == userid && a.acction_.menu_id.Value == menuid).ToList().ForEach(a =>
            {
                a.permission = targetstatus;

            });
            db3.SaveChanges();
            return Json(targetstatus);
        }
        // to show editactionform
        public ActionResult toshoweditactionform(int actionid)
        {
            var action = db3.tbl_actions.Find(actionid);
            return PartialView("/Views/Shared/PartialUsersManagment/_partialeditactionform.cshtml", action);

        }
        //tochangemenuidforthisaction
        public ActionResult tochangemenuidforthisaction(int actionid = 0, int menuid = 0)
        {
            var action = db3.tbl_actions.Find(actionid);
            action.menu_id = menuid;
            db3.SaveChanges();
            return Json(true);
        }


        #region PanelSms

        #region Home
        public ActionResult SendSmsPanel()
        {
            var tempMessage = db3.tbl_TemplateMessages.Select(x => new TemplateMessagesViewModel
            {
                Context = x.Context,
                Id = x.Id,
                Title = x.Title,
                TextForSelect = x.Context.Replace("\n", "\\n")
            }).OrderByDescending(x => x.Id).ToList();
            ViewBag.Templateessage = tempMessage;
            try
            {
                Cls_SMS.ClsGetRemain clsSend = new Cls_SMS.ClsGetRemain();
                ViewBag.RemainCredit = clsSend.GetRemainCredit("koohi8", "87g5820", "KOOHI", "http://193.104.22.14:2055/CPSMSService/Access");
            }
            catch (Exception e)
            {
                ViewBag.RemainCredit = "خطا در برقراری ارتباط با سرور";
            }
            return View();
        }
        public ActionResult FilterMenuCustomization_Groups()
        {
            var result = db3.tbl_Groups.Select(x => x.Name).ToList();
            return Json(result);
        }
        public ActionResult Persons_Read([DataSourceRequest]DataSourceRequest request)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            watch.Start();
            var result = db3.tbl_person.Include("Groups.Group");
            var translateFilters = new List<IFilterDescriptor>();
            var personGroups = new List<PersonViewModel>();

            foreach (var descriptor in request.Filters)
                TransformFilterQuery(descriptor);

            foreach (var descriptor in request.Sorts)
                TransformSortQuery(descriptor);

            var dsResultTemp = result.ToDataSourceResult(request);
            foreach (var person in (List<tbl_person>)dsResultTemp.Data)
            {


                if (person.Groups.Count > 0)
                {
                    foreach (var groups in person.Groups)
                    {
                        var personGroup = new PersonViewModel();
                        personGroup.Cell = person.cell ?? ((person.PersonInformationSettings == null || person.PersonInformationSettings.Count == 0) ? "" : person.PersonInformationSettings.FirstOrDefault(per => per.PropertyName == PersonInformationSetting.Mobile.ToString()).PropertyValue);
                        personGroup.FullNamePerson = (person.Fname ?? "") + " " + (person.Lname ?? "");
                        personGroup.CodeMeli = person.codemelli;
                        personGroup.Id = person.id;
                        personGroup.GroupId = groups.GroupId;
                        personGroup.GroupName = groups.Group.Name;
                        personGroups.Add(personGroup);
                    }
                }
                else
                {
                    var personGroup = new PersonViewModel();
                    personGroup.Cell = person.cell ?? ((person.PersonInformationSettings == null || person.PersonInformationSettings.Count == 0) ? "" : person.PersonInformationSettings.FirstOrDefault(per => per.PropertyName == PersonInformationSetting.Mobile.ToString()).PropertyValue);
                    personGroup.FullNamePerson = (person.Fname ?? "") + " " + (person.Lname ?? "");
                    personGroup.CodeMeli = person.codemelli;
                    personGroup.Id = person.id;
                    personGroup.GroupId = 0;
                    personGroup.GroupName = "تعیین نشده";
                    personGroups.Add(personGroup);
                }
            }
            void TransformFilterQuery(IFilterDescriptor filter)
            {
                if (filter is CompositeFilterDescriptor)
                {
                    foreach (var filterDescriptor in ((CompositeFilterDescriptor)filter).FilterDescriptors)
                        TransformFilterQuery(filterDescriptor);
                }
                else
                {
                    var filterDescriptor = (FilterDescriptor)filter;
                    if (filterDescriptor.Member == "FullNamePerson")
                    {
                        var name = filterDescriptor.Value.ToString();

                        switch (filterDescriptor.Operator)
                        {
                            case FilterOperator.Contains:

                                result = result.Where(
                                    x => (x.Fname ?? "").Contains(name) ||
                                         (x.Lname ?? "").Contains(name) ||
                                         ((x.Fname ?? "") + " " + (x.Lname ?? "")).Contains(name));
                                break;
                            case FilterOperator.IsEqualTo:
                                result = result.Where(
                                    x => x.Fname == (name) ||
                                         x.Lname == (name) ||
                                         (x.Fname ?? "") + " " + (x.Lname ?? "") == (name));
                                break;
                        }
                        var passengerFilter = new FilterDescriptor
                        {
                            Member = filterDescriptor.Member,
                            Operator = filterDescriptor.Operator,
                            Value = filterDescriptor.Value
                        };
                        translateFilters.Add(passengerFilter);

                        filterDescriptor.Member = "Id";
                        filterDescriptor.Operator = FilterOperator.IsNotNull;
                        filterDescriptor.Value = null;
                    }

                    if (filterDescriptor.Member == "GroupName")
                    {
                        var name = filterDescriptor.Value?.ToString();

                        switch (filterDescriptor.Operator)
                        {
                            case FilterOperator.Contains:
                                result = result.Where(x => x.Groups.Any(z => z.Group.Name.Contains(name)));
                                break;
                            case FilterOperator.IsEqualTo:
                                result = result.Where(x => x.Groups.Any(z => z.Group.Name == name));
                                break;
                        }
                        var passengerFilter = new FilterDescriptor
                        {
                            Member = filterDescriptor.Member,
                            Operator = filterDescriptor.Operator,
                            Value = filterDescriptor.Value
                        };
                        translateFilters.Add(passengerFilter);

                        filterDescriptor.Member = "Id";
                        filterDescriptor.Operator = FilterOperator.IsNotNull;
                        filterDescriptor.Value = null;
                    }


                }
            }

            void TransformSortQuery(SortDescriptor sort)
            {
                var sortDescriptor = sort;
                if (sortDescriptor.Member == "FullNamePerson")
                {
                    sortDescriptor.Member = "Fname";
                }
            }
            // var dsResult = personGroups.ToDataSourceResult(request);
            dsResultTemp.Data = personGroups;
            //var jsonNetResult = new JsonNetResult
            //{
            //    Formatting = Formatting.Indented,
            //    SerializerSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore },
            //    Data = data
            //};
            watch.Stop();
            var elapsedMs4 = watch.ElapsedMilliseconds;
            return Json(dsResultTemp);
        }

        public ActionResult PersonsSelected(int[] personId)
        {

            var result = db3.tbl_person.Where(x => personId.Any(y => y == x.id)).Select(x => new PersonViewModel
            {
                Cell = x.cell ?? ((x.PersonInformationSettings == null || x.PersonInformationSettings.Count == 0) ? "" : x.PersonInformationSettings.FirstOrDefault(per => per.PropertyName == PersonInformationSetting.Mobile.ToString()).PropertyValue),
                CodeMeli = x.codemelli,
                FullNamePerson = x.Fname + " " + x.Lname,
                Id = x.id
            }).ToList();
            ViewData["PersonsSelected"] = result;

            return PartialView("_PersonSelected");
        }

        public ActionResult TemplateMessage()
        {
            var tempMessage = db3.tbl_TemplateMessages.Select(x => new TemplateMessagesViewModel
            {
                Context = x.Context,
                Id = x.Id,
                Title = x.Title,
                TextForSelect = x.Context.Replace("\n", "\\n")
            }).OrderByDescending(x => x.Id).ToList();

            return PartialView("_TemplateMessage", tempMessage);
        }

        public ActionResult CreateTemplateMessage(string title, string context)
        {
            var resultStatus = new ResultStatus();
            try
            {
                if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(context))
                {

                    resultStatus.IsSuccessed = false;
                    resultStatus.Message = "اطلاعات را وارد کنید";
                    return Json(resultStatus);
                }

                db3.tbl_TemplateMessages.Add(new tbl_templateMessages()
                {
                    Title = title,
                    Context = context
                });
                db3.SaveChanges();
                resultStatus.IsSuccessed = true;
                resultStatus.Message = "اطلاعات با موفقیت ثبت گردید";
            }
            catch (Exception e)
            {
                resultStatus.IsSuccessed = true;
                resultStatus.Message = "خطا در بروز عملیات";
            }

            return Json(resultStatus);
        }
        public ActionResult DeletTemplateMessage(int id)
        {
            var resultStatus = new ResultStatus();
            try
            {
                if (id == 0)
                {

                    resultStatus.IsSuccessed = false;
                    resultStatus.Message = "اطلاعات را وارد کنید";
                    return Json(resultStatus);
                }

                var temp = db3.tbl_TemplateMessages.FirstOrDefault(x => x.Id == id);
                if (temp == null)
                {

                    resultStatus.IsSuccessed = false;
                    resultStatus.Message = "اطلاعات یافت نشد";
                    return Json(resultStatus);
                }
                db3.tbl_TemplateMessages.Remove(temp);
                db3.SaveChanges();
                resultStatus.IsSuccessed = true;
                resultStatus.Message = "اطلاعات با موفقیت حذف گردید";
            }
            catch (Exception e)
            {
                resultStatus.IsSuccessed = true;
                resultStatus.Message = "خطا در بروز عملیات";
            }

            return Json(resultStatus);
        }

        public ActionResult AutocompletePerson(string data)
        {

            var peson = (from p in db3.tbl_person.Where(x => x.cell.Contains(data) ||
                                                             x.PersonInformationSettings.FirstOrDefault(per => per.PropertyName == PersonInformationSetting.Mobile.ToString()).PropertyValue.Contains(data) ||
                                                             x.Fname.Contains(data) ||
                                                             x.Lname.Contains(data) ||
                                                             (x.Fname + " " + x.Lname).Contains(data))
                         select new
                         { name = (p.Fname ?? "") + " " + (p.Lname ?? ""), cell = p.cell ?? ((p.PersonInformationSettings == null || p.PersonInformationSettings.Count == 0) ? "" : p.PersonInformationSettings.FirstOrDefault(per => per.PropertyName == PersonInformationSetting.Mobile.ToString()).PropertyValue) }).ToList();


            return Json(peson);

        }

        public ActionResult SendMessage(string context, string personsId, string personCellphone, IFormFile fileNumber)
        {
            var resultStatus = new ResultStatus();
            List<int> personId = new List<int>();
            if (personsId != null)
                personId = personsId.Split(',').Select(Int32.Parse).ToList();

            Cls_SMS.ClsSend clsSend = new Cls_SMS.ClsSend();
            try
            {
                if (fileNumber != null)
                {
                    string[] validFileTypes = { ".xls", ".xlsx", ".csv" };
                    string extension = System.IO.Path.GetExtension(fileNumber.FileName).ToLower();
                    if (!validFileTypes.Contains(extension))
                    {
                        resultStatus.IsSuccessed = false;
                        resultStatus.Message = "نوع فایل معتبر نمی باشد";
                        return Json(resultStatus);

                    }

                }

                if ((string.IsNullOrEmpty(context) || (personId.Count == 0 && (string.IsNullOrEmpty(personCellphone) || personCellphone == "0"))) && fileNumber == null)
                {

                    resultStatus.IsSuccessed = false;
                    resultStatus.Message = "اطلاعات را وارد کنید";
                    return Json(resultStatus);
                }

                if (personId.Any())
                {
                    var person = db3.tbl_person.Where(x => personId.Any(y => y == x.id)).ToList();


                    bool isPersian = false;

                    int smsCount = 0;
                    Cls_SMS.ClsSend.FindTxtLanguageAndcount(context, ref isPersian, ref smsCount);
                    var numberOfPersonToSend = 100 / smsCount;
                    int personGroup = person.Count / numberOfPersonToSend;
                    var remainingPerson = person.Count % numberOfPersonToSend;

                    int start = 0;
                    for (int i = 0; i < personGroup; i++)
                    {

                        var lst = person.GetRange(start, numberOfPersonToSend);
                        start = numberOfPersonToSend * (i + 1);
                        var result = clsSend.SendSMS_Batch(context, lst.Select(x => x.cell).ToArray(), "100008001", "koohi8", "87g5820", "http://193.104.22.14:2055/CPSMSService/Access", "KOOHI", false);
                        var sentMessages = new tbl_sentMessag()
                        {
                            CreateDateTime = DateTime.Now,
                            ContextMessage = context,
                            State = result[0],
                            RefNumber = result[1],
                            UserId = int.Parse(User.Claims.FirstOrDefault().Value)
                        };
                        db3.tbl_sentMessag.Add(sentMessages);
                        foreach (var per in lst)
                        {
                            var sentMessagePerson = new tbl_SentMessagPerson()
                            {
                                Person = per,
                                SentMessag = sentMessages,
                            };
                            db3.tbl_SentMessagPerson.Add(sentMessagePerson);
                        }
                    }


                    if (remainingPerson > 0)
                    {
                        var lst = person.GetRange(start, remainingPerson);
                        var result = clsSend.SendSMS_Batch(context, lst.Select(x => x.cell).ToArray(), "100008001", "koohi8", "87g5820", "http://193.104.22.14:2055/CPSMSService/Access", "KOOHI", false);
                        var sentMessages = new tbl_sentMessag()
                        {
                            CreateDateTime = DateTime.Now,
                            ContextMessage = context,
                            State = result[0],
                            RefNumber = result[1],
                            UserId = int.Parse(User.Claims.FirstOrDefault().Value)
                        };
                        db3.tbl_sentMessag.Add(sentMessages);
                        foreach (var per in lst)
                        {
                            var sentMessagePerson = new tbl_SentMessagPerson()
                            {
                                Person = per,
                                SentMessag = sentMessages,
                            };
                            db3.tbl_SentMessagPerson.Add(sentMessagePerson);
                        }
                    }
                }


                if (!string.IsNullOrEmpty(personCellphone) && personCellphone != "0")
                {
                    var result = clsSend.SendSMS_Single(context, personCellphone, "100008001", "koohi8", "87g5820", "http://193.104.22.14:2055/CPSMSService/Access", "KOOHI", false);
                    var sentMessages = new tbl_sentMessag()
                    {
                        CreateDateTime = DateTime.Now,
                        ContextMessage = context,
                        State = result[0],
                        RefNumber = result[1],
                        UserId = int.Parse(User.Claims.FirstOrDefault().Value)
                    };
                    db3.tbl_sentMessag.Add(sentMessages);
                    var person = db3.tbl_person.FirstOrDefault(x => x.cell.Contains(personCellphone) ||
                                                                    x.PersonInformationSettings.Count(per => per.PropertyName == PersonInformationSetting.Mobile.ToString() && per.PropertyValue.Contains(personCellphone)) != 0);
                    db3.tbl_SentMessagPerson.Add(new tbl_SentMessagPerson
                    {
                        Person = person,
                        PersonCellPhone = personCellphone,
                        SentMessag = sentMessages
                    });
                    //  lstPersonCell.Add(personCellphone);
                }

                if (fileNumber != null)
                {

                    List<PersonCellExecl> perExcel = new List<PersonCellExecl>();

                    using (var stream = new MemoryStream())
                    {
                        fileNumber.CopyTo(stream);

                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
                            var rowCount = worksheet.Dimension.Rows;

                            for (int row = 1; row <= rowCount; row++)
                            {
                                perExcel.Add(new PersonCellExecl() { Cell = worksheet.Cells[row, 1].Value.ToString(), Text = worksheet.Cells[row, 2].Value.ToString() });
                            }
                        }
                    }

                    foreach (var item in perExcel.GroupBy(x => x.Text))
                    {
                        var messExcel = perExcel.FirstOrDefault(x => x.Text == item.Key).Text;
                        var mobilesExcel = perExcel.Where(x => x.Text == item.Key).Select(x => x.Cell).ToList();
                        var result = clsSend.SendSMS_Batch(messExcel, mobilesExcel.ToArray(), "100008001", "koohi8", "87g5820", "http://193.104.22.14:2055/CPSMSService/Access", "KOOHI", false);
                        var sentMessages = new tbl_sentMessag()
                        {
                            CreateDateTime = DateTime.Now,
                            ContextMessage = messExcel,
                            State = result[0],
                            RefNumber = result[1],
                            UserId = int.Parse(User.Claims.FirstOrDefault().Value)
                        };
                        db3.tbl_sentMessag.Add(sentMessages);
                        foreach (var perm in mobilesExcel)
                        {
                            var person = db3.tbl_person.FirstOrDefault(x => x.cell.Contains(perm) ||
                                                                                 x.PersonInformationSettings.Count(per => per.PropertyName == PersonInformationSetting.Mobile.ToString() && per.PropertyValue.Contains(perm)) != 0);
                            var sentMessagePerson = new tbl_SentMessagPerson()
                            {
                                Person = person,
                                PersonCellPhone = perm,
                                SentMessag = sentMessages,
                            };
                            db3.tbl_SentMessagPerson.Add(sentMessagePerson);
                        }
                    }
                }
                db3.SaveChanges();

                resultStatus.IsSuccessed = true;
                resultStatus.Message = "پیام با موفقیت ارسال گردید";


            }
            catch (Exception e)
            {
                resultStatus.IsSuccessed = true;
                resultStatus.Message = "خطا در بروز عملیات";
            }

            return Json(resultStatus);
        }
        #endregion



        #region Archive

        public ActionResult ArchiveSms()
        {

            return View();
        }

        public ActionResult FilterMenuCustomization_Branch()
        {
            var result = db3.tbl_branches.Select(x => x.branch_name).ToList();
            return Json(result);
        }

        public ActionResult ArchiveSms_Read([DataSourceRequest]DataSourceRequest request)
        {
            int branchId = 0;
            var userId = int.Parse(User.Claims.FirstOrDefault().Value);
            branchId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "BranchId")?.Value ?? "0");
            if (branchId == 0)
            {
                branchId = (int)db3.tbl_user.FirstOrDefault(x => x.id == userId).branches_id;
            }

            Cls_SMS.ClsSend send = new Cls_SMS.ClsSend();
            var translateFilters = new List<IFilterDescriptor>();
            IEnumerable<ArchiveSmsViewModel> result;
            var branchIdsend = userId == 78 ? branchId : 0;
           
            if (userId != 78)
            {
                result = db3.tbl_SentMessagPerson.Include(x => x.Person).Include(x => x.SentMessag)
                    .Where(x => x.SentMessag.User.branches_id == branchId).OrderByDescending(x => x.SentMessagId)
                    .Select(
                        x => new ArchiveSmsViewModel
                        {
                            Id=x.Id,
                            ContextMessage = x.SentMessag.ContextMessage,
                            FullNamePerson = x.PersonId != null ? (x.Person.Fname ?? "") + " " + (x.Person.Lname ?? "") : x.PersonCellPhone,
                            CreateDateTime = x.SentMessag.CreateDateTime,
                            FullNameSender = x.SentMessag.User.username,
                            Branch = x.SentMessag.User.branches_.branch_name,
                            SentMessagId = x.SentMessagId,
                            StateSendMessageToWebservice = x.SentMessag.State,
                            Mobile = x.PersonId != null ? x.Person.cell ?? ((x.Person.PersonInformationSettings == null || x.Person.PersonInformationSettings.Count == 0) ? "" : x.Person.PersonInformationSettings.FirstOrDefault(per => per.PropertyName == PersonInformationSetting.Mobile.ToString()).PropertyValue) : x.PersonCellPhone,
                            RefNumber = x.SentMessag.RefNumber,
                            DeliveryStatus = (x.DeliveryStatus != null && x.SentMessag.State == "CHECK_OK") ? ((DeliveryStatus)Enum.Parse(typeof(DeliveryStatus), x.DeliveryStatus)).GetDisplayName() : (x.SentMessag.State != "CHECK_OK") ? "خطای ارسال پیام به سرویس" : "نامشخص",
                            // SMSCount = x.SentMessag.ContextMessage.Length>70?(x.SentMessag.ContextMessage.Length / 67).ToString():"1"
                            SMSCount = send.FindTxtcount(x.SentMessag.ContextMessage),
                            State = x.SentMessag.State,
                            cell = x.PersonId != null ? x.Person.cell ?? ((x.Person.PersonInformationSettings == null || x.Person.PersonInformationSettings.Count == 0) ? "" : x.Person.PersonInformationSettings.FirstOrDefault(per => per.PropertyName == PersonInformationSetting.Mobile.ToString()).PropertyValue) : x.PersonCellPhone,

                        });
            }
            else
            {
                result = db3.tbl_SentMessagPerson.Include(x => x.Person).Include(x => x.SentMessag).OrderByDescending(x => x.SentMessagId)
                    .Select(
                        x => new ArchiveSmsViewModel
                        {
                            Id = x.Id,
                            ContextMessage = x.SentMessag.ContextMessage,
                            FullNamePerson = x.PersonId != null ? (x.Person.Fname ?? "") + " " + (x.Person.Lname ?? "") : x.PersonCellPhone,
                            CreateDateTime = x.SentMessag.CreateDateTime,
                            FullNameSender = x.SentMessag.User.username,
                            Branch = x.SentMessag.User.branches_.branch_name,
                            SentMessagId = x.SentMessagId,
                            StateSendMessageToWebservice = x.SentMessag.State,
                            Mobile = x.PersonId != null ? x.Person.cell ?? ((x.Person.PersonInformationSettings == null || x.Person.PersonInformationSettings.Count == 0) ? "" : x.Person.PersonInformationSettings.FirstOrDefault(per => per.PropertyName == PersonInformationSetting.Mobile.ToString()).PropertyValue) : x.PersonCellPhone,
                            RefNumber = x.SentMessag.RefNumber,
                            DeliveryStatus = (x.DeliveryStatus != null && x.SentMessag.State == "CHECK_OK") ? ((DeliveryStatus)Enum.Parse(typeof(DeliveryStatus), x.DeliveryStatus)).GetDisplayName() : (x.SentMessag.State != "CHECK_OK") ? "خطای ارسال پیام به سرویس" : "نامشخص",
                            // SMSCount = x.SentMessag.ContextMessage.Length>70?(x.SentMessag.ContextMessage.Length / 67).ToString():"1"
                            SMSCount = send.FindTxtcount(x.SentMessag.ContextMessage),
                            State = x.SentMessag.State,
                            cell = x.PersonId != null ? x.Person.cell ?? ((x.Person.PersonInformationSettings == null || x.Person.PersonInformationSettings.Count == 0) ? "" : x.Person.PersonInformationSettings.FirstOrDefault(per => per.PropertyName == PersonInformationSetting.Mobile.ToString()).PropertyValue) : x.PersonCellPhone,

                        });
            }

            //if (userId != 78)
            //{

            //    data = db3.tbl_SentMessagPerson.Where(x => (x.SentMessag.User.branches_id == branchId) && x.DeliveryStatus == null);
            //}
            //else
            //{
            //    data = db3.tbl_SentMessagPerson.Where(x => x.DeliveryStatus == null);
            //}

          //  IQueryable<tbl_SentMessagPerson> data,dataFilter;
           var data = db3.tbl_SentMessagPerson;
           // var dataGroup = data.Select(x => new { RefNumber = x.SentMessag.RefNumber }).GroupBy(x => x.RefNumber).ToList();
            var dataGroup2 = data.OrderByDescending(x=>x.SentMessagId);
            foreach (var descriptor in request.Filters)
                TransformFilterQuery(descriptor);

            foreach (var descriptor in request.Sorts)
                TransformSortQuery(descriptor);
            void TransformFilterQuery(IFilterDescriptor filter)
            {
                if (filter is CompositeFilterDescriptor)
                {
                    foreach (var filterDescriptor in ((CompositeFilterDescriptor)filter).FilterDescriptors)
                        TransformFilterQuery(filterDescriptor);
                }
                else
                {
                    var filterDescriptor = (FilterDescriptor)filter;
                    if (filterDescriptor.Member == "FullNamePerson")
                    {
                        var name = filterDescriptor.Value.ToString();

                        switch (filterDescriptor.Operator)
                        {
                            case FilterOperator.Contains:

                                dataGroup2 = dataGroup2.Where(
                                    x => (x.Person.Fname ?? "").Contains(name) ||
                                         (x.Person.Lname ?? "").Contains(name) ||
                                         ((x.Person.Fname ?? "") + " " + (x.Person.Lname ?? "")).Contains(name)).OrderByDescending(x=>x.SentMessagId);
                                break;
                            case FilterOperator.IsEqualTo:
                                dataGroup2 = dataGroup2.Where(
                                    x => x.Person.Fname == (name) ||
                                         x.Person.Lname == (name) ||
                                         (x.Person.Fname ?? "") + " " + (x.Person.Lname ?? "") == (name)).OrderByDescending(x => x.SentMessagId);
                                break;
                        }
                        
                    }
                    if (filterDescriptor.Member == "FullNameSender")
                    {
                        var name = filterDescriptor.Value.ToString();

                        switch (filterDescriptor.Operator)
                        {
                            case FilterOperator.Contains:

                                dataGroup2 = dataGroup2.Where(x => x.SentMessag.User.username.Contains(name)).OrderByDescending(x => x.SentMessagId);
                                break;
                            case FilterOperator.IsEqualTo:
                                dataGroup2 = dataGroup2.Where(x => x.SentMessag.User.username == (name)).OrderByDescending(x => x.SentMessagId);
                                break;
                        }
                       
                    }
                    if (filterDescriptor.Member == "Branch")
                    {
                        var name = filterDescriptor.Value.ToString();

                        switch (filterDescriptor.Operator)
                        {
                            case FilterOperator.Contains:

                                dataGroup2 = dataGroup2.Where(x => x.SentMessag.User.branches_.branch_name.Contains(name)).OrderByDescending(x => x.SentMessagId);
                                break;
                            case FilterOperator.IsEqualTo:
                                dataGroup2 = dataGroup2.Where(x => x.SentMessag.User.branches_.branch_name == (name)).OrderByDescending(x => x.SentMessagId);
                                break;
                        }
                       
                    }
                    if (filterDescriptor.Member == "ContextMessage")
                    {
                        var name = filterDescriptor.Value.ToString();

                        switch (filterDescriptor.Operator)
                        {
                            case FilterOperator.Contains:

                                dataGroup2 = dataGroup2.Where(x => x.SentMessag.ContextMessage.Contains(name)).OrderByDescending(x => x.SentMessagId);
                                break;
                            case FilterOperator.IsEqualTo:
                                dataGroup2 = dataGroup2.Where(x => x.SentMessag.ContextMessage == (name)).OrderByDescending(x => x.SentMessagId);
                                break;
                        }
                       
                    }
                    if (filterDescriptor.Member == "DeliveryStatus")
                    {
                        var name = filterDescriptor.Value.ToString();

                        switch (filterDescriptor.Operator)
                        {
                            case FilterOperator.Contains:

                                dataGroup2 = dataGroup2.Where(x => ((x.DeliveryStatus != null && x.SentMessag.State == "CHECK_OK") ? ((DeliveryStatus)Enum.Parse(typeof(DeliveryStatus), x.DeliveryStatus)).GetDisplayName() : (x.SentMessag.State != "CHECK_OK") ? "خطای ارسال پیام به سرویس" : "نامشخص").Contains(name)).OrderByDescending(x => x.SentMessagId);
                                break;
                            case FilterOperator.IsEqualTo:
                                dataGroup2 = dataGroup2.Where(x => ((x.DeliveryStatus != null && x.SentMessag.State == "CHECK_OK") ? ((DeliveryStatus)Enum.Parse(typeof(DeliveryStatus), x.DeliveryStatus)).GetDisplayName() : (x.SentMessag.State != "CHECK_OK") ? "خطای ارسال پیام به سرویس" : "نامشخص") == (name)).OrderByDescending(x => x.SentMessagId);
                                break;
                        }
                      
                    }
                    if (filterDescriptor.Member == "Mobile")
                    {
                        var name = filterDescriptor.Value.ToString();

                        switch (filterDescriptor.Operator)
                        {
                            case FilterOperator.Contains:

                                dataGroup2 = dataGroup2.Where(x => (x.PersonId != null ? x.Person.cell ?? ((x.Person.PersonInformationSettings == null || x.Person.PersonInformationSettings.Count == 0) ? "" : x.Person.PersonInformationSettings.FirstOrDefault(per => per.PropertyName == PersonInformationSetting.Mobile.ToString()).PropertyValue) : x.PersonCellPhone).Contains(name)).OrderByDescending(x => x.SentMessagId);
                                break;
                            case FilterOperator.IsEqualTo:
                                dataGroup2 = dataGroup2.Where(x => (x.PersonId != null ? x.Person.cell ?? ((x.Person.PersonInformationSettings == null || x.Person.PersonInformationSettings.Count == 0) ? "" : x.Person.PersonInformationSettings.FirstOrDefault(per => per.PropertyName == PersonInformationSetting.Mobile.ToString()).PropertyValue) : x.PersonCellPhone) == (name)).OrderByDescending(x => x.SentMessagId);
                                break;
                        }
                        
                    }
                    if (filterDescriptor.Member == "CreateDateTime")
                    {
                        var name = (DateTime)filterDescriptor.Value;

                        switch (filterDescriptor.Operator)
                        {
                            case FilterOperator.IsGreaterThanOrEqualTo:

                                dataGroup2 = dataGroup2.Where(x => x.SentMessag.CreateDateTime.Date>= name.Date).OrderByDescending(x => x.SentMessagId);
                                break;
                            case FilterOperator.IsEqualTo:

                                dataGroup2 = dataGroup2.Where(x => x.SentMessag.CreateDateTime.Date == name.Date).OrderByDescending(x => x.SentMessagId);
                                break;
                            case FilterOperator.IsGreaterThan:

                                dataGroup2 = dataGroup2.Where(x => x.SentMessag.CreateDateTime.Date > name.Date).OrderByDescending(x => x.SentMessagId);
                                break;
                            case FilterOperator.IsLessThan:

                                dataGroup2 = dataGroup2.Where(x => x.SentMessag.CreateDateTime.Date < name.Date).OrderByDescending(x => x.SentMessagId);
                                break;
                            case FilterOperator.IsNotEqualTo:

                                dataGroup2 = dataGroup2.Where(x => x.SentMessag.CreateDateTime.Date != name.Date).OrderByDescending(x => x.SentMessagId);
                                break;
                            case FilterOperator.IsLessThanOrEqualTo:
                                dataGroup2 = dataGroup2.Where(x => x.SentMessag.CreateDateTime.Date <= name.Date).OrderByDescending(x => x.SentMessagId);
                                break;
                        }

                    }
                }
            }

            void TransformSortQuery(SortDescriptor sort)
            {
                var sortDescriptor = sort;
                if (sortDescriptor.Member == "FullNamePerson")
                {
                    dataGroup2= dataGroup2.OrderBy(x=> (x.Person.Fname ?? "") + " " + (x.Person.Lname ?? ""));
                }
                if (sortDescriptor.Member == "Branch")
                {
                    dataGroup2 = dataGroup2.OrderBy(x => x.SentMessag.User.branches_.branch_name);
                }
                if (sortDescriptor.Member == "SentMessagId")
                {
                    dataGroup2 = dataGroup2.OrderBy(x => x.SentMessagId);
                }
            }
            
            Cls_SMS.ClsStatus clsStatus = new Cls_SMS.ClsStatus();
            var datagroupFilter = dataGroup2.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize)
               .ToList();
            try
            {
                foreach (var sms in datagroupFilter.Where(x => x.DeliveryStatus == null).Select(x => new { RefNumber = x.SentMessag.RefNumber })
                    .GroupBy(x => x.RefNumber))
                {

                    var lstStatusSms = clsStatus.StatusSMS("koohi8", "87g5820", "http://193.104.22.14:2055/CPSMSService/Access", "KOOHI",
                        "KOOHI+" + sms.Key);
                    if (lstStatusSms != null && lstStatusSms.Count > 0)
                    {

                        foreach (var perSms in datagroupFilter.Where(x => x.SentMessag.RefNumber == sms.Key))
                        {

                            Cls_SMS.ClsStatus.STC_SMSStatus findPerson = null;
                            if (perSms.Person != null)
                            {
                                if (perSms.Person.cell != null)
                                {
                                    findPerson = lstStatusSms.Cast<Cls_SMS.ClsStatus.STC_SMSStatus>()
                                        .FirstOrDefault(x => x.ReceiveNumber.Replace("+98", "0").Contains(perSms.Person.cell));
                                }
                                else if((perSms.Person.PersonInformationSettings != null && perSms.Person.PersonInformationSettings.Count != 0))
                                {
                                    var personMobile =
                                              perSms.Person.PersonInformationSettings.Where(per =>
                                                  per.PropertyName == PersonInformationSetting.Mobile.ToString()).ToList();
                                    findPerson = lstStatusSms.Cast<Cls_SMS.ClsStatus.STC_SMSStatus>()
                                        .FirstOrDefault(x => personMobile.Any(y => y.PropertyValue == x.ReceiveNumber.Replace("+98", "0")));
                                }
                                
                            }
                            else if (perSms.PersonCellPhone!=null)
                            {
                                findPerson = lstStatusSms.Cast<Cls_SMS.ClsStatus.STC_SMSStatus>()
                                    .FirstOrDefault(x => x.ReceiveNumber.Replace("+98", "0").Contains(perSms.PersonCellPhone));
                            }
                            if (findPerson != null)
                            {
                                perSms.DeliveryStatus = findPerson.DeliveryStatus;
                                var test = result.FirstOrDefault(x => x.Id == perSms.Id);
                                if (test != null)
                                    test.DeliveryStatus = ((DeliveryStatus)Enum.Parse(typeof(DeliveryStatus), findPerson.DeliveryStatus)).GetDisplayName();
                            }
                            else
                            {
                                perSms.DeliveryStatus = lstStatusSms.Cast<Cls_SMS.ClsStatus.STC_SMSStatus>()
                                    .FirstOrDefault().DeliveryStatus;
                                var test = result.FirstOrDefault(x => perSms.Id == x.Id);
                                if (test != null)
                                    test.DeliveryStatus = ((DeliveryStatus)Enum.Parse(typeof(DeliveryStatus), lstStatusSms.Cast<Cls_SMS.ClsStatus.STC_SMSStatus>()
                                        .FirstOrDefault().DeliveryStatus)).GetDisplayName();
                            }

                            data.FirstOrDefault(x => x.Id == perSms.Id).DeliveryStatus = perSms.DeliveryStatus;
                        }

                    }
                 
                }
            }
            catch (Exception e)
            {
               
            }
           
            db3.SaveChanges();

            var dsResult = result.ToList().ToDataSourceResult(request);

            return Json(dsResult);
        }

        #endregion

        #region Group
        public IActionResult Group()
        {
            return View();
        }
        public ActionResult ListPersons()
        {
            return PartialView("_Person");
        }
        public ActionResult PersonsForGroup_Read([DataSourceRequest]DataSourceRequest request, int groupId)
        {
            var result = db3.tbl_person.Where(x => !x.Groups.Any(y => y.GroupId == groupId) || !x.Groups.Any())
                .Select(x => new PersonViewModel
                {
                    CodeMeli = x.codemelli,
                    Cell = x.cell ?? ((x.PersonInformationSettings == null || x.PersonInformationSettings.Count == 0) ? "" : x.PersonInformationSettings.FirstOrDefault(per => per.PropertyName == PersonInformationSetting.Mobile.ToString()).PropertyValue),
                    FullNamePerson = x.Fname + " " + x.Lname,
                    Id = x.id
                }).ToList();

            var dsResult = result.ToDataSourceResult(request);
            return Json(dsResult);
        }


        public ActionResult ListPersonGroups()
        {
            return PartialView("_PersonGroup");
        }
        public ActionResult GroupPerson_Read([DataSourceRequest]DataSourceRequest request)
        {

            var result = db3.tbl_GroupPersons
                        .Include(x => x.Person).Include(x => x.Group).Select(x => new PersonViewModel
                        {
                            Address = x.Person.address,
                            Cell = x.Person.cell ?? ((x.Person.PersonInformationSettings == null || x.Person.PersonInformationSettings.Count == 0) ? "" : x.Person.PersonInformationSettings.FirstOrDefault(per => per.PropertyName == PersonInformationSetting.Mobile.ToString()).PropertyValue),
                            CodeMeli = x.Person.codemelli,
                            FullNamePerson = x.Person.Fname + " " + x.Person.Lname,
                            GroupId = x.GroupId,
                            Id = x.PersonId,
                            Tell = x.Person.tell,
                            GroupName = x.Group.Name,
                        }).ToList();
            var dsResult = result.ToDataSourceResult(request);
            return Json(dsResult);
        }


        public ActionResult ListGroups()
        {
            return PartialView("_Groups");
        }
        public JsonResult Groups_Read()
        {
            var result = db3.tbl_Groups.Select(i => new
            {
                GroupName = i.Name,
                Id = i.Id
            });

            return Json(result);
        }

        public ActionResult CreateGroup(string title, string context)
        {
            var resultStatus = new ResultStatus();
            try
            {
                if (string.IsNullOrEmpty(title))
                {

                    resultStatus.IsSuccessed = false;
                    resultStatus.Message = "عنوان گروه اجباری می باشد";
                    return Json(resultStatus);
                }

                db3.tbl_Groups.Add(new tbl_Groups
()
                {
                    Name = title,
                    Description = context
                });
                db3.SaveChanges();
                resultStatus.IsSuccessed = true;
                resultStatus.Message = "اطلاعات با موفقیت ثبت گردید";
            }
            catch (Exception e)
            {
                resultStatus.IsSuccessed = true;
                resultStatus.Message = "خطا در بروز عملیات";
            }

            return Json(resultStatus);
        }

        public ActionResult PersonGroup_Destroy(int groupId, int personId)
        {
            var resultStatus = new ResultStatus();
            try
            {
                if (groupId == 0 && personId == 0)
                {

                    resultStatus.IsSuccessed = false;
                    resultStatus.Message = "  مشتری را انتخاب کنید";
                    return Json(resultStatus);
                }

                var per = db3.tbl_GroupPersons.FirstOrDefault(x => x.GroupId == groupId && x.PersonId == personId);
                if (per == null)
                {
                    resultStatus.IsSuccessed = false;
                    resultStatus.Message = "  مشتری با این مشخصات پیدا نشد";
                    return Json(resultStatus);
                }

                db3.tbl_GroupPersons.Remove(per);
                db3.SaveChanges();
                resultStatus.IsSuccessed = true;
                resultStatus.Message = "اطلاعات با موفقیت حذف گردید";
            }
            catch (Exception e)
            {
                resultStatus.IsSuccessed = true;
                resultStatus.Message = "خطا در بروز عملیات";
            }

            return Json(resultStatus);
        }

        public ActionResult AddPersonToGroup(int groupId, int[] personId)
        {

            var resultStatus = new ResultStatus();
            try
            {
                if (groupId == 0 && personId.Length == 0)
                {

                    resultStatus.IsSuccessed = false;
                    resultStatus.Message = "گروه و مشتری را انتخاب کنید";
                    return Json(resultStatus);
                }

                personId = personId.Where(x => !db3.tbl_GroupPersons.Any(y => y.GroupId == groupId && y.PersonId == x))
                    .ToArray();
                foreach (var per in personId)
                {

                    db3.tbl_GroupPersons.Add(new tbl_GroupPersons()
                    {
                        GroupId = groupId,
                        PersonId = per,
                    });
                }
                db3.SaveChanges();
                resultStatus.IsSuccessed = true;
                resultStatus.Message = "اطلاعات با موفقیت ثبت گردید";
            }
            catch (Exception e)
            {
                resultStatus.IsSuccessed = true;
                resultStatus.Message = "خطا در بروز عملیات";
            }

            return Json(resultStatus);
        }

        #endregion



        #endregion


    }
}








