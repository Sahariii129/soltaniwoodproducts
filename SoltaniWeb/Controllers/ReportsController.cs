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
using Microsoft.AspNetCore.Hosting;
using Stimulsoft.Base;
using Stimulsoft.Report.Mvc;
using Stimulsoft.Report;
using Stimulsoft.Report.Export;
using SoltaniWeb.Models.Services.Interfaces;

namespace core970910.Controllers
{

    public class ReportsController : Controller
    {
        // GET: Reports
        _4820_soltaniwebContext db = new _4820_soltaniwebContext();

        private readonly IHubContext<ChatHub> _hub;
        private IHostingEnvironment _hostingEnvironment;
        private IUserServices _userServices;

        public ReportsController(IHubContext<ChatHub> hub, IHostingEnvironment environment, IUserServices userServices)
        {
            _hostingEnvironment = environment;
            _hub = hub;
            _userServices = userServices;
            StiLicense.LoadFromString("6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHl2AD0gPVknKsaW0un+3PuM6TTcPMUAWEURKXNso0e5OJN40hxJjK5JbrxU+NrJ3E0OUAve6MDSIxK3504G4vSTqZezogz9ehm+xS8zUyh3tFhCWSvIoPFEEuqZTyO744uk+ezyGDj7C5jJQQjndNuSYeM+UdsAZVREEuyNFHLm7gD9OuR2dWjf8ldIO6Goh3h52+uMZxbUNal/0uomgpx5NklQZwVfjTBOg0xKBLJqZTDKbdtUrnFeTZLQXPhrQA5D+hCvqsj+DE0n6uAvCB2kNOvqlDealr9mE3y978bJuoq1l4UNE3EzDk+UqlPo8KwL1XM+o1oxqZAZWsRmNv4Rr2EXqg/RNUQId47/4JO0ymIF5V4UMeQcPXs9DicCBJO2qz1Y+MIpmMDbSETtJWksDF5ns6+B0R7BsNPX+rw8nvVtKI1OTJ2GmcYBeRkIyCB7f8VefTSOkq5ZeZkI8loPcLsR4fC4TXjJu2loGgy4avJVXk32bt4FFp9ikWocI9OQ7CakMKyAF6Zx7dJF1nZw");
        }



