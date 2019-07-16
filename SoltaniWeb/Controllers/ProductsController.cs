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

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using SoltaniWeb.Models.Services.Interfaces;
using System.Net;
using Kendo.Mvc.Infrastructure;

namespace SoltaniWeb.Controllers
{

    //[getstatisticController]
    //[Authorize]
    //[HandleError(View = "Error")]
    [ResponseCache(Duration = 600)]
    public class ProductsController : Controller
    {
        _4820_soltaniwebContext db3 = new _4820_soltaniwebContext();
        // GET: Products
        private IProductsServices ProductsServices;
        public ProductsController(IProductsServices _productsServices)
        {
            ProductsServices = _productsServices;
        }
        [Route("GetProducts/{CatgId}")]
        
        public ActionResult GetProducts(int CatgId = 0, string key = "")
        {

            var q = ProductsServices.GetproductsOfCategoryToShow(CatgId);
            if (q != null)
            {

                return View(q);
            }
            else
            {
                return NoContent();
            }

        }
        public ActionResult Searchproducts(string filter, int section_id = 0, int category_id = 0, int order = 0, int status = 0, int take = 6, int skip = 0)
        {

            if (filter == null)
            {
                return View();
            }
            if (string.IsNullOrEmpty(filter.Trim()))
            {
                return View();
            }
            var q = ProductsServices.Searchproducts(filter, section_id, category_id, order, status);

            int number = q.Count();
            var totalPages = (int)Math.Ceiling(number / (float)take);
            ViewBag.totalpages = totalPages;
            ViewBag.pagesize = take;
            ViewBag.skip = skip;
            int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
            ViewBag.pagenumber = pagenumber;
            ViewBag.filter = filter;
            var result = q.Select(n => n);
            var selectedproductlist = Request.Cookies["productsselected"];
            if (selectedproductlist != null)
            {
                selectedproducts s = JsonConvert.DeserializeObject<selectedproducts>(selectedproductlist);
                int scount = s.productlist.Count();
                int[] no = new int[scount];
                no = s.productlist.ToArray();


                ViewBag.selectedlist = no;
            }

            //int[] a = ViewBag.selectedlist.productlist;
            //string[] selectedlist = Request.Cookies["productsselected"].Split(',');
            return View(result.Skip(skip).Take(take));
        }
        public ActionResult Searchproductsbyajax(string filter, int[] productlist, int section_id = 0, int category_id = 0, int order = 0, int status = 0, int take = 6, int skip = 0)
        {
            var q = ProductsServices.Searchproducts(filter, section_id, category_id, order, status);

            int number = q.Count();
            ViewBag.number = number;
            var totalPages = (int)Math.Ceiling(number / (float)take);
            ViewBag.totalpages = totalPages;
            ViewBag.pagesize = take;
            ViewBag.skip = skip;
            int pagenumber = (int)Math.Floor(skip / (float)take) + 1;
            ViewBag.pagenumber = pagenumber;
            var result = q.Select(n => n);
            ViewBag.selectedlist = productlist;
            return PartialView("~/Views/Shared/PartialSearchProducts/_partialProductsinsearch.cshtml", result.Skip(skip).Take(take));

        }

        public ActionResult comparisonProducts(int p1 = 0, int p2 = 0, int p3 = 0, int p4 = 0)

        {

            int[] plist = new int[] { p1, p2, p3, p4 };
            var products = db3.tbl_products.Where(a => plist.Contains(a.idproduct));



            return View(products);
        }
        public ActionResult getproduct(int productid = 0)
        {
            var p = db3.tbl_products.Where(a => a.idproduct == productid).SingleOrDefault();
            return PartialView("~/Views/Shared/PartialSearchProducts/_partialMediaCom.cshtml", p);
        }

        public async Task<ActionResult> AGT(string s = "", string key = "")
        {

            ViewBag.title = "هایگلاس AGT | کالای چوب سلطانی";

            var q = ProductsServices.GetproductsOfCategoryToShow(1);

            return View(q);
        }

        public ActionResult ashak_mdf(string s = "", string key = "")
        {
            productsrepository rep_products = new productsrepository();

            var q = ProductsServices.GetproductsOfCategoryToShow(80);

            ViewBag.title = "هایگلاس Ashak | کالای چوب سلطانی";



            return View(q);

        }
        public ActionResult larixwood_Highgloss(string s = "", string key = "")
        {

            var q = ProductsServices.GetproductsOfCategoryToShow(76);

            ViewBag.title = "هایگلاس Larix | کالای چوب سلطانی";





            return View(q);

        }
        public ActionResult samsung_staron(string s = "", string key = "")
        {


            var q = ProductsServices.GetproductsOfCategoryToShow(48);

            ViewBag.title = "استارون سامسونگ | کالای چوب سلطانی";




            return View(q);
        }


