using System;
using System.Collections.Generic;
using System.Linq;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoltaniWeb.Models.Domain;
using SoltaniWeb.Models.Extensions;
using SoltaniWeb.Models.structs;
using SoltaniWeb.Models.structs.CompanyVM;
using SoltaniWeb.Models.structs.PersonVM;
using SoltaniWeb.Models.Services.Company;
using SoltaniWeb.Models.Services.Person;
namespace SoltaniWeb.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        _4820_soltaniwebContext db = new _4820_soltaniwebContext();
        private readonly IPersonService _personService;

        public CompanyController(ICompanyService companyService, IPersonService personService)
        {
            _companyService = companyService;
            _personService = personService;
        }
        public IActionResult Index()
        {
            var companyTypeClass = new List<CompanyTypeClass>
            {
                new CompanyTypeClass{Id=0,Name="dfgf"},
                new CompanyTypeClass{Id=1,Name="sdfgsdfg"},
            };
            ViewData["CompanyTypeClass"] = companyTypeClass;
            ViewData["defaultCategory"] = companyTypeClass.First();
            return View();
        }

        public ActionResult Companies()
        {
           
            return PartialView("_CompanyList");
        }

        public ActionResult Company_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = _companyService.GetAll();
            var dsResultTemps = data.ToDataSourceResult(request);
            return Json(dsResultTemps);
        }
        [AcceptVerbs("Post")]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, CompanyViewModel company)
        {
            if (company != null && ModelState.IsValid)
            {
                _companyService.Update(company);
            }

            return Json(new[] { company }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult ListCompany()
        {
            return PartialView("_ListCompanyForDrop");
        }
        public JsonResult CompanyForDrop_Read()
        {
            var result = _companyService.GetAll().Select(i => new
            {
                CompanyName = i.Name,
                Id = i.Id
            });

            return Json(result);
        }

        #region CompanyPerson

        public IActionResult CompanyPerson(int companyId)
        {
            ViewBag.CompanyId= companyId;
            return PartialView("_CompanyPerson");
        }

        public IActionResult CompanyPerson_Read([DataSourceRequest] DataSourceRequest request,int? companyId)
        {
            var data = _companyService.GetCompanyPerson(companyId);
            var dsResultTemps = data.ToDataSourceResult(request);
            return Json(dsResultTemps);
        }

        public IActionResult Person()
        {
            return PartialView("_Person");
        }
        public IActionResult Person_Read([DataSourceRequest] DataSourceRequest request, int companyId)
        {
            var result = db.tbl_person.Where(x => !x.tbl_CompanyPerson.Any(y => y.CompanyId == companyId) || !x.tbl_CompanyPerson.Any())
                .Select(x => new PersonViewModel
                {
                    CodeMeli = x.codemelli,
                    Cell = x.cell ?? ((x.PersonInformationSettings == null || x.PersonInformationSettings.Count == 0) ? "" : x.PersonInformationSettings.FirstOrDefault(per => per.PropertyName == PersonInformationSetting.Mobile.ToString()).PropertyValue),
                    FullNamePerson = (x.Fname ??"")+ " " + (x.Lname ?? ""),
                    Id = x.id
                }).ToList();

            
            var dsResultTemps = result.ToDataSourceResult(request);
            return Json(dsResultTemps);
        }
        public ActionResult PersonCompanyDestroy(int companyId, int personId)
        {
            var resultStatus = new ResultStatus();
            try
            {
                if (companyId == 0 && personId == 0)
                {

                    resultStatus.IsSuccessed = false;
                    resultStatus.Message = "  مشتری را انتخاب کنید";
                    return Json(resultStatus);
                }

                resultStatus = _companyService.DeletPersonsFromCompany(companyId, personId);
            }
            catch (Exception e)
            {
                resultStatus.IsSuccessed = true;
                resultStatus.Message = "خطا در بروز عملیات";
            }

            return Json(resultStatus);
        }

        public ActionResult AddPersonToCompany(int companyId, int[] personId)
        {

            var resultStatus = new ResultStatus();
            try
            {
                if (companyId == 0 && personId.Length == 0)
                {

                    resultStatus.IsSuccessed = false;
                    resultStatus.Message = "گروه و مشتری را انتخاب کنید";
                    return Json(resultStatus);
                }

               resultStatus=_companyService.AddPersonsToCompany(companyId,personId);
            }
            catch (Exception e)
            {
                resultStatus.IsSuccessed = true;
                resultStatus.Message = "خطا در بروز عملیات";
            }

            return Json(resultStatus);
        }


        #endregion


        public IActionResult Create(int? id)
        {
            //var company = new CompanyViewModel();
            //if (id != null)
            //{
            //    company = _companyService.FindCompany(id.Value);
            //}
            //return View(company);
            return PartialView("_Create");
        }

        [HttpPost]
        public ActionResult Create(CompanyViewModel model)
        {
            
            ResultStatus op = new ResultStatus();
            
            try
            {
                if (ModelState.IsValid)
                {
                    model.CompanyType = model.CompanyType.ToEnum<CompanyType>().ToString();
                    if (model.Id != 0)
                    {
                        _companyService.Update(model);
                        op.Message = "عملیات درخواستی با موفقیت انجام شد";
                        op.IsSuccessed = true;
                    }
                    else
                    {
                        var companyId= _companyService.Create(model);
                        op.Message = companyId == 0 ? "خطا در عملیات" : "عملیات درخواستی با موفقیت انجام شد";
                        op.IsSuccessed = companyId != 0 ;
                    }
                   
                    
                    return Json(new { ResultStatus = op });
                }
                op.IsSuccessed = true;
                op.Message = "اطلاعات را کامل کنید";
                return Json(new { ResultStatus = op });
            }
            catch (Exception e)
            {
                op.IsSuccessed = false;
                op.Message = "خطا در عملیات";
                return Json(new { ResultStatus = op });
            }
           
        }
    }
}