using Microsoft.AspNetCore.Mvc.Filters;
using SoltaniWeb.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SoltaniWeb.Controllers
{
    public class SWPAuthorizeController : ActionFilterAttribute 
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _4820_soltaniwebContext db = new _4820_soltaniwebContext();

         
            if (filterContext.HttpContext.Session.GetInt32("userid") == null)
            {

                if (filterContext.HttpContext.Request.Cookies["userid"] != null)
                {
                    filterContext.HttpContext.Session.SetString("username", filterContext.HttpContext.Request.Cookies["username"]);
                    filterContext.HttpContext.Session.SetInt32("userid", int.Parse(filterContext.HttpContext.Request.Cookies["userid"]));

                    int userid = filterContext.HttpContext.Session.GetInt32("userid").Value;
                    string fname = db.tbl_user.Where(a => a.id == userid).SingleOrDefault().name;
                    string lname = db.tbl_user.Where(a => a.id == userid).SingleOrDefault().Lname;
                    string fullname = fname + " " + lname;
                    filterContext.HttpContext.Session.SetString("fullname", fullname);

                }
            }

            //
            int usernameid = 0;
            string action = filterContext.RouteData.Values["Action"].ToString();
            string controller= filterContext.RouteData.Values["Controller"].ToString();
            bool? permission = null;
            if (filterContext.HttpContext.Session.GetInt32("userid") != null)
            {
                usernameid = filterContext.HttpContext.Session.GetInt32("userid").Value;
                var q = db.tbl_actionaccessibility.Where(a => a.acction_.action_name.ToLower() == action.ToLower() && a.acction_.controller_.controller_name.ToLower() == (controller.ToLower() + "controller").ToLower() && a.userid == usernameid).FirstOrDefault();
                if (q != null)
                {
                    int idd = q.id;
                    permission = q.permission;
                }
                else
                {
                    permission = false;
                }
            }

            if (permission==null)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/Home/login.cshtml",
                };
            }else if(permission == false)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/Home/AccessIsDenied.cshtml",
                };
            }


        }


    }





}