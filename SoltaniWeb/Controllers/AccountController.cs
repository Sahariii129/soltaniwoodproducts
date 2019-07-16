using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoltaniWeb.Models.Domain;
using SoltaniWeb.Models.Services.Interfaces;
using SoltaniWeb.Models.structs;
using SoltaniWeb.Models.utility;

namespace SoltaniWeb.Controllers
{

    public class AccountController : Controller
    {
        private IViewRenderService _viewRender;
        private IUserServices _userservices;

        public AccountController(IViewRenderService viewRender,IUserServices userServices)
        {
            _viewRender = viewRender;
            _userservices = userServices;
        }
        _4820_soltaniwebContext db = new _4820_soltaniwebContext();



        //[Route("Login")]
        public IActionResult LogIn()
        {

            loginstruct model = new loginstruct();

            return PartialView("~/Views/Shared/NewLayoutPartial/_PartialLogIn.cshtml", model);

        }
        [HttpPost]
        public IActionResult LogInAction([FromBody]loginstruct login)
        {
            if (ModelState.IsValid)
            {
                var Thisuser = db.tbl_user.SingleOrDefault(a => a.username == login.username && a.password == login.password);
                if (Thisuser == null)
                {
                    return Json(new { message = "کاربری با چنین مشخصات وجود ندارد", status = "NoUser" });

                }


                if (Thisuser.status)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,Thisuser.id.ToString()),
                        new Claim(ClaimTypes.Name,login.username),
                        //new Claim(nameof(Thisuser.Lname),Thisuser.Lname),

                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = login.RememberMe
                    };
                    HttpContext.SignInAsync(principal, properties);
                    // //fill accessibilityaction tables for this user

                    //ToDo in other part in this site;


                    //