        public ActionResult purchasecartreport(int cartid = 0)
        {

            int userid = _userServices.GetUseridByUsername(User.Identity.Name);

            var q = db.tbl_purchasekart.Where(a => a.id == cartid && a.userid == userid).SingleOrDefault();
            var thisuser = db.tbl_user.Where(a => a.id == userid).SingleOrDefault();

            if (thisuser.Access != 0)
            {

                if (q == null)
                {
                    return RedirectToAction("login", "home");
                }
            }
            //StimulsoftLicenseHelper.Activate();

            //step 2: set license key
           //var lic = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHlNHxonFYHbpjDx+27slnZs5ftj9P0vr+YW2Pss00P90qmOB2PRI4SjLhAufUnckk8d8LLVu4bgMi/81ymyeO7J3xLjnveXaZmAwzelnxcpnNxi4aUQulVJFvwK+qiOxltk2avtYSEE8jmq4WkdUTp9xske7sHZRhrGePQV5sn54z+RtcNe6cUUNgTwUc8dglgPK/WFe8jcs9XjzaRp3RM+l4PfDluVrp1RaQNJsb10wa5XZT2xv3nuWlW9OMBsPLc/63Hsm5zhEfIP3P6J0pV9x/P2lHAiXuDGKMbeKntP0fxrqxRGrhelfT9AV1O5eDpofH8TAi9HvDPn6LHKsddVVcRfXq658kn94eQWyZkEwfPQJWgQUmXhPcZbj5ecea/dnruFjI+1Y1sxLbJA2wzzYtCU2NEj0kAfxId97OiRITg7oQZs2IMiaOFJgvDL1hR5uv69OffNFZk6+h2bm8OB9GF/PZuisO+BzM+HQRlPQP8FWo6jd79o03LiCtH6mVOrQfEAhF8r0qeF7y8TV2OJ8hHnve7mXKTXr4KmGkRzzmDmfAMrpfoPFF4HtglInJTHbKYqiH2z1DDpJVUNa0+kn/Y9RzSZLtIRBVNzgUMubsB+CPJWMal80oD9bTW8/I3O7QFgvSGIUgCaFAs3rYlkJ3Obt7AVXexGQUuTph7Z4auM4/V4DDhhvdy+8+PIj1/kAilviTYXWxqhbLfciZOEMHeGCtepilOvgvY7arGeSqv5tUfIPqoyBh6RSczg+Hf0soFq0t7ysW3FyteYsfsbHppN1htRrfcP3fEn89vddsamdUgUBgZJ8nDKZ+4E0d+g3w+Jdegnu7PAjNjd5YKLt85NfBRwktdWGwp7fKvK2NpidjRVXRHi1CYlcDoxeaCqOqW7/vPv668X6omx6EsYOyWiFskWQxm/E7dpA4ixmNOyMgolUMRbqQK1uVIT1xeavP3r3C9Pm5f4pBkzpUxX54pc6Z+T/psyIXfR77C+kVXEt3+TA9TX+jZ7Zi8oSX1ImDJWn9wPFMCl11Gl3vHTYno2cvK2zeM8vasV/Q+C9GRJkXkDEceDLHaFbDiWQaJi4TJex/1sbYS239UhciSOVizEhpdVA1E7D6vPzSJ9TQjgWtrbANDmjpHzK2YiTnvbUKf9FibYLmtl0iJ3WUhzO1R03jDUJSvF030BPxGWUt5UMTSpaImLTboUbWbCkLnkDXPyxT9xZORjgWTQzVYNUmCXW2qEtzOyVbB+m16KzWvFf69QI1zMEo/N6a1JzT7499RnmqkuqPyBESnVvAaG7Qfz8jff5tK6ml+BTsCRX4MsHV/Sh100Tv4Kt3raiWKyBG6L2UVbKvtaFnGSYPJzNSY6ndlrxWC65VSSbeXxR6AzKVjLw9sgJLh9i6IQ5d5cUfEFzFKNWvCPDiEERKPpG2Vj4yR3HoayBrgpRqgaLAq1/V//hlWT/dcRIevJGjDgbZGv2E1KOiB4Llj/HX0HAO7cCZrtM57hf5jMnqGDvkYSOOHTPqMPv9QbLdBX+5RnHoa8xtXPp1vVPbJXZHJ/f9rVOnGCqjtR6pWAGDYOOZkXZ02y0zjow0go2JuCZnzYZw37U12hcrb2gxPAlmy/SQerFJXaySRLSXu0QW1pfw==";
           // StiLicense.Key = lic;
            return View();
        }
        public ActionResult ViewerEventpcart()
        {
            return StiNetCoreViewer.ViewerEventResult(this);
            //return StiMvcViewer.ViewerEventResult();
            //return StiNetCoreViewer.ViewerEventResult();
        }
        public ActionResult ViewerEventorder()
        {
            //return StiMvcViewer.ViewerEventResult();
            return StiNetCoreViewer.ViewerEventResult(this);
        }
        public ActionResult ViewerEventpricereport()
        {
            //return StiMvcViewer.ViewerEventResult();
            return StiNetCoreViewer.ViewerEventResult(this);
        }
        public ActionResult GetReportofpcart(int cartid = 0)
        {
            var cart = db.tbl_purchasekart.ToList().Where(a => a.id == cartid).Select(g => new
            {
                id = g.id.ToString(),
                username = g.user.username,
                paymentdate = shamsi.ToShamsi(g.purchasedateend.Value).ToString("yyyy/MM/dd"),
                fullname = g.user.name + " " + g.user.Lname,
                email = g.user.email,
                cellphone = g.user.tbl_usermoreinfo.Count() != 0 ? g.user.tbl_usermoreinfo.FirstOrDefault().cellphone1 : "",
                paymenttime = g.purchasedateend.Value.ToString("HH:mm"),
                ispaid = g.ispaid == true ? "تسویه شده " : "عدم تسویه",
                isdeleverd = g.isdeleverd == true ? "تحویل مشتری" : "عدم تحویل"
            });
            var cartitems = db.tbl_purchasekartitemlist.Where(a => a.perchasekart_id == cartid).ToList().Select(g => new
            {
                productid = g.product_id.ToString(),
                Catgname = g.product_.category.categoryname,
                productname = g.product_.name + "   " + g.product_.codename,
                weight = g.product_.weight.ToString() + "kg",
                number = g.number.ToString(),
                price = string.Format("{0:#,##0.##}", g.price),
                totalprice = string.Format("{0:#,##0.##}", g.price * g.number),


            });
            abstractinfoofpurchasecart getabstractinfo = new abstractinfoofpurchasecart();

            var abstractinfo = getabstractinfo.getabstractcartinfo(cartid);


            StiReport report = new StiReport();
           
            report.Load(Path.Combine(_hostingEnvironment.WebRootPath, "/Content/reports/Reportcart.mrt"));
            report.RegData("purchasecartinfo", cart);
            report.RegData("purchasecartitems", cartitems);
            report.RegData("abstractinfo", abstractinfo);
            CheckReference(report);

            //return StiMvcViewer.GetReportResult(report);
            return StiNetCoreViewer.GetReportResult(this, report);
        }
        private void CheckReference(StiReport report)
        {
            string assemblyName = Assembly.GetExecutingAssembly().ManifestModule.Name;
            List<string> refs = new List<string>(report.ReferencedAssemblies);
            if (!refs.Contains(assemblyName))
            {
                refs.Add(assemblyName);
                report.ReferencedAssemblies = refs.ToArray();
            }
        }


