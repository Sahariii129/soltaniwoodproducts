using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoltaniWeb.Models.Domain;
using Microsoft.AspNetCore.Http;
using SoltaniWeb.Models.structs;
using System.Drawing.Printing;
using System.IO;
using Newtonsoft.Json;
using SoltaniWeb.Models.utility;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Text.RegularExpressions;
using SoltaniWeb.Models.repository;
using SoltaniWeb.Models.Services.Interfaces;

namespace SoltaniWeb.Controllers
{
    //[getstatisticController]
    public class HomeController : Controller
    {

        _4820_soltaniwebContext db = new _4820_soltaniwebContext();
        private readonly IHttpContextAccessor httpContextAccessor;
        private IHostingEnvironment _hostingEnvironment;
        private IUserServices _userServices;
        public HomeController(IHostingEnvironment environment, IHttpContextAccessor httpContextAccessor, IUserServices userServices)
        {
            _hostingEnvironment = environment;
            this.httpContextAccessor = httpContextAccessor;
            _userServices = userServices;

        }

        
        public IActionResult Index()
        {
            try
            {
                ViewBag.title = "صفحه اصلی | کالای چوب سلطانی";
                
                    return View();
               
            }

            catch (Exception e)
            {

                throw new Exception(e.InnerException.ToString());
            }

        }

        public IActionResult Index2()
        {
            return View();

        }
        public IActionResult Index3()
        {
            return View();

        }
        public ActionResult aboutus()
        {
            ViewBag.title = "درباره ما | کالای چوب سلطانی";
            return View();

        }

        public ActionResult contactus()
        {
            
            ViewBag.title = "تماس با ما | کالای چوب سلطانی";

            return View();

        }







        public JsonResult DuplicateUserName(string username)
        {

            var q = db.tbl_user.Where(a => a.username == username).SingleOrDefault();
            if (q != null)
            {
                return Json(false);

            }
            else
            {
                return Json(true);
            }

        }

        public JsonResult DuplicateEmail(string email)
        {

            var q = db.tbl_user.Where(a => a.email == email).SingleOrDefault();
            if (q != null)
            {
                return Json(false);

            }
            else
            {
                return Json(true);
            }

        }
      

        public ActionResult sendtext(tbl_contactus t)
        {
            try
            {

                //var q=db.tbl_newscomments .Where(a=> a.newsid == t.newsid ).SingleOrDefault ();

                t.sabtdate = DateTime.Now;
                t.ip = this.httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();


                db.tbl_contactus.Add(t);

                db.SaveChanges();
                Emailparameter emailp = new Emailparameter();
                emailp.from = "soltaniwoodproducts@gmail.com";
                emailp.password = "3821911038219110";
                emailp.smtp = "smtp.gmail.com";
                emailp.subject = string.Format("پرسش ثبت شده در سایت از طرف : {0}", t.email);
                emailp.to = "soltaniwoodproducts@gmail.com";
                string path = Path.Combine(_hostingEnvironment.WebRootPath, "/Content/text/contactus.txt");
                string Body = System.IO.File.ReadAllText(path);

                Body = Body.Replace("t.name", t.name);
                Body = Body.Replace("t.email", t.email);
                Body = Body.Replace("t.ip", t.ip);
                Body = Body.Replace("t.sabtdate", shamsi.ToShamsi(t.sabtdate).ToString());
                Body = Body.Replace("t.text", t.text);

                emailp.text = Body;


                if (Email.send(emailp))
                {
                    TempData["message"] = "متن پرسش شما با موفقیت ارسال گردید";
                    return RedirectToAction("contactus");
                }
                else
                {
                    TempData["message"] = "در ارسال پیام خطایی رخ داده است.  لطفاً آدرس ایمیل وارده را مجدداً بررسی فرمایید";
                    return RedirectToAction("contactus");
                }



            }
            catch (Exception e)
            {
                TempData["message"] = "در ثبت پرسش شما خطایی رخ داده است. مجدداً تلاش فرمایید";
                return RedirectToAction("contactus");
            }

        }


        public ActionResult registerforjob()
        {

            return View();

        }


        public ActionResult editapplicantinfo()
        {
            return View();
        }