        public ActionResult seetec(string s = "", string key = "")
        {

            var q = ProductsServices.GetproductsOfCategoryToShow(59);

            ViewBag.title = "هایگلاس سیتک | کالای چوب سلطانی";




            return View(q);
        }

        public ActionResult Xtra_Solid(string s = "", string key = "")
        {

            var q = ProductsServices.GetproductsOfCategoryToShow(51);

            ViewBag.title = "ام دی اف اکسترا سالید | کالای چوب سلطانی";



            return View(q);
        }

        public ActionResult Xtra_Roysa(string s = "", string key = "")
        {

            var q = ProductsServices.GetproductsOfCategoryToShow(52);

            ViewBag.title = "ام دی اف اکسترا رویسا | کالای چوب سلطانی";




            return View(q);
        }

        public ActionResult Alvic_MDF(string s = "", string key = "")
        {

            var q = ProductsServices.GetproductsOfCategoryToShow(49);

            ViewBag.title = "آلویک | کالای چوب سلطانی";




            return View(q);
        }


        public ActionResult CLEAF_MDF(string s = "", string key = "")
        {

            var q = ProductsServices.GetproductsOfCategoryToShow(50);

            ViewBag.title = "ام دی اف کلیف ایتالیا | کالای چوب سلطانی";




            return View(q);
        }


        public ActionResult Bestwood_MDF(string s = "", string key = "")
        {

            var q = ProductsServices.GetproductsOfCategoryToShow(47);

            ViewBag.title = "ام دی اف Bestwood | کالای چوب سلطانی";




            return View(q);
        }
        public ActionResult Royal_mdf(string s = "", string key = "")

        {

            var q = ProductsServices.GetproductsOfCategoryToShow(27);

            ViewBag.title = "رویال MDF | کالای چوب سلطانی";

            return View(q);
        }
        public ActionResult SWP_mdf(string s = "", string key = "")
        {

            var q = ProductsServices.GetproductsOfCategoryToShow(33);

            ViewBag.title = "ام دی اف SWP | کالای چوب سلطانی";




            return View(q);
        }

        public ActionResult Other_mdf(string s = "", string key = "")
        {

            var q = ProductsServices.GetproductsOfCategoryToShow(22);

            ViewBag.title = "ام دی اف سایر شرکتها | کالای چوب سلطانی";




            return View(q);
        }

        public ActionResult Royal_melamine(string s = "", string key = "")
        {

            var q = ProductsServices.GetproductsOfCategoryToShow(29);

            ViewBag.title = "ملامینه رویال | کالای چوب سلطانی";




            return View(q);
        }

        public ActionResult Polywood(string s = "", string key = "")
        {

            var q = ProductsServices.GetproductsOfCategoryToShow(18);

            ViewBag.title = "چند لایی | کالای چوب سلطانی";




            return View(q);
        }

        public ActionResult MDF_25mm(string s = "", string key = "")
        {

            var q = ProductsServices.GetproductsOfCategoryToShow(31);

            ViewBag.title = "ام دی اف 25 میل | کالای چوب سلطانی";




            return View(q);
        }

        public ActionResult Luminak_Adhesives(string s = "", string key = "")
        {

            var q = ProductsServices.GetproductsOfCategoryToShow(30);

            ViewBag.title = "چسب لومیناک| کالای چوب سلطانی";




            return View(q);
        }
        public ActionResult Rafsenjan_neopan(string s = "", string key = "")
        {

            var q = ProductsServices.GetproductsOfCategoryToShow(13);

            ViewBag.title = "نئوپان رفسنجان| کالای چوب سلطانی";




            return View(q);
        }

        public async Task<ActionResult> ACK(string s = "", string key = "")
        {

            var q = ProductsServices.GetproductsOfCategoryToShow(2);

            ViewBag.title = "  آذران چوب کیمیا MDF | کالای چوب سلطانی";

            return View(q);
        }

        public ActionResult pakchoob(string s = "", string key = "")
        {

            var q = ProductsServices.GetproductsOfCategoryToShow(32);


            ViewBag.title = "  پاک چوب MDF | کالای چوب سلطانی";

            return View(q);
        }

        public ActionResult xtra(string s = "", string key = "")
        {

            var q = ProductsServices.GetproductsOfCategoryToShow(24);


            ViewBag.title = "  ام دی اف برجسته و فوق برجسته اکسترا | کالای چوب سلطانی";

            return View(q);
        }