        public ActionResult pricelistbygrade()
        {
            //StimulsoftLicenseHelper.Activate();

            //// step 2: set license key
            //var lic = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHlNHxonFYHbpjDx+27slnZs5ftj9P0vr+YW2Pss00P90qmOB2PRI4SjLhAufUnckk8d8LLVu4bgMi/81ymyeO7J3xLjnveXaZmAwzelnxcpnNxi4aUQulVJFvwK+qiOxltk2avtYSEE8jmq4WkdUTp9xske7sHZRhrGePQV5sn54z+RtcNe6cUUNgTwUc8dglgPK/WFe8jcs9XjzaRp3RM+l4PfDluVrp1RaQNJsb10wa5XZT2xv3nuWlW9OMBsPLc/63Hsm5zhEfIP3P6J0pV9x/P2lHAiXuDGKMbeKntP0fxrqxRGrhelfT9AV1O5eDpofH8TAi9HvDPn6LHKsddVVcRfXq658kn94eQWyZkEwfPQJWgQUmXhPcZbj5ecea/dnruFjI+1Y1sxLbJA2wzzYtCU2NEj0kAfxId97OiRITg7oQZs2IMiaOFJgvDL1hR5uv69OffNFZk6+h2bm8OB9GF/PZuisO+BzM+HQRlPQP8FWo6jd79o03LiCtH6mVOrQfEAhF8r0qeF7y8TV2OJ8hHnve7mXKTXr4KmGkRzzmDmfAMrpfoPFF4HtglInJTHbKYqiH2z1DDpJVUNa0+kn/Y9RzSZLtIRBVNzgUMubsB+CPJWMal80oD9bTW8/I3O7QFgvSGIUgCaFAs3rYlkJ3Obt7AVXexGQUuTph7Z4auM4/V4DDhhvdy+8+PIj1/kAilviTYXWxqhbLfciZOEMHeGCtepilOvgvY7arGeSqv5tUfIPqoyBh6RSczg+Hf0soFq0t7ysW3FyteYsfsbHppN1htRrfcP3fEn89vddsamdUgUBgZJ8nDKZ+4E0d+g3w+Jdegnu7PAjNjd5YKLt85NfBRwktdWGwp7fKvK2NpidjRVXRHi1CYlcDoxeaCqOqW7/vPv668X6omx6EsYOyWiFskWQxm/E7dpA4ixmNOyMgolUMRbqQK1uVIT1xeavP3r3C9Pm5f4pBkzpUxX54pc6Z+T/psyIXfR77C+kVXEt3+TA9TX+jZ7Zi8oSX1ImDJWn9wPFMCl11Gl3vHTYno2cvK2zeM8vasV/Q+C9GRJkXkDEceDLHaFbDiWQaJi4TJex/1sbYS239UhciSOVizEhpdVA1E7D6vPzSJ9TQjgWtrbANDmjpHzK2YiTnvbUKf9FibYLmtl0iJ3WUhzO1R03jDUJSvF030BPxGWUt5UMTSpaImLTboUbWbCkLnkDXPyxT9xZORjgWTQzVYNUmCXW2qEtzOyVbB+m16KzWvFf69QI1zMEo/N6a1JzT7499RnmqkuqPyBESnVvAaG7Qfz8jff5tK6ml+BTsCRX4MsHV/Sh100Tv4Kt3raiWKyBG6L2UVbKvtaFnGSYPJzNSY6ndlrxWC65VSSbeXxR6AzKVjLw9sgJLh9i6IQ5d5cUfEFzFKNWvCPDiEERKPpG2Vj4yR3HoayBrgpRqgaLAq1/V//hlWT/dcRIevJGjDgbZGv2E1KOiB4Llj/HX0HAO7cCZrtM57hf5jMnqGDvkYSOOHTPqMPv9QbLdBX+5RnHoa8xtXPp1vVPbJXZHJ/f9rVOnGCqjtR6pWAGDYOOZkXZ02y0zjow0go2JuCZnzYZw37U12hcrb2gxPAlmy/SQerFJXaySRLSXu0QW1pfw==";
            //StiLicense.Key = lic;
            var lic = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHlyBxErOHyaSR9igWOmIpe54D459EnV27B85mUVtGMJoAJ5J4RRiq3EfolQPYY1F49n0mb+F3MG+0ljOxwN4HATAf6fYoKQ1/iUgY6vaEeC5sHh8gfGTprH6x1fC/NVcPUJ/i6KWSxBY4nIv4lRlX3HOUO3USw0i4dW/9jx9b0QNwcDq4rFeRQw7kTTgkRKmY919lyMvTzfZR8CDGeZLM/HMpmj4ECf64h80SPx1SsMENkOUYkdxWM6RgqOgEc44sMRrb+fGswOItPe+m4VPrei7ioUJDxdHtMlA6PPimgjVQdmfF71YEZn01y2M3by3Y3NoELAswiU5MMV2QeaiPTV/eUpEUyICJPT8qLm8NvbBJytSFUM6wCi7NM6rSsjFaVWmpSmfZNJYIGS1DjNadeN62H7tGzPd8WwmKojrERdKtV1df3Uh3FK72EIyeIvc6E5XQCemL1C9dNvvcXaxSDRwPP2ax7In67L6Wb6pOKS+4KqxU+ga/dUN0ayxngW/BIl/KCSaMXCot2h1L/1q0x7";
            StiLicense.Key = lic;
            return View();
        }

