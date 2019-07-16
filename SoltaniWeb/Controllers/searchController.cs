using SoltaniWeb.Models.Domain;
using SoltaniWeb.Models.repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SoltaniWeb.Controllers
{
    //       [HandleError(View = "Error")]
    [getstatisticController]
    public class searchController : Controller
    {
        // GET: search
        public ActionResult Index()
        {
            return View();
        }
    
            _4820_soltaniwebContext db = new _4820_soltaniwebContext();
        public ActionResult searchsamplesfiles(string searchitems = "")
        {
            var q = db.tbl_files.Where(a => (a.keywords.Contains(searchitems) || a.madeby.Fname.Contains(searchitems) || a.madeby.Lname.Contains(searchitems) || a.decortype.name.Contains(searchitems) || a.tbl_samples.FirstOrDefault().products.keywords.Contains(searchitems))).OrderByDescending(a => a.id);


            ViewBag.title = "کالای چوب سلطانی" + "|" + " نتایج جستجو در خصوص  " + " " + searchitems;
            ViewBag.search = "نتایج جستجو در خصوص : ";
            ViewBag.searchitems = searchitems;

            return View(q);

        }



        [HttpGet]
        public ActionResult advanced_search(string decorname = "", string designer = "")
        {
            if (decorname == "")
            {
                if (designer == "")
                {
                    var q = "";
                    return View();
                }
                else
                {
                   
                    var q = db.tbl_files.Where(a => ((a.madeby.Fname.Contains(designer) || a.madeby.Lname.Contains(designer)) && (a.decortype.name.Contains(decorname)))).OrderByDescending(a => a.id);

                    ViewBag.title = "کالای چوب سلطانی" + "|" + " نتایج جستجو در خصوص  " + " " + designer;
                    ViewBag.search = "نتایج جستجو در خصوص : ";
                    ViewBag.designer = designer;
                    ViewBag.decorname = decorname;
                    return View(q);
                }

            }
            else
            {
                
                var q = db.tbl_files.Where(a => ((a.madeby.Fname.Contains(designer) || a.madeby.Lname.Contains(designer)) && (a.decortype.name.Contains(decorname)))).OrderByDescending(a => a.id);

                ViewBag.title = "کالای چوب سلطانی" + "|" + " نتایج جستجو در خصوص  " + " " + designer;
                ViewBag.search = "نتایج جستجو در خصوص : ";
                ViewBag.designer = designer;
                ViewBag.decorname = decorname;
                return View(q);

            }


        }

      
        public ActionResult searchproductsbyajax(string searchitem = "", int section_id = 0, int category_id =0 , int status=0)
        {
                     

            if (status == 0)
            {
                ViewBag.status = " در تمامی کالا ها";
                ViewBag.statusid = 0;

                if (section_id == 0)
                {
                    var results = db.tbl_products.Where(a => a.status == true && (a.name.Contains(searchitem) || a.keywords.Contains(searchitem) || a.description.Contains(searchitem) || a.codename.Contains(searchitem) || a.dimension.Contains(searchitem) || a.category.categoryname.Contains(searchitem))).OrderBy(a => a.categoryid).ToList().Select(x=> new {idproduct=x.idproduct, name = x.name , categoryname=x.category.categoryname , image = x.imageproducts , codename = x.codename , number = x.tbl_listkala.Sum(k=>k.kalanumberm) });

                    ViewBag.searchitem = searchitem;
                    ViewBag.section = "تمام بخش ها  ";
                    ViewBag.sectionid = 0;
                    return Json(results);
                }
                else
                {
                    var results = db.tbl_products.Where(a => (a.category.section_.id == section_id && a.status == true) && (a.name.Contains(searchitem) || a.keywords.Contains(searchitem) || a.description.Contains(searchitem) || a.codename.Contains(searchitem) || a.dimension.Contains(searchitem) || a.category.categoryname.Contains(searchitem))).OrderBy(a => a.categoryid).ToList().Select(x => new { idproduct = x.idproduct, name = x.name, categoryname = x.category.categoryname, image = x.imageproducts, codename = x.codename, number = x.tbl_listkala.Sum(k => k.kalanumberm) });

                    ViewBag.searchitem = searchitem;
                    ViewBag.section = db.tbl_section.Where(a => a.id == section_id).SingleOrDefault().name;
                    ViewBag.sectionid = db.tbl_section.Where(a => a.id == section_id).SingleOrDefault().id;

                    return Json(results);
                }

            }
            else
            {
                ViewBag.status = " در کالاهای موجود";
                ViewBag.statusid = 1;

                if (section_id == 0)
                {
                    var results = db.tbl_products.Where(a => (a.status == true && a.tbl_listkala.Sum(f => f.kalanumberm) > 0) && (a.name.Contains(searchitem) || (a.keywords.Contains(searchitem) || a.description.Contains(searchitem) || a.codename.Contains(searchitem) || a.dimension.Contains(searchitem) || a.category.categoryname.Contains(searchitem)))).OrderBy(a => a.categoryid).ToList().Select(x => new { idproduct = x.idproduct, name = x.name, categoryname = x.category.categoryname, image = x.imageproducts, codename = x.codename, number = x.tbl_listkala.Sum(k => k.kalanumberm) });

                    ViewBag.searchitem = searchitem;
                    ViewBag.section = "تمام بخش ها  ";
                    ViewBag.sectionid = 0;

                    return Json(results);
                }
                else
                {
                    var results = db.tbl_products.Where(a => a.category.section_.id == section_id && a.status == true && a.tbl_listkala.Sum(f => f.kalanumberm) > 0 && (a.name.Contains(searchitem) || a.keywords.Contains(searchitem) || a.description.Contains(searchitem) || a.codename.Contains(searchitem) || a.dimension.Contains(searchitem) || a.category.categoryname.Contains(searchitem))).OrderBy(a => a.categoryid).ToList().Select(x => new { idproduct = x.idproduct, name = x.name, categoryname = x.category.categoryname, image = x.imageproducts, codename = x.codename, number = x.tbl_listkala.Sum(k => k.kalanumberm) });

                    ViewBag.searchitem = searchitem;
                    ViewBag.section = db.tbl_section.Where(a => a.id == section_id).SingleOrDefault().name;
                    ViewBag.sectionid = db.tbl_section.Where(a => a.id == section_id).SingleOrDefault().id;

                    return Json(results);
                }





            }






        }




        public ActionResult switchtofarsi()
        {
           
            var q = db.tbl_products.Select(a => new { name = a.name });
            q.ToList().ForEach(a =>
            {
                a.name.ToString().Replace("ي", "ی").Replace("ك", "ک"); 
            });
            return RedirectToAction("index", "home");
        }

        [HttpPost]
        public JsonResult categoryfind(int section_id = 0)
        {
           
            var categorylist = db.tbl_category.Where(a => a.section_id == section_id).ToList();
            var category = categorylist.Select(g => new { id = g.id, name = g.categoryname });
            return Json(category);
        }

    }
}