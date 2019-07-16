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
    public class SWPAuthorizeIdentityController : ActionFilterAttribute 
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _4820_soltaniwebContext db = new _4820_soltaniwebContext();

          
            //
            int usernameid = 0;
            string action = filterContext.RouteData.Values["Action"].ToString();
            string controller= filterContext.RouteData.Values["Controller"].ToString();
            bool? permission = null;
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //usernameid = filterContext.HttpContext.Session.GetInt32("userid").Value;
                usernameid = int.Parse(filterContext.HttpContext.User.Claims.FirstOrDefault().Value);
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
                    ViewName = "~/Views/Home/AccessIsDenied.cshtml",
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