        public ActionResult GetReportofpriceproducts()
        {


            //db.Database.CommandTimeout = 2000;
            var pricelist = db.tbl_products.Where(a => a.category.status.Value == true && a.category.section_.status.Value == true).ToList().GroupBy(a => new { categoryid = a.categoryid, categoryname = a.category.categoryname, grade = a.grade != null ? a.grade.ToUpper() : "", sectionid = a.category.section_id.Value, secionname = a.category.section_.name, order = a.category.section_.ordering.Value });
            var productpricelist = pricelist
            .Select(g => new
            {
                sectionid = g.Key.sectionid,
                sectionname = g.Key.secionname,
                categoryid = g.Key.categoryid,
                categoryname = g.Key.categoryname,
                firstproductname = g.FirstOrDefault() != null ? g.FirstOrDefault().name : "",
                codename = g.FirstOrDefault() != null ? g.FirstOrDefault().codename : "",
                categorygradesellprice = g.FirstOrDefault() != null ? (g.FirstOrDefault().lastcellcost.HasValue == true ? string.Format("{0:#,##0.##}", g.FirstOrDefault().lastcellcost.Value) : "") : "",
                sellpricedate = g.FirstOrDefault() != null ? g.FirstOrDefault().updatesellcost.ToPersianDate() : "",
                categorygradebuyprice = g.FirstOrDefault() != null ? (g.FirstOrDefault().lastbuycost.HasValue == true ? string.Format("{0:#,##0.##}", g.FirstOrDefault().lastbuycost.Value) : "") : "",
                buypricedate = g.FirstOrDefault() != null ? g.FirstOrDefault().updatebuycost.ToPersianDate() : "",
                grade = g.Key.grade.ToString(),
                order = g.Key.order,
                categorygradesellpriceorder = g.FirstOrDefault() != null ? (g.FirstOrDefault().lastcellcost.HasValue == true ? g.FirstOrDefault().lastcellcost.Value : 0) : 0,

            }).OrderBy(a => a.categorygradesellpriceorder);

            report_obj rep = new report_obj();
            rep.Report_Name = "فهرست قیمت محصولات به تفکیک کلاس";
            rep.Report_Date = DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ":" + DateTime.Now.TimeOfDay.Seconds + "-" + DateTime.Now.ToPersianDate();
            List<report_obj> lrep = new List<report_obj>();
            lrep.Add(rep);

            StiReport report = new StiReport();
            report.Load(StiNetCoreHelper.MapPath(this, "wwwroot/Content/reports/Reportproductprice.mrt"));
            //report.Load(Path.Combine(_hostingEnvironment.WebRootPath, "/Content/reports/Reportproductprice.mrt"));
            report.RegData("pricelist", productpricelist);

            report.RegBusinessObject("Report_Desc", lrep.ToList());
            CheckReference(report);

            //return StiMvcViewer.GetReportResult(report);
            return StiNetCoreViewer.ViewerEventResult(this);

        }