        public ActionResult Recruitment(string codemelli, string cellnumber, int uniqekey = 0)
        {

            string attention = " در صورت تکمیل هر چه بیشتر و دقیق تر اطلاعات فوق, کارشناسان استخدام زود تر با شما ارتباط برقرار می کنند";


            int user = 0;

            if (User.Identity.IsAuthenticated)
            {
            user = int.Parse(HttpContext.User.Claims.FirstOrDefault().Value);

            }
            var access = db.tbl_accesslevels.Where(a => a.userid == user && a.accessid == 31 && a.accessvalue == true).SingleOrDefault();

            if (access == null)
            {


                var q = db.tbl_applicants.Where(a => a.codemelli == codemelli && a.cellnumber == cellnumber && a.uniqekey == uniqekey).SingleOrDefault();


                if (q != null)
                {
                    List<tbl_skillappinfo> lstskillinf = new List<tbl_skillappinfo>();
                    foreach (var item in db.tbl_skills)
                    {
                        if (q.tbl_skillappinfo.Where(a => a.appid == q.applicant_id && a.skillid == item.id).SingleOrDefault() == null)
                        {
                            tbl_skillappinfo t = new tbl_skillappinfo();
                            t.appid = q.applicant_id;
                            t.skillid = item.id;
                            t.skillvalue = "1";

                            lstskillinf.Add(t);
                        }

                    }

                    db.tbl_skillappinfo.AddRange(lstskillinf);
                    db.SaveChanges();

                    applicantrep ap = new applicantrep();
                    ap.applicant_id = q.applicant_id;
                    ap.applicant_image = q.applicant_image;
                    ap.birthday = q.birthday.HasValue == true ? q.birthday.Value : DateTime.Now;
                    ap.borncity = q.borncity;
                    ap.cellnumber = q.cellnumber;
                    ap.codemelli = q.codemelli;
                    ap.degree_id = q.degree_id;
                    ap.driverlicense = q.driverlicense;
                    ap.edu_field = q.edu_field;
                    ap.Expected_salary = q.Expected_salary;
                    ap.f_name = q.f_name;
                    ap.fathername = q.fathername;
                    ap.Insurance = q.Insurance;
                    ap.Insurancenumber = q.Insurancenumber;
                    ap.Insurancepriod = q.Insurancepriod;
                    ap.job_id = q.job_id;
                    ap.l_name = q.l_name;
                    ap.Married = q.Married;
                    ap.Mentalphysicalhealth = q.Mentalphysicalhealth;
                    ap.method_introduction = q.method_introduction;
                    ap.militaryservicestatus = q.militaryservicestatus;
                    ap.nationality = q.nationality;
                    ap.number_child = q.number_child;
                    ap.Religion = q.Religion;
                    ap.sabtdate = q.sabtdate;
                    ap.sex = q.sex;
                    ap.shenasnamenum = q.shenasnamenum;
                    ap.status_smoking = q.status_smoking;
                    ap.Type_cooperation = q.Type_cooperation;
                    ap.workingnow = q.workingnow;
                    TempData["message"] = attention;
                    return View(ap);
                }
                else
                {
                    TempData["message"] = "اطلاعات وارده صحیح نمی باشد";
                    return RedirectToAction("editapplicantinfo", "home");
                }

            }
            else
            {
                var q = db.tbl_applicants.Where(a => a.codemelli == codemelli && a.cellnumber == cellnumber).SingleOrDefault();




                if (q != null)
                {

                    List<tbl_skillappinfo> lstskillinf = new List<tbl_skillappinfo>();
                    foreach (var item in db.tbl_skills)
                    {
                        if (q.tbl_skillappinfo.Where(a => a.appid == q.applicant_id && a.skillid == item.id).SingleOrDefault() == null)
                        {
                            tbl_skillappinfo t = new tbl_skillappinfo();
                            t.appid = q.applicant_id;
                            t.skillid = item.id;
                            t.skillvalue = "1";

                            lstskillinf.Add(t);
                        }

                    }

                    db.tbl_skillappinfo.AddRange(lstskillinf);
                    db.SaveChanges();
                    applicantrep ap = new applicantrep();
                    ap.applicant_id = q.applicant_id;
                    ap.applicant_image = q.applicant_image;
                    ap.birthday = q.birthday.HasValue == true ? q.birthday.Value : DateTime.Now;
                    ap.borncity = q.borncity;
                    ap.cellnumber = q.cellnumber;
                    ap.codemelli = q.codemelli;
                    ap.degree_id = q.degree_id;
                    ap.driverlicense = q.driverlicense;

                    ap.edu_field = q.edu_field;
                    ap.Expected_salary = q.Expected_salary;
                    ap.f_name = q.f_name;
                    ap.fathername = q.fathername;
                    ap.Insurance = q.Insurance;
                    ap.Insurancenumber = q.Insurancenumber;
                    ap.Insurancepriod = q.Insurancepriod;
                    ap.job_id = q.job_id;
                    ap.l_name = q.l_name;
                    ap.Married = q.Married;
                    ap.Mentalphysicalhealth = q.Mentalphysicalhealth;
                    ap.method_introduction = q.method_introduction;
                    ap.militaryservicestatus = q.militaryservicestatus;
                    ap.nationality = q.nationality;
                    ap.number_child = q.number_child;
                    ap.Religion = q.Religion;
                    ap.sabtdate = q.sabtdate;
                    ap.sex = q.sex;
                    ap.shenasnamenum = q.shenasnamenum;
                    ap.status_smoking = q.status_smoking;
                    ap.Type_cooperation = q.Type_cooperation;
                    ap.workingnow = q.workingnow;
                    ap.uniqekey = q.uniqekey;
                    TempData["access"] = "okay";

                    return View(ap);
                }
                else
                {
                    TempData["message"] = "اطلاعات وارده صحیح نمی باشد";
                    return RedirectToAction("editapplicantinfo", "home");
                }
            }





        }