        public ActionResult mellamine(string s = "", string key = "ملامینه - پویا - نئوپان - رفسنجان - شیراز - ایران ")
        {

            var q = ProductsServices.GetproductsOfCategoryToShow(4);

            ViewBag.title = "  ملامینه پویا - نئوپان رفسنجان | کالای چوب سلطانی";

            return View(q);
        }

        public ActionResult plate(string s = "", string key = "")
        {

            var q = ProductsServices.GetproductsOfCategoryToShow(5);


            ViewBag.title = " صفحه کابینت سراوان چوب سبز  | کالای چوب سلطانی";

            return View(q);


        }

        public ActionResult zibasazco_plate(string s = "", string key = "")
        {

            var q = ProductsServices.GetproductsOfCategoryToShow(21);

            ViewBag.title = " صفحه کابینت شرکت زیباساز  | کالای چوب سلطانی";

            return View(q);


        }

        public ActionResult pvcband(string s = "", string key = "")
        {

            var q = ProductsServices.GetproductsOfCategoryToShow(8);

            ViewBag.title = " نوار پی وی سی تکنوپل | کالای چوب سلطانی";

            return View(q);
        }

        public ActionResult pvcsheet(string s = "", string key = "")
        {

            var q = ProductsServices.GetproductsOfCategoryToShow(9);

            ViewBag.title = " پی وی سی شیت | کالای چوب سلطانی";

            return View(q);
        }
        public ActionResult RAW_MDF(string s = "", string key = "")
        {

            var q = ProductsServices.GetproductsOfCategoryToShow(40);

            ViewBag.title = " ام دی اف خام| کالای چوب سلطانی";

            return View(q);
        }

        public ActionResult wood(string s = "", string key = "")
        {

            var q = ProductsServices.GetproductsOfCategoryToShow(14);

            ViewBag.title = " تخته روسی | کالای چوب سلطانی";

            return View(q);
        }
        public ActionResult parslaminate_3mm(string s = "", string key = "")
        {

            var q = ProductsServices.GetproductsOfCategoryToShow(7);

            ViewBag.title = " سه میل پارس لمینت | کالای چوب سلطانی";

            return View(q);
        }

        public ActionResult AGE_Highgloss(string s = "", string key = "")
        {

            var q = ProductsServices.GetproductsOfCategoryToShow(6);


            ViewBag.title = " هایگلاس AGE | کالای چوب سلطانی";

            return View(q);


        }



        public ActionResult mdf_3mm(string s = "", string key = "")
        {

            var q = ProductsServices.GetproductsOfCategoryToShow(23);


            ViewBag.title = " ام دی اف 3 میل کاغذی | کالای چوب سلطانی";

            return View(q);


        }

        public ActionResult White_MDF(string s = "", string key = "")
        {

            var q = ProductsServices.GetproductsOfCategoryToShow(3);


            ViewBag.title = " ام دی اف سفید صابونی | کالای چوب سلطانی";

            return View(q);


        }

        public ActionResult AGE_baseboard(string s = "", string key = "")
        {


            var q = db3.tbl_products.Where(a => a.categoryid == 25 && a.grade.StartsWith("G")).OrderBy(a => a.codename);

            ViewBag.title = " قرنیز ام دی اف AGE | کالای چوب سلطانی";

            return View(q);


        }

        public ActionResult AGE_Wallmount(string s = "", string key = "")
        {

            var q = db3.tbl_products.Where(a => a.categoryid == 25 && a.grade.StartsWith("D-MDF")).OrderBy(a => a.codename);

            ViewBag.title = "  دیوارکوب ام دی اف و پی وی سی AGE | کالای چوب سلطانی";

            return View(q);


        }


        public ActionResult AGE_Wall_Highgloss(string s = "", string key = "")
        {

            var q = db3.tbl_products.Where(a => a.categoryid == 25 && a.grade.StartsWith("D-HG")).OrderBy(a => a.codename);

            ViewBag.title = "  دیوارکوب هایگلاس AGE | کالای چوب سلطانی";

            return View(q);


        }

        public ActionResult AGE_profil(string s = "", string key = "")
        {

            var q = db3.tbl_products.Where(a => a.categoryid == 25 && a.grade.StartsWith("profil")).OrderBy(a => a.codename);

            ViewBag.title = "  پروفیل AGE | کالای چوب سلطانی";

            return View(q);


        }

        [HttpPost]
        public ActionResult sortingbyalpha(int id = 0)
        {



            productsrepository rep_products = new productsrepository();
            entityproducutsreposit rep_ent = new entityproducutsreposit();
            var q = rep_products.getproducts().Where(a => a.categoryid.Equals(id)).OrderBy(a => a.name);

            return PartialView("_Partialproductslist", q);


        }