        public ActionResult orderreport(int orderid = 0)
        {
            //StimulsoftLicenseHelper.Activate();
            

            // step 2: set license key
            //var lic = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHlNHxonFYHbpjDx+27slnZs5ftj9P0vr+YW2Pss00P90qmOB2PRI4SjLhAufUnckk8d8LLVu4bgMi/81ymyeO7J3xLjnveXaZmAwzelnxcpnNxi4aUQulVJFvwK+qiOxltk2avtYSEE8jmq4WkdUTp9xske7sHZRhrGePQV5sn54z+RtcNe6cUUNgTwUc8dglgPK/WFe8jcs9XjzaRp3RM+l4PfDluVrp1RaQNJsb10wa5XZT2xv3nuWlW9OMBsPLc/63Hsm5zhEfIP3P6J0pV9x/P2lHAiXuDGKMbeKntP0fxrqxRGrhelfT9AV1O5eDpofH8TAi9HvDPn6LHKsddVVcRfXq658kn94eQWyZkEwfPQJWgQUmXhPcZbj5ecea/dnruFjI+1Y1sxLbJA2wzzYtCU2NEj0kAfxId97OiRITg7oQZs2IMiaOFJgvDL1hR5uv69OffNFZk6+h2bm8OB9GF/PZuisO+BzM+HQRlPQP8FWo6jd79o03LiCtH6mVOrQfEAhF8r0qeF7y8TV2OJ8hHnve7mXKTXr4KmGkRzzmDmfAMrpfoPFF4HtglInJTHbKYqiH2z1DDpJVUNa0+kn/Y9RzSZLtIRBVNzgUMubsB+CPJWMal80oD9bTW8/I3O7QFgvSGIUgCaFAs3rYlkJ3Obt7AVXexGQUuTph7Z4auM4/V4DDhhvdy+8+PIj1/kAilviTYXWxqhbLfciZOEMHeGCtepilOvgvY7arGeSqv5tUfIPqoyBh6RSczg+Hf0soFq0t7ysW3FyteYsfsbHppN1htRrfcP3fEn89vddsamdUgUBgZJ8nDKZ+4E0d+g3w+Jdegnu7PAjNjd5YKLt85NfBRwktdWGwp7fKvK2NpidjRVXRHi1CYlcDoxeaCqOqW7/vPv668X6omx6EsYOyWiFskWQxm/E7dpA4ixmNOyMgolUMRbqQK1uVIT1xeavP3r3C9Pm5f4pBkzpUxX54pc6Z+T/psyIXfR77C+kVXEt3+TA9TX+jZ7Zi8oSX1ImDJWn9wPFMCl11Gl3vHTYno2cvK2zeM8vasV/Q+C9GRJkXkDEceDLHaFbDiWQaJi4TJex/1sbYS239UhciSOVizEhpdVA1E7D6vPzSJ9TQjgWtrbANDmjpHzK2YiTnvbUKf9FibYLmtl0iJ3WUhzO1R03jDUJSvF030BPxGWUt5UMTSpaImLTboUbWbCkLnkDXPyxT9xZORjgWTQzVYNUmCXW2qEtzOyVbB+m16KzWvFf69QI1zMEo/N6a1JzT7499RnmqkuqPyBESnVvAaG7Qfz8jff5tK6ml+BTsCRX4MsHV/Sh100Tv4Kt3raiWKyBG6L2UVbKvtaFnGSYPJzNSY6ndlrxWC65VSSbeXxR6AzKVjLw9sgJLh9i6IQ5d5cUfEFzFKNWvCPDiEERKPpG2Vj4yR3HoayBrgpRqgaLAq1/V//hlWT/dcRIevJGjDgbZGv2E1KOiB4Llj/HX0HAO7cCZrtM57hf5jMnqGDvkYSOOHTPqMPv9QbLdBX+5RnHoa8xtXPp1vVPbJXZHJ/f9rVOnGCqjtR6pWAGDYOOZkXZ02y0zjow0go2JuCZnzYZw37U12hcrb2gxPAlmy/SQerFJXaySRLSXu0QW1pfw==";
            //StiLicense.Key = lic;

            //Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnO...";
            //Stimulsoft.Base.StiLicense.LoadFromFile("license.key");

            return View();
        }