        public ActionResult saveskillsforapp(int appid, List<int> skillidlst, List<string> skillvaluelst)
        {

            int i = 0;

            List<tbl_skillappinfo> lsts = new List<tbl_skillappinfo>();
            foreach (var item in skillidlst)
            {

                tbl_skillappinfo t = new tbl_skillappinfo();
                t.appid = appid;
                t.skillid = item;
                t.skillvalue = skillvaluelst.ElementAt(i);
                i += 1;
                lsts.Add(t);
            }

            var qtodelete = db.tbl_skillappinfo.Where(a => a.appid == appid).ToList();
            db.tbl_skillappinfo.RemoveRange(qtodelete);
            db.SaveChanges();
            db.tbl_skillappinfo.AddRange(lsts);
            db.SaveChanges();
            return Json(true);
        }
        public ActionResult saveotherinfoapplicant(applicantrep t)
        {
            try
            {



                var app = db.tbl_applicants.Where(a => a.applicant_id == t.applicant_id).SingleOrDefault();
                app.f_name = t.f_name;
                app.l_name = t.l_name;

                app.birthday = t.birthday.HasValue == true ? shamsi.ToMiladi(t.birthday.Value) : DateTime.Now;

                app.borncity = t.borncity;
                app.degree_id = t.degree_id;
                app.edu_field = t.edu_field;
                app.Expected_salary = t.Expected_salary;
                app.fathername = t.fathername;
                app.Insurance = t.Insurance;
                app.Insurancenumber = t.Insurancenumber;
                app.Insurancepriod = t.Insurancepriod;
                app.job_id = t.job_id;
                app.Married = t.Married;
                app.Mentalphysicalhealth = t.Mentalphysicalhealth;
                app.method_introduction = t.method_introduction;
                app.militaryservicestatus = t.militaryservicestatus;
                app.nationality = t.nationality;
                app.number_child = t.number_child;
                app.Religion = t.Religion;
                app.sex = t.sex;
                app.shenasnamenum = t.shenasnamenum;
                app.status_smoking = t.status_smoking;
                app.workingnow = t.workingnow;
                app.Type_cooperation = t.Type_cooperation;
                app.cellnumber = t.cellnumber;
                app.uniqekey = t.uniqekey;
                db.SaveChanges();
               
                TempData["step"] = "1";
                return RedirectToAction("Recruitment", "home", new { codemelli = app.codemelli, cellnumber = app.cellnumber, uniqekey = app.uniqekey });

            }
            catch (Exception e)
            {

                throw e;
            }
        }