        [HttpPost]
        public ActionResult sortingdefault(int id = 0)
        {



            productsrepository rep_products = new productsrepository();
            entityproducutsreposit rep_ent = new entityproducutsreposit();
            var q = rep_products.getproducts().Where(a => a.categoryid.Equals(id)).OrderBy(a => a.codename);

            return PartialView("_Partialproductslist", q);
        }

        [HttpPost]
        public ActionResult serachforproducts(int id = 0, string text = "")
        {



            productsrepository rep_products = new productsrepository();
            entityproducutsreposit rep_ent = new entityproducutsreposit();
            var q = rep_products.getproducts().Where(a => a.categoryid.Equals(id) && (a.name.Contains(text) || a.codename.Contains(text))).OrderBy(a => a.codename);

            return PartialView("_Partialproductslist", q);
        }

        [HttpPost]
        public JsonResult filterbysade(int id = 0)
        {


            var qname = db3.tbl_products.Where(a => (a.categoryid == id && a.grade == "A")).OrderBy(a => a.codename).Select(a => new { id = a.idproduct, name = a.name, image = a.imageproducts, code = a.codename, grade = a.grade });
            //qname.FirstOrDefault().name
            return Json(qname);


        }


        [HttpPost]
        public JsonResult filterbyfantezi(int id = 0)
        {


            var qname = db3.tbl_products.Where(a => (a.categoryid == id && a.grade == "B")).OrderBy(a => a.codename).Select(a => new { id = a.idproduct, name = a.name, image = a.imageproducts, code = a.codename, grade = a.grade });
            //qname.FirstOrDefault().name
            return Json(qname);


        }


        [HttpPost]
        public JsonResult filterbywood(int id = 0)
        {


            var qname = db3.tbl_products.Where(a => (a.categoryid == id && a.grade == "c")).OrderBy(a => a.codename).Select(a => new { id = a.idproduct, name = a.name, image = a.imageproducts, code = a.codename, grade = a.grade });
            //qname.FirstOrDefault().name
            return Json(qname);


        }


        [HttpPost]
        public JsonResult filterbydecor(int id = 0)
        {


            var qname = db3.tbl_products.Where(a => (a.categoryid == id && a.grade == "D")).OrderBy(a => a.codename).Select(a => new { id = a.idproduct, name = a.name, image = a.imageproducts, code = a.codename, grade = a.grade });
            //qname.FirstOrDefault().name
            return Json(qname);


        }


        [HttpPost]
        public ActionResult filterbyA(int id = 0)
        {



            productsrepository rep_products = new productsrepository();
            entityproducutsreposit rep_ent = new entityproducutsreposit();
            var q = rep_products.getproducts().Where(a => a.categoryid.Equals(id) && a.grade.ToUpper() == "A").OrderBy(a => a.codename);

            return PartialView("_Partialproductslist", q);


        }


        public ActionResult filterbyB(int id = 0)
        {



            productsrepository rep_products = new productsrepository();
            entityproducutsreposit rep_ent = new entityproducutsreposit();
            var q = rep_products.getproducts().Where(a => a.categoryid.Equals(id) && a.grade.ToUpper() == "B").OrderBy(a => a.codename);

            return PartialView("_Partialproductslist", q);


        }
        public ActionResult filterbyC(int id = 0)
        {



            productsrepository rep_products = new productsrepository();
            entityproducutsreposit rep_ent = new entityproducutsreposit();
            var q = rep_products.getproducts().Where(a => a.categoryid.Equals(id) && a.grade.ToUpper() == "C").OrderBy(a => a.codename);

            return PartialView("_Partialproductslist", q);


        }




        [HttpPost]
        public JsonResult addcountsamples(int id = 0)
        {


            var qname = db3.tbl_products.Where(a => a.categoryid == id).OrderBy(a => a.name).Select(a => new { id = a.idproduct, name = a.name, image = a.imageproducts, code = a.codename, grade = a.grade });
            //qname.FirstOrDefault().name
            return Json(qname);


        }

        public FileResult getimage(int id = 0)
        {

            var ObjFiles = db3.tbl_products.Where(a => a.idproduct == id).SingleOrDefault();



            return File(ObjFiles.image, System.Net.Mime.MediaTypeNames.Application.Octet, ObjFiles.name);
        }
        public ActionResult getproductinfo(int productid = 0)
        {

            var q = db3.tbl_products.Where(a => a.idproduct == productid).SingleOrDefault();
            return PartialView("_partialproductinformation", q);

        }

    }
}