        public ActionResult orderexporttopdf (int orderid)
        {

            //StimulsoftLicenseHelper.Activate();
            //Stimulsoft.Base.StiActivator

            // step 2: set license key
            //var lic = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHlNHxonFYHbpjDx+27slnZs5ftj9P0vr+YW2Pss00P90qmOB2PRI4SjLhAufUnckk8d8LLVu4bgMi/81ymyeO7J3xLjnveXaZmAwzelnxcpnNxi4aUQulVJFvwK+qiOxltk2avtYSEE8jmq4WkdUTp9xske7sHZRhrGePQV5sn54z+RtcNe6cUUNgTwUc8dglgPK/WFe8jcs9XjzaRp3RM+l4PfDluVrp1RaQNJsb10wa5XZT2xv3nuWlW9OMBsPLc/63Hsm5zhEfIP3P6J0pV9x/P2lHAiXuDGKMbeKntP0fxrqxRGrhelfT9AV1O5eDpofH8TAi9HvDPn6LHKsddVVcRfXq658kn94eQWyZkEwfPQJWgQUmXhPcZbj5ecea/dnruFjI+1Y1sxLbJA2wzzYtCU2NEj0kAfxId97OiRITg7oQZs2IMiaOFJgvDL1hR5uv69OffNFZk6+h2bm8OB9GF/PZuisO+BzM+HQRlPQP8FWo6jd79o03LiCtH6mVOrQfEAhF8r0qeF7y8TV2OJ8hHnve7mXKTXr4KmGkRzzmDmfAMrpfoPFF4HtglInJTHbKYqiH2z1DDpJVUNa0+kn/Y9RzSZLtIRBVNzgUMubsB+CPJWMal80oD9bTW8/I3O7QFgvSGIUgCaFAs3rYlkJ3Obt7AVXexGQUuTph7Z4auM4/V4DDhhvdy+8+PIj1/kAilviTYXWxqhbLfciZOEMHeGCtepilOvgvY7arGeSqv5tUfIPqoyBh6RSczg+Hf0soFq0t7ysW3FyteYsfsbHppN1htRrfcP3fEn89vddsamdUgUBgZJ8nDKZ+4E0d+g3w+Jdegnu7PAjNjd5YKLt85NfBRwktdWGwp7fKvK2NpidjRVXRHi1CYlcDoxeaCqOqW7/vPv668X6omx6EsYOyWiFskWQxm/E7dpA4ixmNOyMgolUMRbqQK1uVIT1xeavP3r3C9Pm5f4pBkzpUxX54pc6Z+T/psyIXfR77C+kVXEt3+TA9TX+jZ7Zi8oSX1ImDJWn9wPFMCl11Gl3vHTYno2cvK2zeM8vasV/Q+C9GRJkXkDEceDLHaFbDiWQaJi4TJex/1sbYS239UhciSOVizEhpdVA1E7D6vPzSJ9TQjgWtrbANDmjpHzK2YiTnvbUKf9FibYLmtl0iJ3WUhzO1R03jDUJSvF030BPxGWUt5UMTSpaImLTboUbWbCkLnkDXPyxT9xZORjgWTQzVYNUmCXW2qEtzOyVbB+m16KzWvFf69QI1zMEo/N6a1JzT7499RnmqkuqPyBESnVvAaG7Qfz8jff5tK6ml+BTsCRX4MsHV/Sh100Tv4Kt3raiWKyBG6L2UVbKvtaFnGSYPJzNSY6ndlrxWC65VSSbeXxR6AzKVjLw9sgJLh9i6IQ5d5cUfEFzFKNWvCPDiEERKPpG2Vj4yR3HoayBrgpRqgaLAq1/V//hlWT/dcRIevJGjDgbZGv2E1KOiB4Llj/HX0HAO7cCZrtM57hf5jMnqGDvkYSOOHTPqMPv9QbLdBX+5RnHoa8xtXPp1vVPbJXZHJ/f9rVOnGCqjtR6pWAGDYOOZkXZ02y0zjow0go2JuCZnzYZw37U12hcrb2gxPAlmy/SQerFJXaySRLSXu0QW1pfw==";
            //StiLicense.Key = lic;
            var order = db.tbl_order.ToList().Where(a => a.id == orderid).Select(g => new
            {
                id = g.id.ToString(),
                username = g.user.username,
                sodoordate = g.sodoor_date.ToPersianDate(),
                sodoortime = g.sodoor_date.ToShortTimeString(),
                fullname = g.orderfor.Fname + " " + g.orderfor.Lname,
                cellphone = g.orderfor.cell,
                frombranch = g.from_branch.branch_name,
                tobranch = g.to_branch.branch_name,
                sharh = g.sharh,
                done = (g.done == false ? "نزد " + g.to_branch.branch_name : "تحویل شده"),
                address = g.orderfor.address,
                frombranch_address = g.from_branch.Address,
                tobranch_address = g.to_branch.Address
            });


            var orderitems = db.tbl_orderdetails.Where(a => a.order_id == orderid).ToList().Select(g => new
            {
                productid = g.product_id.ToString(),
                Catgname = g.product_.category.categoryname,
                productname = g.product_.category.categoryname + "| " + g.product_.name + "| " + g.product_.codename + "| " + g.product_.dimension,
                weight = g.product_.weight.ToString() + "kg",
                number = g.number.ToString(),
                totalweight = (g.product_.weight.HasValue ? (g.product_.weight.Value * g.number).ToString() : ""),
                desc = g.description



            });

            int branch = db.tbl_order.Where(a => a.id == orderid).SingleOrDefault().from_branchid;

            StiReport report = new StiReport();
            string webroot = _hostingEnvironment.WebRootPath;
            string myroot = "/Content/reports/Reportorder.mrt";
            string targetroot = webroot + myroot;
            //report.Load(Path.Combine(_hostingEnvironment.WebRootPath, "/Content/reports/Reportorder.mrt"));
            report.Load(targetroot);
            //report.Load(Path.Combine(_hostingEnvironment.WebRootPath, "/Content/reports/Reportorder.mrt"));
            report.RegData("orderinfo", order);
            report.RegData("orderitems", orderitems);
            report.Render();
            MemoryStream stream = new MemoryStream();

        
            StiPdfExportService pdfexport = new StiPdfExportService();
            StiPdfExportSettings st = new StiPdfExportSettings();
            
            pdfexport.ExportPdf(report, stream,st);
            
            stream.Position = 0;
          
            
            byte[] b = stream.ToArray();
            tbl_filestosend t = new tbl_filestosend();
            t.caption = "حواله " + order.SingleOrDefault().id;
            t.filecontent = b;
            t.uploaddatetime = DateTime.Now;
            t.imagename = "order"+ order.SingleOrDefault().id+ ".pdf";
            t.user_id =int.Parse(User.Claims.FirstOrDefault().Value);
             t.filecontent = b;

            db.tbl_filestosend.Add(t);
            db.SaveChanges();
            //
            //var context = GlobalHost.ConnectionManager.GetHubContext<chatHub>();





            string msgframe = "";
            msgframe += "<p class='msgtext'>حواله " + orderid + " جهت آقای "+ order.SingleOrDefault().fullname + "</p>";
            msgframe += "<a target='_blank' class='btn btn-success' href='/admin/DownLoadFile/" + t.id + "'>دانلود فایل</a>";
            string mymsgpure = $"حواله {orderid} جهت آقای {order.SingleOrDefault().fullname}";
            string thisdatetime = order.SingleOrDefault().sodoordate + "  " + order.SingleOrDefault().sodoortime;

            tbl_signalRmsg msg = new tbl_signalRmsg { from_userid = int.Parse(User.Claims.FirstOrDefault().Value), msg_text = msgframe, datetime_send = DateTime.Now, visibleforall = true, visibletofrom = true, visibletoto = true,msg_textpure= mymsgpure };
            db.tbl_signalRmsg.Add(msg);
            db.SaveChanges();

            _hub.Clients.All.SendAsync("addNewMessage", msg.id, User.Claims.FirstOrDefault().Value, User.Identity.Name, msgframe, thisdatetime, "public", branch);



            return Json(true);

            
        }