        public ActionResult applyformsave(applicantrepfirst model)
        {

            int user = (User.Identity.IsAuthenticated ? _userServices.GetUseridByUsername(User.Identity.Name) : 0);
            var accessok = db.tbl_accesslevels.Where(a => a.userid == user && a.accessid == 31 && a.accessvalue == true).SingleOrDefault();
            var q = db.tbl_applicants.Where(a => a.codemelli == model.codemelli).SingleOrDefault();
            if (q == null)
            {
                Random rd = new Random();
                model.uniqekey = rd.Next(100000, 1000000);
                model.sabtdate = DateTime.Now;
                tbl_applicants app = new tbl_applicants();
                app.birthday = shamsi.ToMiladi(DateTime.Parse(model.birthday));
                app.cellnumber = model.cellnumber;
                app.codemelli = model.codemelli;
                app.f_name = model.f_name;
                app.job_id = model.job_id;
                app.l_name = model.l_name;
                app.uniqekey = model.uniqekey;
                app.sabtdate = model.sabtdate;
                if (accessok != null)
                {
                    db.tbl_applicants.Add(app);
                    db.SaveChanges();
                    List<tbl_skillappinfo> lstskillinf = new List<tbl_skillappinfo>();


                    foreach (var item in db.tbl_skills)
                    {
                        tbl_skillappinfo t = new tbl_skillappinfo();
                        t.appid = app.applicant_id;
                        t.skillid = item.id;
                        t.skillvalue = "1";
                        lstskillinf.Add(t);

                    }

                    db.tbl_skillappinfo.AddRange(lstskillinf);
                    db.SaveChanges();



                    TempData["message"] = "اطلاعات اولیه متقاضی ثبت شد.";
                    return RedirectToAction("registerforjob", "home");
                }
                else
                {
                    db.tbl_applicants.Add(app);
                    db.SaveChanges();
                    List<tbl_skillappinfo> lstskillinf = new List<tbl_skillappinfo>();


                    foreach (var item in db.tbl_skills)
                    {
                        tbl_skillappinfo t = new tbl_skillappinfo();
                        t.appid = app.applicant_id;
                        t.skillid = item.id;
                        t.skillvalue = "1";
                        lstskillinf.Add(t);

                    }

                    db.tbl_skillappinfo.AddRange(lstskillinf);
                    db.SaveChanges();


                    ViewBag.applicantid = model.applicant_id;
                    TempData["message"] = "جهت تکمیل اطلاعات مربوط به فرم استخدام کد تائید برای شما پیامک شده است. ";
                    Cls_SMS.ClsSend sms_Single = new Cls_SMS.ClsSend();
                    string[] ret1 = new string[2];
                    string smstext = "کد ملی :" + model.codemelli + "\n" + "کد تائید :" + model.uniqekey.ToString() + "\n" + "جهت تکمیل اطلاعات متقاضی";
                    ret1 = sms_Single.SendSMS_Single(smstext, model.cellnumber.ToString(), "100008001", "koohi8", "87g5820", "http://193.104.22.14:2055/CPSMSService/Access", "KOOHI", false);

                    return RedirectToAction("registerforjob", "home");

                }



            }
            else
            {
                TempData["message"] = "این کد ملی قبلاً ثبت شده است.";
                return RedirectToAction("registerforjob", "home");
            }






        }

        public bool senduniqekey(string codemelli, string cellnumber)
        {

            Random rd = new Random();

            var q = db.tbl_applicants.Where(a => a.codemelli == codemelli && a.cellnumber == cellnumber).SingleOrDefault();
            if (q != null)
            {
                q.uniqekey = rd.Next(100000, 1000000);
                db.SaveChanges();
                Cls_SMS.ClsSend sms_Single = new Cls_SMS.ClsSend();
                string[] ret1 = new string[2];
                string smstext = "کد ملی :" + codemelli + "\n" + "کد تائید :" + q.uniqekey.ToString();
                ret1 = sms_Single.SendSMS_Single(smstext, q.cellnumber.ToString(), "100008001", "koohi8", "87g5820", "http://193.104.22.14:2055/CPSMSService/Access", "KOOHI", false);
                return true;
            }
            else
            {
                return false;
            }
        }