                    return Json(new { message = "کاربر با موفقیت وارد سایت شد", status = "success", username = Thisuser.username });

                }
                else
                {
                    return Json(new { message = "کاربر غیر فعال می باشد", status = "notactive" });

                }



            }
            else
            {
                return Json(new { message = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToList(), status = "validationerror" });


            }

        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("index", "home");
        }


        public ActionResult register()
        {

            //statistics.statisticssave("Home", "register", Request.UserHostAddress, (Nullable<int>)Session["userid"]);
            RegisterViewModel model = new RegisterViewModel();
            return PartialView("~/Views/Shared/NewLayoutPartial/_PartialRegister.cshtml", model);

        }


        [HttpPost]
        public ActionResult registerAction([FromForm]RegisterViewModel R, IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { message = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToList(), status = "validationerror" });
            }
            try
            {

                tbl_user t = new tbl_user();

                if (image != null)
                {
                    if (image.Length > 2097152)
                    {
                        return Json(new JsonResults { jsonmessage = "حجم تصویر بیش از 2 مگابایت است", jsonstatus = "Error" });
                    }
                    if (image.ContentType != "image/jpeg")
                    {
                        return Json(new JsonResults { jsonmessage = "فرمت تصویر باید JPG باشد", jsonstatus = "Error" });
                    }
                    t.img = saveimageinsql.perform(image, true, 256, 256);
                }
                t.password = R.password;
                t.signupdate = DateTime.Now;
                t.status = false;
                t.Tokenstring = NameGenerator.GenerateUniqCode();
                Random rd = new Random();
                t.uniqkey = rd.Next(100, 10000);
                t.username = R.username;
                t.confirmpass = R.confirmpass;
                t.Access = 2;
                t.email = R.email;
                t.cellphone = R.cellphone;
                db.tbl_user.Add(t);
                db.SaveChanges();
                tbl_user user = db.tbl_user.SingleOrDefault(a => a.id == t.id);
                // ارسال ایمیل فعال سازی

                Emailparameter emailp = new Emailparameter();
                emailp.from = "soltaniwoodproducts@gmail.com";
                emailp.password = "3821911038219110";
                emailp.smtp = "smtp.gmail.com";
                emailp.subject = "ایمیل فعال سازی حساب کاربری";
                emailp.to = t.email;
                string body = _viewRender.RenderToStringAsync("_ActiveEmail", user);
                emailp.text = body;


                if (Email.send(emailp) == true)
                {
                    return Json(new JsonResults { jsonmessage = "نام کاربری شما با موفقیت ثبت شد. برای فعال سازی به ایمیل خود مراجعه فرمایید و بر روی لینک فعالسازی کلیک نمایید", jsonstatus = "SuccessRegisterEmail" });
                    //return Json(true);
                }
                else
                {
                    return Json(new JsonResults { jsonmessage = "نام کاربری شما با موفقیت ثبت شد. ایمیل مورد نظر فعال یا معتبر نمی باشد. لذا خواهشمند است جهت فعال سازی حساب کاربری خود نام کاربری خود را به شماره 09177017801 پیامک نمایید", jsonstatus = "SuccessRegisterFailEmail" });
                }

            }
            catch (Exception e)
            {

                return Json(new JsonResults { jsonmessage = e.Message, jsonstatus = "Exception" });
            }
        }

        public ActionResult ActiveAccount(string tokenstring)
        {
            var activecookie = Request.Cookies["activeacount"];
            int repeatnumber = (activecookie != null ? int.Parse(activecookie) : 0);
            if (repeatnumber > 3)
            {
                ViewBag.status = "Error";
                ViewBag.message = "بیش از حد از این لینک استفاده کرده اید";
                repeatnumber += 1;

                return View();
            }

            var user = db.tbl_user.Where(a => a.Tokenstring == tokenstring).SingleOrDefault();
            if (user == null)
            {
                ViewBag.status = "Error";
                ViewBag.message = "چنین کاربری وجود ندارد";
                repeatnumber += 1;
                Response.Cookies.Append("activeacount", repeatnumber.ToString(), new CookieOptions() { Expires = DateTime.Now.AddMinutes(10) });

                return View();
            }
            user.status = true;
            user.Tokenstring = NameGenerator.GenerateUniqCode();
            db.SaveChanges();
            _userservices.GiveAccessMenuToUser(user.id);
            ViewBag.status = "Success";
            ViewBag.message = "حساب کاربری شما فعال گردید.";
            repeatnumber += 1;
            Response.Cookies.Append("activeacount", repeatnumber.ToString(), new CookieOptions() { Expires = DateTime.Now.AddMinutes(10) });

            return View();
        }
       
        public ActionResult recoveryaccount()
        {
            return View();
        }
        [HttpPost]
        public ActionResult emailforrecoveryaccount([FromForm]email email)
        {
            var user = db.tbl_user.Where(a => a.email == email.emailaddress).SingleOrDefault();
            if (user==null)

            {
                return Json(new JsonResults { jsonmessage = "چنین ایمیلی در سیستم وجود ندارد", jsonstatus = "NoEmail" });

            }
            // ارسال ایمیل فعال سازی

            Emailparameter emailp = new Emailparameter();
            emailp.from = "soltaniwoodproducts@gmail.com";
            emailp.password = "3821911038219110";
            emailp.smtp = "smtp.gmail.com";
            emailp.subject = "ایمیل بازیابی حساب کاربری";
            emailp.to = email.emailaddress;
            string body = _viewRender.RenderToStringAsync("_ChangePassword", user);
            emailp.text = body;
            if (Email.send(emailp) == true)
            {
               
                return Json(new JsonResults { jsonmessage = "جهت بازیابی حساب کاربری خود به ایمیل خود مراجعه فرمایید", jsonstatus = "Success" });

            }
            else
            {
               return Json(new JsonResults { jsonmessage = "خطایی اتفاق افتاده است. مجدداً تلاش نمایید", jsonstatus = "Errorinsending" });

            }

        }

        public IActionResult ChangePassword(string tokenstring)
        {
            var changepasswordcookie = Request.Cookies["changepasswordcookie"];
            int repeatnumber = (changepasswordcookie != null ? int.Parse(changepasswordcookie) : 0);
            if (repeatnumber > 3)
            {
                ViewBag.status = "Error";
                ViewBag.message = "بیش از حد از این لینک استفاده کرده اید";
                repeatnumber += 1;

                return View();
            }
            var user = db.tbl_user.Where(a => a.Tokenstring == tokenstring).SingleOrDefault();
            if (user == null)
            {
                ViewBag.status = "Error";
                ViewBag.message = "چنین کاربری وجود ندارد";
                repeatnumber += 1;
                Response.Cookies.Append("changepasswordcookie", repeatnumber.ToString(), new CookieOptions() { Expires = DateTime.Now.AddMinutes(10) });

                return View();
            }
         
            ViewBag.status = "Success";
            //ViewBag.message = "حساب کاربری شما فعال گردید.";
            repeatnumber += 1;
            Response.Cookies.Append("changepasswordcookie", repeatnumber.ToString(), new CookieOptions() { Expires = DateTime.Now.AddMinutes(10) });

            return View(new userchangepasswordviewmodel {userid=user.id, username = user.username , password = user.password , confirmpass = user.confirmpass });
        }

        public IActionResult changepasswordAction([FromForm]userchangepasswordviewmodel newpass)
        {
            if (!ModelState.IsValid)
            {
                return Json(new  { jsonmessage = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToList(), jsonstatus = "validationerror" });
            }
            var user = db.tbl_user.Where(a => a.id == newpass.userid).SingleOrDefault();
            if (user==null)
            {
                return Json(new JsonResults { jsonmessage = "چنین کاربری وجود ندارد", jsonstatus = "NoUser" });

            }

            user.password = newpass.password;
            user.confirmpass = newpass.confirmpass;
            user.Tokenstring = NameGenerator.GenerateUniqCode();
            db.SaveChanges();
            return Json(new JsonResults { jsonmessage = "عملیات با موفقیت انجام شد", jsonstatus = "Success" });
        }
    }

}