        public ActionResult GetReportoforder(int orderid = 0)
        {
            var order = db.tbl_order.ToList().Where(a => a.id == orderid).Select(g => new
            {
                id = g.id.ToString(),
                username = g.user.username,
                sodoordate = g.sodoor_date.ToPersianDate(),
                sodoortime = g.sodoor_date.ToShortTimeString(),
                fullname = g.orderfor.Fname + " " + g.orderfor.Lname,
                cellphone = g.orderfor.cell,
                frombranch = g.from_branch.branch_name,
                tobranch = g.to_branch.branch_name,
                sharh = g.sharh,
                done = (g.done == false ? "نزد " + g.to_branch.branch_name : "تحویل شده"),
                address = g.orderfor.address,
                frombranch_address = g.from_branch.Address,
                tobranch_address = g.to_branch.Address
            });


            var orderitems = db.tbl_orderdetails.Where(a => a.order_id == orderid).ToList().Select(g => new
            {
                productid = g.product_id.ToString(),
                Catgname = g.product_.category.categoryname,
                productname =g.product_.category.categoryname +"| "+g.product_.name + "| " + g.product_.codename+"| "+ g.product_.dimension,
                weight = g.product_.weight.ToString() + "kg",
                number = g.number.ToString(),
                totalweight = (g.product_.weight.HasValue? (g.product_.weight.Value * g.number).ToString():""),
                desc = g.description
              


            });


            StiReport report = new StiReport();
            string webroot = _hostingEnvironment.WebRootPath;
            string myroot = "/Content/reports/Reportorder.mrt";
            string targetroot = webroot + myroot;
            //report.Load(Path.Combine(_hostingEnvironment.WebRootPath, "/Content/reports/Reportorder.mrt"));
            report.Load(targetroot);
            report.RegData("orderinfo", order);
            report.RegData("orderitems", orderitems);
            
            CheckReference(report);

            return StiNetCoreViewer.GetReportResult(this, report);
        }


    }
}