        [HttpPost]
        public ActionResult addcontact(int applicant_id, int contacttype_id, string contact_value)
        {

            string regexp = db.tbl_contactype.Where(a => a.id == contacttype_id).SingleOrDefault().Regexp;
            bool isnumber = Regex.IsMatch(contact_value, @regexp, RegexOptions.IgnoreCase);
            if (isnumber == true)
            {

                tbl_appcontactsinfo t = new tbl_appcontactsinfo();
                t.applicant_id = applicant_id;
                t.contacttype_id = contacttype_id;
                t.contact_value = contact_value;
                db.tbl_appcontactsinfo.Add(t);
                db.SaveChanges();
                var contact = db.tbl_appcontactsinfo.Where(a => a.id == t.id).Select(g => new { id = g.id, contacttype = g.contacttype_.contacttype, contact_value = g.contact_value });
                return Json(contact);
            }
            else
            {
                return Json(false);
            }


        }






        public bool delcontact(int id)
        {

            var q = db.tbl_appcontactsinfo.Find(id);
            if (q != null)
            {
                db.tbl_appcontactsinfo.Remove(q);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool delimage(int appid)
        {

            var q = db.tbl_applicants.Find(appid);
            if (q != null)
            {
                q.applicant_image = null;

                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }


        public ActionResult savehistoryjob(int appid, string companyName, string post, string jobname, string periodhistory, string leavingreson, string startdate, string enddate, int id = 0)
        {

            if (id == 0)
            {

                tbl_jobexperiences t = new tbl_jobexperiences();
                t.applicant_id = appid;
                t.CompanyName = companyName;
                t.jobname = jobname;
                t.leavingreson = leavingreson;
                t.post = post;
                t.Periodhistory = periodhistory;

                t.startdate = startdate != null ? shamsi.ToMiladi(DateTime.Parse(startdate.Trim())) : DateTime.Now;


                t.enddate = startdate != null ? shamsi.ToMiladi(DateTime.Parse(enddate.Trim())) : DateTime.Now;

                db.tbl_jobexperiences.Add(t);
                db.SaveChanges();
                var q = db.tbl_jobexperiences.Where(a => a.id == t.id).ToList().Select(g => new { id = g.id, companyName = g.CompanyName, post = g.post, jobname = g.jobname, leavingreson = g.leavingreson, periodhistory = g.Periodhistory, startdate = g.startdate.HasValue == true ? shamsi.ToShamsi(g.startdate.Value).ToString("yyyy/MM/dd") : "", enddate = g.enddate.HasValue == true ? shamsi.ToShamsi(g.enddate.Value).ToString("yyyy/MM/dd") : "" });
                return Json(q);
            }
            else
            {

                var q = db.tbl_jobexperiences.Where(a => a.id == id).SingleOrDefault();
                q.CompanyName = companyName;
                q.jobname = jobname;
                q.leavingreson = leavingreson;
                q.Periodhistory =periodhistory;
                q.post = post;
                if (startdate != "")
                {
                    q.startdate = shamsi.ToMiladi(DateTime.Parse(startdate));

                }
                if (enddate != "")
                {
                    q.enddate = shamsi.ToMiladi(DateTime.Parse(enddate));

                }
                db.SaveChanges();

                var q2 = db.tbl_jobexperiences.Where(a => a.id == id).ToList().Select(g => new { id = g.id, companyName = g.CompanyName, post = g.post, jobname = g.jobname, leavingreson = g.leavingreson, periodhistory = g.Periodhistory, startdate = g.startdate.HasValue == true ? shamsi.ToShamsi(g.startdate.Value).ToString("yyyy/MM/dd") : "", enddate = g.enddate.HasValue == true ? shamsi.ToShamsi(g.enddate.Value).ToString("yyyy/MM/dd") : "" });
                return Json(q2);
            }

        }

        public int deletehistoryjob(int id = 0)
        {

            var q = db.tbl_jobexperiences.Where(a => a.id == id).SingleOrDefault();
            db.tbl_jobexperiences.Remove(q);
            db.SaveChanges();
            return q.id;


        }



        public ActionResult gethistoryjobitem(int id = 0)
        {

            var q = db.tbl_jobexperiences.Where(a => a.id == id).ToList().Select(g => new { id = g.id, app = g.applicant_id, CompanyName = g.CompanyName, post = g.post, jobname = g.jobname, leavingreson = g.leavingreson, Periodhistory = g.Periodhistory, startdate = g.startdate.HasValue == true ? shamsi.ToShamsi(g.startdate.Value).ToString("yyyy/MM/dd") : "", enddate = g.enddate.HasValue == true ? shamsi.ToShamsi(g.enddate.Value).ToString("yyyy/MM/dd") : "" });
            return Json(q);




        }


        public ActionResult recoveryaccount()
        {
            return View();
        }

        public ActionResult emailforrecoveryaccount(email email)
        {
            var user = db.tbl_user.Where(a => a.email == email.emailaddress).SingleOrDefault();
            if (user == null)
            {
                TempData["message"] = "چنین ایمیلی در سیستم وجود ندارد";
            }
            else
            {
                //

                // ارسال ایمیل فعال سازی

                Emailparameter emailp = new Emailparameter();
                emailp.from = "soltaniwoodproducts@gmail.com";
                emailp.password = "3821911038219110";
                emailp.smtp = "smtp.gmail.com";
                emailp.subject = "ایمیل بازیابی حساب کاربری";
                emailp.to = email.emailaddress;
                emailp.text = "<div style='direction:rtl;text-align:right;'>برای بازیابی حساب کاربری خود در سایت کالای چوب سلطانی روی لینک زیر کلیک نمایید</div><div style='direction:ltr;text-align:left;'><a href='http://www.soltaniwoodproducts.com/Home/changepassword?username=" + user.username + "&&uniqkey=" + user.uniqkey + "'>http://www.soltaniwoodproducts.com/Home/changepassword?username=" + user.username + "&&uniqkey=" + user.uniqkey + "</a></div>";



                if (Email.send(emailp) == true)
                {
                    //return JavaScript(MessageBox.Show(" نام کاربری شما با موفقیت ثبت شد. برای فعال سازی به ایمیل خود مراجعه فرمایید. ", Location.topLeft, Type.success, Modal.WithModal));
                    TempData["message"] = "جهت بازیابی حساب کاربری خود به ایمیل خود مراجعه فرمایید";

                }
                else
                {
                    //return JavaScript(MessageBox.Show(" نام کاربری شما با موفقیت ثبت شد. جهت فعال سازی حساب کاربری کافی است نام کاربری خود را به شماره 09177017801 پیامک نمایید. ", Location.topLeft, Type.success, Modal.WithModal));
                    TempData["message"] = "خطایی اتفاق افتاده است. مجدداً تلاش نمایید";
                }

                //
            }
            return RedirectToAction("recoveryaccount");
        }

      
        public ActionResult uploadapplicantimage(int id, IFormFile img)
        {







            if (img != null)


            {

                using (var ms = new MemoryStream())
                {
                    img.CopyTo(ms);
                    byte[] b = ms.ToArray();
                    string s = Convert.ToBase64String(b);
                    // act on the Base64 data

                    System.Drawing.Image imgmem = System.Drawing.Image.FromStream(ms);
                    System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(imgmem, 300, 300);
                    System.IO.MemoryStream memThumbnail = new System.IO.MemoryStream();
                    bmp.Save(memThumbnail, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] bb = memThumbnail.ToArray();


                    var q = db.tbl_applicants.Where(a => a.applicant_id == id).SingleOrDefault();
                    q.applicant_image = bb;
                    db.SaveChanges();
                }
                var q2 = db.tbl_applicants.Where(a => a.applicant_id == id).ToList().Select(g => new { id = g.applicant_id, img = g.applicant_image });
                var result = JsonConvert.SerializeObject(q2);

                return Json(result);
            }
            else
            {
                return Json(null);
            }
        }


        public ActionResult Getcategory(int section_id)
        {
            var q = db.tbl_category.Where(a => a.section_id == section_id && (a.status.HasValue ? a.status.Value == true : 1 == 2)).ToList().Select(g => new { id = g.id, catgname = g.categoryname });
            return Json(q);
        }


    }
}